using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Player : Singleton<Player>
{
    [Header("移动")]
    public bool canMove = true;
    public Vector2 Direction;
    public float CurrrentSpeed = 3;
    private float x, y;

    [Header("玩家属性")]
    public float HP = 100;
    public bool isDead = false;
    public Vector3 RightPos = new(0.32f, 0.26f, 0f);
    public Vector3 LeftPos = new(-0.32f, 0.26f, 0f);
    public float FireSpeed = 0.1f;

    [Header("玩家组件")]
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(Fire), 0, FireSpeed);
    }

    private void Update()
    {
        Move();
        Dead();
    }

    private void Move()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        Direction = new Vector2(x, y) * CurrrentSpeed;

        transform.position = new(Mathf.Clamp(transform.position.x, -2.8f, 2.8f), Mathf.Clamp(transform.position.y, -4.25f, 4.25f));

        if (canMove)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                transform.Translate(2 * Time.deltaTime * Direction);
            else
                transform.Translate(Direction * Time.deltaTime);
        }
    }

    private void Dead()
    {
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (HP <= 0)
        {
            HP = 0;
            isDead = true;
            canMove = false;
            animator.SetBool("isLiving", false);
        }
        if (animatorStateInfo.normalizedTime >= 0.99f && animatorStateInfo.IsName("PlayerDown"))
        {
            Destroy(gameObject);
            GameManager.Instance.isPlaying = false;
        }
    }

    private void Fire()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Behaviours.Fire("PlayerBullet", transform.position, RightPos, LeftPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            canMove = false;
            animator.SetBool("isLiving", false);
            isDead = true;
        }
        if (collision.CompareTag("EnemyBullet"))
        {
            HP -= Bullet.EnemyBulletDamage;
            Debug.Log(Bullet.EnemyBulletDamage);
        }
    }

    private void OnDestroy()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var obj in Enemies)
            Pool.Recyle(obj);

        GameObject[] PlayerBullets = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (var obj in PlayerBullets)
            Pool.Recyle(obj);

        GameObject[] EnemyBullets = GameObject.FindGameObjectsWithTag("EnemyBullet");
        foreach (var obj in EnemyBullets)
            Pool.Recyle(obj);
    }
}
