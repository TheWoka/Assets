using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public CharacterScript character;
    [SerializeField] public PlayerMining digSpeed;
    /* [SerializeField] public PlayerPickupScript playerInventory; */

    [Header("Улучшение скорости")]
    public float upgradeSpeedCharPerLevel = 0.5f;
    public int currCharSpeedLevel = 0;
    public int maxCharSpeedLevel = 3;

    [Header("Улучшение клопания")]
    public float upgradeDigPerLevel = 0.75f;
    public int currDigLevel = 0;
    public int maxDigLevel = 3;

    public void BuySpeedCharUpgrade()
    {
        if (currCharSpeedLevel >= maxCharSpeedLevel)
        {
            Debug.Log("Максимальный уровень скорости!");
            return;
        } 

        float oldspeed = character.GetSpeed();
        float newspeed = oldspeed + upgradeSpeedCharPerLevel;

        character.SetNewSpeed(newspeed);
        currCharSpeedLevel++;

        Debug.Log("Уровень скорости: "+ currCharSpeedLevel);
    }

    public void BuyDigUpgrade()
    {
        if (currDigLevel >= maxDigLevel)
        {
            Debug.Log("Максимальный уровень копания!");
            return;
        } 

        float olddig = digSpeed.GetOreDamage();
        float newdig = olddig + upgradeDigPerLevel;

        digSpeed.SetOreDamage(newdig);
        currDigLevel++;

        Debug.Log("Уровень ск. копания: "+ currDigLevel);
    }
}
