using UnityEngine;
using UnityEngine.Tilemaps;

public class Ore: MonoBehaviour
{
    [SerializeField] private float oreHealth = 15f;
    [SerializeField] private GameObject drop;
    private float currentHealth;
    private bool isBroken = false;

    // Назначение всякого
    void Start()
    {
        currentHealth = oreHealth;
    }

    // Руда сосет урон
    public void ApplyDamage(float amount)
    {
        if (isBroken) return;
        currentHealth -= amount;
        Debug.Log($"Руда: {currentHealth}/{oreHealth}");
        
        if (currentHealth <= 0)
        {
            BreakOre();
        }
    }

    // Ломание руды
    private void BreakOre()
    {
        isBroken = true;

        // Дроп руды короче
        if (drop != null)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.3f, 0.3f), 0);
            Instantiate(drop, transform.position + randomOffset, Quaternion.identity);
            Debug.Log("СПАВН РУДЫ!");
        }
        
        Destroy(gameObject);
    }
}
