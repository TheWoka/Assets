using UnityEngine;
using TMPro;

public class HeightChecker : MonoBehaviour
{
    [SerializeField] private Transform groundPoint;
    [SerializeField] private Transform drillPoint;  
    [Header("UI")]
    [SerializeField] private TMP_Text heightText;  
    
    [Header("Настройки")]
    [SerializeField] private int decimalPlaces = 2; 

    void Update()
    {
        if (groundPoint == null || drillPoint == null || heightText == null)
            return;

        float height = Mathf.Abs(drillPoint.position.y - (groundPoint.position.y + 4.51f));

        heightText.text = $"{height}м";
    }
}
