using UnityEngine;
using UnityEngine.SceneManagement;

public class DrillHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int regen = 0;
    [SerializeField] private DrillController drillController;
    [SerializeField] private GameObject deathPanel;

    private int currentHealth;
    private bool isDead = false;
    public int CurrentHealth => currentHealth;
    public bool IsDead => isDead;

    void Start()
    {
        currentHealth = maxHealth;

        if (drillController == null)
            drillController = GetComponent<DrillController>();

        if (drillController != null)
            drillController.OnReachedStop += RegenerateHealth;
    }

    void OnDestroy()
    {
        if (drillController != null)
            drillController.OnReachedStop -= RegenerateHealth;
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        Debug.Log("Бур получил урон. HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead) return;
        isDead = true;

        Debug.Log("Бур уничтожен");

        if (drillController != null)
            drillController.StopDrill();

        if (deathPanel != null)
            deathPanel.SetActive(true);
    }

    // ГЕТТЕРЫ 
    public int GetMaxHp()
    {
        return maxHealth;
    }

    public int GetRegen()
    {
        return regen;
    }

    // СЕТТЕРЫ
    public void SetNewMaxHp(int newValue)
    {
        if (newValue > 20)
        {
            newValue = 20;
        }

        maxHealth = newValue;
    }

    public void SetNewRegen(int newValue)
    {
        if (newValue > 5)
        {
            newValue = 5;
        }

        regen = newValue;
    }

    void RegenerateHealth()
    {
        if (isDead || regen <= 0) return;
        
        currentHealth = Mathf.Min(currentHealth + regen, maxHealth);
        Debug.Log($"Бур восстановил {regen} HP. Текущее здоровье: {currentHealth}/{maxHealth}");
    }
}