using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [Header("ÒÆ¶¯")]
    public Vector2 Direction;
    public bool canMove;
    public float Speed = 3;
    public float MoveTime;
    public Vector3 min = new(-3, 0, 0);
    public Vector3 max = new(3, 4.5f, 0);

    [Header("ÊôÐÔ")]
    public float HP = 100;
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    [Header("¿ª»ð")]
    public bool canFire = false;
    public Vector3 FirePos = new(0, 0.5f);
    public float FireSpeed = 0.5f;

    private void Awake()
    {
        transform.SetParent(GameObject.Find("EnemyManager").transform);
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        canMove = true;
        HP = Enemy.Enemy2HP;
        animator.SetBool("isLiving", true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(Fire), 0, FireSpeed);
    }

    private void FixedUpdate()
    {
        MoveTime -= 0.02f;
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
                canFire = true;
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
        if (animatorStateInfo.normalizedTime >= 0.99f && animatorStateInfo.IsName("Enemy2Down"))
        {
            FireSpeed -= EnemyManager.Instance.Enemy2Counter * 0.01f;
            Bullet.EnemyBulletDamage += 2;
            EnemyManager.Instance.Enemy2Counter++;
            Player.Instance.HP += 3;
            Enemy.Enemy2HP += 10;
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
