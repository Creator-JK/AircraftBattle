using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo animatorStateInfo;

    public bool canMove = true;
    public float HP = 10;
    public float Speed;
    public float y;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        transform.SetParent(GameObject.Find("EnemyManager").transform);
    }

    private void OnEnable()
    {
        y = Random.Range(-4.5f, 0f);
        Speed = Enemy.Enemy1Speed;
        HP = Enemy.Enemy1HP;
        animator.SetBool("isLiving", true);
        canMove = true;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < y)
        {
            animator.SetBool("isLiving", false);
            canMove = false;
        }
    }

    private void Update()
    {
        if (canMove)
            transform.Translate(Speed * Time.deltaTime * Vector3.down);
        Dead();
    }

    private void Dead()
    {
        animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        if (HP < 0)
        {
            animator.SetBool("isLiving", false);
            canMove = false;
        }
        if (animatorStateInfo.normalizedTime >= 0.99f && animatorStateInfo.IsName("Enemy1Down"))
        {
            Pool.Recyle(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            HP -= Bullet.PlayerBulletDamage;
        }
    }
}
