using UnityEngine;
using TMPro; 

public class PlayerResources : MonoBehaviour
{
    [Header("Счётчик руды")]
    [SerializeField] private int coalCount = 0;
    [SerializeField] private int traviyCount = 0;
    [SerializeField] private int vibraniumCount = 0;

    [SerializeField] private TextMeshProUGUI oreTraviyText, oreCoalText, oreVibroText; 
    [SerializeField] private TextMeshProUGUI oreTraviyTextMenu, oreCoalTextMenu, oreVibroTextMenu; 

    
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
    public void SetOreTravCount(int value)
    {
        traviyCount = Mathf.Max(0, value);
        UpdateOreTravUI();
    }
    public void SetOreCoalCount(int value)
    {
        coalCount = Mathf.Max(0, value);
        UpdateOreCoalUI();
    }
    public void SetOreVibroCount(int value)
    {
        vibraniumCount = Mathf.Max(0, value);
        UpdateOreVibroUI();
    }
    

    // ОБНОВЛЕНИЕ UI
    private void UpdateOreTravUI()
    {
        if (oreTraviyText != null)
        {
            oreTraviyText.text = $"{traviyCount}";
            oreTraviyTextMenu.text = $"{traviyCount}";
        }
    }
    private void UpdateOreCoalUI()
    {
        if (oreCoalText != null)
        {
            oreCoalText.text = $"{coalCount}";
            oreCoalTextMenu.text = $"{coalCount}";
        }
    }
    private void UpdateOreVibroUI()
    {
        if (oreVibroText != null)
        {
            oreVibroText.text = $"{vibraniumCount}";
            oreVibroTextMenu.text = $"{vibraniumCount}";
        }
    }
}
