using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 15f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyCrawler enemy = collision.GetComponent<EnemyCrawler>();
            if (enemy != null)
                enemy.TakeDamage(damage);

            Destroy(gameObject);
            return;
        }

        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}