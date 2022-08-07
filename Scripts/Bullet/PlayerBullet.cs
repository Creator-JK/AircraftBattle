using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private Vector2 Direction = new(0, 5);

    private void Start()
    {
        transform.SetParent(GameObject.Find("BulletManager").transform);
    }

    private void FixedUpdate()
    {
        if (transform.position.y > 5)
            Pool.Recyle(gameObject);
    }

    private void Update()
    {
        transform.Translate(Direction * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
            Pool.Recyle(gameObject);
    }
}
