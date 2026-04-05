using UnityEngine;
using TMPro; 

public class PlayerResources : MonoBehaviour
{
    [Header("Счётчик руды")]
    [SerializeField] private int traviyCount = 0, coalCount = 0, vibraniumCount = 0;

    [SerializeField] private TextMeshProUGUI oreTraviyText, oreCoalText, oreVibroText; 
    
    void Start()
    {
        UpdateOreTravUI();
        UpdateOreCoalUI();
        UpdateOreVibroUI();
    }
    
    public void AddTraviyOre(int amount)
    {
        traviyCount += amount;
        Debug.Log($"Руда подобрана! Всего: {traviyCount}");
        UpdateOreTravUI();
    }
    public void AddCoalOre(int amount)
    {
        coalCount += amount;
        Debug.Log($"Руда подобрана! Всего: {coalCount}");
        UpdateOreCoalUI();
    }
    public void AddVibroOre(int amount)
    {
        vibraniumCount += amount;
        Debug.Log($"Руда подобрана! Всего: {vibraniumCount}");
        UpdateOreVibroUI();
    }
    
    public int GetOreTravCount()
    {
        return traviyCount;
    }
    public int GetOreCoalCount()
    {
        return coalCount;
    }
    public int GetOreVibroCount()
    {
        return vibraniumCount;
    }
    
    private void UpdateOreTravUI()
    {
        if (oreTraviyText != null)
        {
            oreTraviyText.text = $"Травия: {traviyCount}";
        }
    }
    private void UpdateOreCoalUI()
    {
        if (oreCoalText != null)
        {
            oreCoalText.text = $"Угля: {coalCount}";
        }
    }
    private void UpdateOreVibroUI()
    {
        if (oreVibroText != null)
        {
            oreVibroText.text = $"Вибраниума: {vibraniumCount}";
        }
    }
}
