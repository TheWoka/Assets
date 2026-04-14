using UnityEngine;

public class GunUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public TurretController turret;
    /* [SerializeField] public PlayerPickupScript playerInventory; */

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

        float oldReload = turret.GetReload();
        float newReload = oldReload - upgradeRePerLevel;

        turret.SetNewReload(newReload);
        currReLevel++;

        Debug.Log("Ур урона: " + currReLevel);
    }
}
