using UnityEngine;
using TMPro; 

public class PlayerResources : MonoBehaviour
{
    [Header("Счётчик руды")]
    [SerializeField] private int traviyCount = 0;
    [SerializeField] private int coalCount = 0;
    [SerializeField] private int vibraniumCount = 0;

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
            oreTraviyText.text = $"{traviyCount}";
        }
    }
    private void UpdateOreCoalUI()
    {
        if (oreCoalText != null)
        {
            oreCoalText.text = $"{coalCount}";
        }
    }
    private void UpdateOreVibroUI()
    {
        if (oreVibroText != null)
        {
            oreVibroText.text = $"{vibraniumCount}";
        }
    }
}
