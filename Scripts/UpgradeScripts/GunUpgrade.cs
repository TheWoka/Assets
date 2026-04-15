using UnityEngine;

public class GunUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public TurretController turret;
    [SerializeField] public PlayerResources playerInventory;

    [Header("Улучшение скорострельности")]
    public float upgradeShPerLevel = 0.05f;
    public int currShLevel = 0;
    public int maxShLevel = 5;

    [Header("Улучшение урона")]
    public float upgradeDmPerLevel = 0.5f;
    public int currDmLevel = 0;
    public int maxDmLevel = 5;

    [Header("Улучшение перезарядки")]
    public float upgradeRePerLevel = 1f;
    public int currReLevel = 0;
    public int maxReLevel = 3;

    public void BuyShootUpgrade()
    {
        if (currShLevel >= maxShLevel)
        {
            Debug.Log("Максимальный уровень скорострельности!");
            return;
        }
        else
        {
            switch (currShLevel)
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
                    if (playerInventory.GetOreCoalCount() < 8 && playerInventory.GetOreVibroCount() < 4 && playerInventory.GetOreTravCount() < 6)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }             
                    playerInventory.SetOreCoalCount(8);
                    playerInventory.SetOreTravCount(6);
                    playerInventory.SetOreVibroCount(4);
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
                    if (playerInventory.GetOreVibroCount() < 8 && playerInventory.GetOreTravCount() < 10)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(10);
                    playerInventory.SetOreVibroCount(8);
                    break;
            }
        }

        float oldShootTime = turret.GetTimeBtwShoots();
        float newShootTime = oldShootTime - upgradeShPerLevel;

        turret.SetNewTimeBtwShoots(newShootTime);
        currShLevel++;

        Debug.Log("Ур скорострельности: "+ currShLevel);
    }

    public void BuyDamageUpgrade()
    {
        if (currDmLevel >= maxDmLevel)
        {
            Debug.Log("Максимальный уровень урона!");
            return;
        }
        else
        {
            switch (currDmLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 3 && playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(3);
                    playerInventory.SetOreTravCount(3);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 5 && playerInventory.GetOreVibroCount() < 2 && playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(3);
                    playerInventory.SetOreVibroCount(2);
                    break;
                case 2:
                    if (playerInventory.GetOreCoalCount() < 9 && playerInventory.GetOreVibroCount() < 3 && playerInventory.GetOreTravCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }
                    playerInventory.SetOreCoalCount(9);
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
                    if (playerInventory.GetOreVibroCount() < 10 && playerInventory.GetOreTravCount() < 12)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                  
                    playerInventory.SetOreTravCount(12);
                    playerInventory.SetOreVibroCount(10);
                    break;
            }
        }

        float oldDamage = turret.GetDamage();
        float newDamage = oldDamage + upgradeDmPerLevel;

        turret.SetNewDmg(newDamage);
        currDmLevel++;

        Debug.Log("Ур урона: " + currDmLevel);
        
    }

    public void BuyReloadUpgrade()
    {
        if (currReLevel >= maxReLevel)
        {
            Debug.Log("Максимальный уровень перезарядки!");
            return;
        }
        else
        {
            switch (currReLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 4 && playerInventory.GetOreTravCount() < 2)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(4);
                    playerInventory.SetOreTravCount(2);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 10 && playerInventory.GetOreVibroCount() < 5 && playerInventory.GetOreTravCount() < 8)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(10);
                    playerInventory.SetOreTravCount(8);
                    playerInventory.SetOreVibroCount(5);
                    break;
                case 2:
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

        float oldReload = turret.GetReload();
        float newReload = oldReload - upgradeRePerLevel;

        turret.SetNewReload(newReload);
        currReLevel++;

        Debug.Log("Ур урона: " + currReLevel);
    }
}
