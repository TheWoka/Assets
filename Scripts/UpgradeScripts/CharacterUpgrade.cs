using UnityEngine;

public class CharacterUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public CharacterScript character;
    [SerializeField] public PlayerMining digSpeed;
    [SerializeField] public PlayerResources playerInventory;

    [Header("Улучшение скорости")]
    public float upgradeSpeedCharPerLevel = 0.5f;
    public int currCharSpeedLevel = 0;
    public int maxCharSpeedLevel = 3;

    [Header("Улучшение клопания")]
    public float upgradeDigPerLevel = 0.75f;
    public int currDigLevel = 0;
    public int maxDigLevel = 3;

    private int spendCoal, spendTraviy, spendVibro;

    public void BuySpeedCharUpgrade()
    {
        if (currCharSpeedLevel >= maxCharSpeedLevel)
        {
            Debug.Log("Максимальный уровень скорости!");
            return;
        }
        else
        {
            switch (currCharSpeedLevel)
            {
                case 0:
                    if (playerInventory.GetOreTravCount() < 3 && playerInventory.GetOreCoalCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(3);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 9 && playerInventory.GetOreVibroCount() < 4 && playerInventory.GetOreTravCount() < 7)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(9);
                    playerInventory.SetOreTravCount(7);
                    playerInventory.SetOreVibroCount(4);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 9 && playerInventory.GetOreTravCount() < 12)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                
                    playerInventory.SetOreTravCount(12);
                    playerInventory.SetOreVibroCount(9);
                    break;
            }
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
        else
        {
            switch (currDigLevel)
            {
                case 0:
                    if (playerInventory.GetOreCoalCount() < 5 && playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }    
                    playerInventory.SetOreCoalCount(5);
                    playerInventory.SetOreTravCount(3);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 7 && playerInventory.GetOreVibroCount() < 3 && playerInventory.GetOreTravCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }    
                    playerInventory.SetOreCoalCount(7);
                    playerInventory.SetOreTravCount(5);
                    playerInventory.SetOreVibroCount(3);
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

        float olddig = digSpeed.GetOreDamage();
        float newdig = olddig + upgradeDigPerLevel;

        digSpeed.SetOreDamage(newdig);
        currDigLevel++;

        Debug.Log("Уровень ск. копания: "+ currDigLevel);
    }
}
