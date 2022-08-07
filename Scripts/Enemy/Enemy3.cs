using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    [Header("移动")]
    public Vector2 Direction;
    public bool canMove;
    public float Speed = 1;
    public float MoveTime;
    public Vector3 min = new(-3, 0, 0);
    public Vector3 max = new(3, 4.5f, 0);

    [Header("属性")]
    public float HP = 500;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    [Header("开火")]
    public float FireInterval = 2;
    public bool canFire = false;
    public Vector3 FirePos = new(0, 0.5f);
    public float FireSpeed = 0.1f;

    private void Awake()
    {
        transform.SetParent(GameObject.Find("EnemyManager").transform);
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        canMove = true;
        HP = Enemy.Enemy3HP;
        FireInterval = 2f;
        animator.SetBool("isLiving", true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(Fire), 0, FireSpeed);
    }

    private void FixedUpdate()
    {
        MoveTime -= 0.02f;
        FireInterval -= 0.02f;
        if (FireInterval <= 0)
        {
            FireInterval = 2f;
            canFire = !canFire;
        }
    }

    private void Update()
    {
        Move();
        Dead();
        if (Player.Instance.isDead)
            canFire = false;
    }

    private void Fire()
    {
        if (canFire)
            Behaviours.Fire("EnemyBullet", transform.position, FirePos);
    }

    private void Move()
    {
        if (canMove)
        {
            if (transform.position.y > 4f)
                transform.Translate(new Vector3(0, -2, 0) * Time.deltaTime);
            else
            {
                if (MoveTime < 0)
                {
                    Direction = new(Random.Range(-Speed, Speed), Random.Range(-Speed, Speed));
                    MoveTime = Random.Range(2f, 3f);
                }
                Direction = Behaviours.MoveEdgeDetection(transform.position, Direction, min, max);
                transform.Translate(Direction * Time.deltaTime);
            }
        }
    }

    private void Dead()
    {
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (HP < 0)
        {
            canMove = false;
            canFire = false;
            animator.SetBool("isLiving", false);
        }
        if (animatorStateInfo.normalizedTime >= 0.99f && animatorStateInfo.IsName("Enemy3Down"))
        {
            FireInterval -= EnemyManager.Instance.Enemy2Counter * 0.1f;
            Bullet.EnemyBulletDamage += 4;
            EnemyManager.Instance.Enemy3Counter++;
            Player.Instance.HP += 5;
            Enemy.Enemy3HP += 50;
            Enemy.Enemy1HP += 5;
            Enemy.Enemy1Speed++;
            Pool.Recyle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HP -= Bullet.PlayerBulletDamage;
            animator.SetTrigger("Hit");
        }
    }
}
