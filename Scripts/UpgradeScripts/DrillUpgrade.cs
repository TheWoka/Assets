using UnityEngine;

public class DrillUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public DrillHealth drill;
    [SerializeField] public DrillController drillSpeeder;
    /* [SerializeField] public PlayerPickupScript playerInventory; */

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

        int oldreg = drill.GetRegen();
        int newreg = oldreg + upgradeRegPerLevel;

        drill.SetNewRegen(newreg);
        currRegLevel++;

        Debug.Log("Уровень регена: "+ currRegLevel);
    }
}
