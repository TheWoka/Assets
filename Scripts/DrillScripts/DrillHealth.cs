using UnityEngine;
using UnityEngine.SceneManagement;

public class DrillHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    [SerializeField] private DrillController drillController;
    [SerializeField] private float loadSceneDelay = 1f;

    private int currentHealth;
    private MenuGameScript menuGame;
    private bool isDead = false;
    public int CurrentHealth => currentHealth;
    public bool IsDead => isDead;

    void Start()
    {
        currentHealth = maxHealth;

        if (drillController == null)
            drillController = GetComponent<DrillController>();
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

        if (menuGame != null)
            menuGame.ShowGameOverPanel();
    }
}