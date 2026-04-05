using UnityEngine;

public class OrePickupScript : MonoBehaviour
{
[Header("Настройки притягивания")]
    [SerializeField] private float pickupRange = 0.5f;
    [SerializeField] private float attractSpeed = 3f;  
    
    private Rigidbody2D rb;
    private Transform player;
    private bool isAttracted = false;
    private string oreTag;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        oreTag = gameObject.tag;
    }

    void Update()
    {
        if (player == null) return;
        float distance = Vector2.Distance(transform.position, player.position);
        
        if (distance <= pickupRange)
        {
            isAttracted = true;
            
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * attractSpeed;
            
            if (distance < 0.5f)
            {
                PickupOreByTag();
            }
        }
        else if (!isAttracted)
        {
            rb.linearVelocity *= 0.98f;
        }
    }

    private void PickupOreByTag()
    {
        PlayerResources playerResources = player.GetComponent<PlayerResources>();
        if (playerResources != null)
        {
            switch (oreTag)
            {
                case "Traviy":
                    playerResources.AddTraviyOre(1);
                    break;
                case "Coal":
                    playerResources.AddCoalOre(1);
                    break;
                case "Vibranium":
                    playerResources.AddVibroOre(1);
                    break;
                default:
                    Debug.LogWarning("Неизвестный тип руды: " + oreTag);
                    break;
            }
        }
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRange);
    }
}
