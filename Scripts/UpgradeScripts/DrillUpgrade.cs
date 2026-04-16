using UnityEngine;
using TMPro;

public class DrillUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public DrillHealth drill;
    [SerializeField] public DrillController drillSpeeder;
    [SerializeField] public PlayerResources playerInventory;

    [Header("UI Тексты затрат")]
    [SerializeField] private TMP_Text speedCostText; // Текст для скорости (3 строки)
    [SerializeField] private TMP_Text hpCostText;    // Текст для HP (3 строки)
    [SerializeField] private TMP_Text regCostText;   // Текст для регена (3 строки)

    [Header("Улучшение подъема")]
    public float upgradeDrSpeedPerLevel = 1f;
    public int currSpeedLevel = 0;
    public int maxSpeedLevel = 5;

    [Header("Улучшение живучести")]
    public int upgradeHpPerLevel = 1;
    public int currHpLevel = 0;
    public int maxHpLevel = 5;

    [Header("Улучшение регенерации")]
    public int upgradeRegPerLevel = 1;
    public int currRegLevel = 0;
    public int maxRegLevel = 5;

    void Start()
    {
        // Инициализируем тексты при старте
        UpdateSpeedCostText();
        UpdateHpCostText();
        UpdateRegCostText();
    }

    public void BuyDrSpeedUpgrade()
    {
        if (currSpeedLevel >= maxSpeedLevel)
        {
            Debug.Log("Максимальный уровень ск. бура!");
            return;
        }        
        else
        {
            switch (currSpeedLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 2 || playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 2);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreVibroCount() < 1 || playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 1);
                    break;
                case 2:
                    if (playerInventory.GetOreCoalCount() < 8 || playerInventory.GetOreVibroCount() < 3 || playerInventory.GetOreTravCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }              
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 8);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 5);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 3);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 5 || playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 5);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 8 || playerInventory.GetOreTravCount() < 12)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 12);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 8);
                    break;
            }
        }

        float oldspeed = drillSpeeder.GetDrillSpeed();
        float newspeed = oldspeed + upgradeDrSpeedPerLevel;

        drillSpeeder.SetDrillSpeed(newspeed);
        currSpeedLevel++;

        // 🔥 Обновляем текст следующего уровня
        UpdateSpeedCostText();
        Debug.Log("Уровень скорости: "+ currSpeedLevel);
    }
    
    public void BuyHpUpgrade()
    {
        if (currHpLevel >= maxHpLevel)
        {
            Debug.Log("Максимальный уровень хп!");
            return;
        } 
        else
        {
            switch (currHpLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 3 || playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 3);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreVibroCount() < 1 || playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 1);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 3 || playerInventory.GetOreTravCount() < 6)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }             
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 6);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 3);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 6 || playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 6);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 10 || playerInventory.GetOreTravCount() < 10)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 10);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 10);
                    break;
            }
        }

        int oldhp = drill.GetMaxHp();
        int newhp = oldhp + upgradeHpPerLevel;

        drill.SetNewMaxHp(newhp);
        currHpLevel++;

        // 🔥 Обновляем текст следующего уровня
        UpdateHpCostText();
        Debug.Log("Уровень хп: "+ currHpLevel);
    }

    public void BuyRegUpgrade()
    {
        if (currRegLevel >= maxRegLevel)
        {
            Debug.Log("Максимальный уровень регенерации!");
            return;
        }       
        else
        {
            switch (currRegLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 2 || playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 2);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreVibroCount() < 2 || playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 2);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 5 || playerInventory.GetOreTravCount() < 7)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }             
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 7);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 5);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 7 || playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 7);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 9 || playerInventory.GetOreTravCount() < 9)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 9);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 9);
                    break;
            }
        }

        int oldreg = drill.GetRegen();
        int newreg = oldreg + upgradeRegPerLevel;

        drill.SetNewRegen(newreg);
        currRegLevel++;

        // 🔥 Обновляем текст следующего уровня
        UpdateRegCostText();
        Debug.Log("Уровень регена: "+ currRegLevel);
    }

    // ================= UI МЕТОДЫ ОБНОВЛЕНИЯ =================

    void UpdateSpeedCostText()
    {
        int coal = 0, trav = 0, vib = 0;

        switch (currSpeedLevel)
        {
            case 0: coal = 2; trav = 2; vib = 0; break;
            case 1: coal = 5; trav = 2; vib = 1; break;
            case 2: coal = 8; trav = 5; vib = 3; break;
            case 3: coal = 0; trav = 8; vib = 5; break;
            case 4: coal = 0; trav = 12; vib = 8; break;
            case 5: 
                if (speedCostText != null) speedCostText.text = "MAX\n\nMAX\n\nMAX"; 
                return;
        }

        if (speedCostText != null) speedCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }

    void UpdateHpCostText()
    {
        int coal = 0, trav = 0, vib = 0;

        switch (currHpLevel)
        {
            case 0: coal = 3; trav = 2; vib = 0; break;
            case 1: coal = 5; trav = 3; vib = 1; break;
            case 2: coal = 0; trav = 6; vib = 3; break;
            case 3: coal = 0; trav = 8; vib = 6; break;
            case 4: coal = 0; trav = 10; vib = 10; break;
            case 5: 
                if (hpCostText != null) hpCostText.text = "MAX\n\nMAX\n\nMAX"; 
                return;
        }

        if (hpCostText != null) hpCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }

    void UpdateRegCostText()
    {
        int coal = 0, trav = 0, vib = 0;

        switch (currRegLevel)
        {
            case 0: coal = 2; trav = 2; vib = 0; break;
            case 1: coal = 5; trav = 3; vib = 2; break;
            case 2: coal = 0; trav = 7; vib = 5; break;
            case 3: coal = 0; trav = 8; vib = 7; break;
            case 4: coal = 0; trav = 9; vib = 9; break;
            case 5: 
                if (regCostText != null) regCostText.text = "MAX\n\nMAX\n\nMAX"; 
                return;
        }

        if (regCostText != null) regCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }
}