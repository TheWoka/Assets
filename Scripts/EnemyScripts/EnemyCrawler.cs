using UnityEngine;

public class EnemyCrawler : MonoBehaviour
{
    [Header("Движение")]
    [SerializeField] private float baseVerticalSpeed = 2.5f;
    [SerializeField] private float verticalSpeedVariation = 0.5f; 
    [SerializeField] private float horizontalSpeed = 3f;
    
    [Header("Поведение")]
    [SerializeField] private float wanderRadius = 2f; 
    [SerializeField] private float wanderFrequency = 1.5f;  
    [SerializeField] private float wanderJitter = 0.4f; 
    [SerializeField] private float jitterSpeed = 3f;    
    [SerializeField] private float driftFactor = 0.3f; 
    [SerializeField] private bool followDrillVertically = true; 

    private float wanderPhaseOffset; // Короче сдвиг фазы по синусу

    
    [Header("Атака")]
    [SerializeField] private int contactDamage = 1;

    [Header("Жизни")]
    [SerializeField] private int hp = 3;
    private Transform drillTarget;
    private float wanderPhaseOffsett;
    private float currentVerticalSpeed;
    private float jitterSeed; 
    private float driftVelocity;

    void Start()
    {
        currentVerticalSpeed = baseVerticalSpeed + Random.Range(-verticalSpeedVariation, verticalSpeedVariation);

        wanderPhaseOffsett = Random.Range(0f, 100f);
        jitterSeed = Random.Range(0f, 1000f);
    }

    public void SetVerticalSpeed(float newSpeed)
    {
        baseVerticalSpeed = newSpeed;
        currentVerticalSpeed = newSpeed + Random.Range(-verticalSpeedVariation, verticalSpeedVariation);
    } 

    void FixedUpdate()
    {
        if (drillTarget == null) return;

        // Основная синусоида
        float wanderPhase = Time.time * wanderFrequency + wanderPhaseOffsett;
        float sineWander = Mathf.Sin(wanderPhase) * wanderRadius;
        
        // Плавное дрожание через шум Перлина (не резкий рандом!)
        float jitter = Mathf.PerlinNoise(Time.time * jitterSpeed, jitterSeed) * 2f - 1f; // диапазон [-1; 1]
        jitter *= wanderJitter; // масштабируем силу
        
        // Итоговое целевое смещение: синус + дрожание
        float targetXOffset = sineWander + jitter;
        float targetX = drillTarget.position.x + targetXOffset;
        
        // Плавное движение к цели по X
        float currentX = transform.position.x;
        float newX = Mathf.SmoothDamp(currentX, targetX, ref driftVelocity, driftFactor);

        // Движение по Y
        float newY;
        if (followDrillVertically)
        {
            float targetY = drillTarget.position.y + Random.Range(-1f, 2f);
            newY = Mathf.MoveTowards(transform.position.y, targetY, currentVerticalSpeed * Time.deltaTime);
        }
        else
        {
            newY = transform.position.y + currentVerticalSpeed * Time.deltaTime;
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
        
        // Визуальное покачивание (опционально)
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Sin(Time.time * 10) * 5f);

        // Удаление если улетел слишком далеко
        if (drillTarget != null && transform.position.y > drillTarget.position.y + 15f)
        {
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        drillTarget = target;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0) Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Drill"))
        {
            DrillHealth drillHealth = collision.GetComponent<DrillHealth>();
            if (drillHealth != null)
            {
                drillHealth.TakeDamage(contactDamage);
            }
            Destroy(gameObject);
        }
    }

    void Die()
    {
        // ДАЛЕЕ ЭФФЕКТЫ ТУТ
        Destroy(gameObject);
    }
    
    void OnDrawGizmosSelected()
    {
        if (drillTarget != null)
        {
            Gizmos.color = new Color(1, 0.5f, 0, 0.3f);
            Gizmos.DrawLine(
                drillTarget.position + Vector3.left * wanderRadius, 
                drillTarget.position + Vector3.right * wanderRadius
            );
        }
    }
}