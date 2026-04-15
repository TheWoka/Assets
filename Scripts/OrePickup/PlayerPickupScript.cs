using UnityEngine;
using TMPro; 

public class PlayerResources : MonoBehaviour
{
    [Header("Счётчик руды")]
    [SerializeField] private int coalCount = 0;
    [SerializeField] private int traviyCount = 0;
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
        UpdateOreTravUI();
    }
    public void AddCoalOre(int amount)
    {
        coalCount += amount;
        UpdateOreCoalUI();
    }
    public void AddVibroOre(int amount)
    {
        vibraniumCount += amount;
        UpdateOreVibroUI();
    }
    
    // ГЕТТЕРЫ
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

    // СЕТТЕРЫ 
    public void SetOreTravCount(int spendOre)
    {
        if (traviyCount - spendOre < 0) traviyCount = 0;
        else traviyCount -= spendOre;
        UpdateOreTravUI();
    }
    public void SetOreCoalCount(int spendOre)
    {
        if (coalCount - spendOre < 0) coalCount = 0;
        else coalCount -= spendOre;
        UpdateOreCoalUI();
    }
    public void SetOreVibroCount(int spendOre)
    {
        if (vibraniumCount - spendOre < 0) vibraniumCount = 0;
        else vibraniumCount -= spendOre;
        UpdateOreVibroUI();
    }
    

    // ОБНОВЛЕНИЕ UI
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
