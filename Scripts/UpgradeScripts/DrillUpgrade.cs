using UnityEngine;

public class DrillUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public DrillHealth drill;
    [SerializeField] public DrillController drillSpeeder;
    [SerializeField] public PlayerResources playerInventory;

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
                    if (playerInventory.GetOreCoalCount() < 2 && playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(2);
                    playerInventory.SetOreTravCount(2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 && playerInventory.GetOreVibroCount() < 1 && playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(2);
                    playerInventory.SetOreVibroCount(1);
                    break;
                case 2:
                    if (playerInventory.GetOreCoalCount() < 8 && playerInventory.GetOreVibroCount() < 3 && playerInventory.GetOreTravCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }              
                    playerInventory.SetOreCoalCount(8);
                    playerInventory.SetOreTravCount(5);
                    playerInventory.SetOreVibroCount(3);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 5 && playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(8);
                    playerInventory.SetOreVibroCount(5);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 8 && playerInventory.GetOreTravCount() < 12)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(12);
                    playerInventory.SetOreVibroCount(8);
                    break;
            }
        }

        float oldspeed = drillSpeeder.GetDrillSpeed();
        float newspeed = oldspeed + upgradeDrSpeedPerLevel;

        drillSpeeder.SetDrillSpeed(newspeed);
        currSpeedLevel++;

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
                    if (playerInventory.GetOreCoalCount() < 3 && playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(3);
                    playerInventory.SetOreTravCount(2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 && playerInventory.GetOreVibroCount() < 1 && playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(3);
                    playerInventory.SetOreVibroCount(1);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 3 && playerInventory.GetOreTravCount() < 6)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }             
                    playerInventory.SetOreTravCount(6);
                    playerInventory.SetOreVibroCount(3);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 6 && playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(8);
                    playerInventory.SetOreVibroCount(6);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 10 && playerInventory.GetOreTravCount() < 10)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(10);
                    playerInventory.SetOreVibroCount(10);
                    break;
            }
        }

        int oldhp = drill.GetMaxHp();
        int newhp = oldhp + upgradeHpPerLevel;

        drill.SetNewMaxHp(newhp);
        currHpLevel++;

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
                    if (playerInventory.GetOreCoalCount() < 2 && playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(2);
                    playerInventory.SetOreTravCount(2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 &&  playerInventory.GetOreVibroCount() < 2 && playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(3);
                    playerInventory.SetOreVibroCount(2);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 5 && playerInventory.GetOreTravCount() < 7)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }             
                    playerInventory.SetOreTravCount(7);
                    playerInventory.SetOreVibroCount(5);
                    break;
                case 3:
                    if (playerInventory.GetOreVibroCount() < 7 && playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreTravCount(8);
                    playerInventory.SetOreVibroCount(7);
                    break;        
                case 4:
                    if (playerInventory.GetOreVibroCount() < 9 && playerInventory.GetOreTravCount() < 9)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(9);
                    playerInventory.SetOreVibroCount(9);
                    break;
            }
        }

        int oldreg = drill.GetRegen();
        int newreg = oldreg + upgradeRegPerLevel;

        drill.SetNewRegen(newreg);
        currRegLevel++;

        Debug.Log("Уровень регена: "+ currRegLevel);
    }
}
