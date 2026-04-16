using UnityEngine;
using TMPro;

public class CharacterUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public CharacterScript character;
    [SerializeField] public PlayerMining digSpeed;
    [SerializeField] public PlayerResources playerInventory;

    [Header("UI Тексты затрат")]
    [SerializeField] private TMP_Text speedCostText;    // Один текст для скорости (3 строки)
    [SerializeField] private TMP_Text digCostText;      // Один текст для копания (3 строки)

    [Header("Улучшение скорости")]
    public float upgradeSpeedCharPerLevel = 0.5f;
    public int currCharSpeedLevel = 0;
    public int maxCharSpeedLevel = 3;

    [Header("Улучшение клопания")]
    public float upgradeDigPerLevel = 0.75f;
    public int currDigLevel = 0;
    public int maxDigLevel = 3;

    void Start()
    {
        // Обновляем тексты при старте
        UpdateSpeedCostText();
        UpdateDigCostText();
    }

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
                    if (playerInventory.GetOreTravCount() < 3 || playerInventory.GetOreCoalCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 9 || playerInventory.GetOreVibroCount() < 4 || playerInventory.GetOreTravCount() < 7)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    } 
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 9);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 7);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 4);
                    break;
                case 2:
                    if (playerInventory.GetOreVibroCount() < 9 || playerInventory.GetOreTravCount() < 12)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }                
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 12);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 9);
                    break;
            }
        }

        float oldspeed = character.GetSpeed();
        float newspeed = oldspeed + upgradeSpeedCharPerLevel;

        character.SetNewSpeed(newspeed);
        currCharSpeedLevel++;

        // 🔥 ОБНОВЛЯЕМ ТЕКСТ ПОСЛЕ ПОКУПКИ
        UpdateSpeedCostText();

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
                    if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreTravCount() < 3)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }    
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                    break;
                case 1:
                    if (playerInventory.GetOreCoalCount() < 7 || playerInventory.GetOreVibroCount() < 3 || playerInventory.GetOreTravCount() < 5)
                    {
                        Debug.Log("Недостаточно руды!");
                        return;
                    }    
                    playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 7);
                    playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 5);
                    playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 3);
                    break;
                case 2:
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

        float olddig = digSpeed.GetOreDamage();
        float newdig = olddig + upgradeDigPerLevel;

        digSpeed.SetOreDamage(newdig);
        currDigLevel++;

        // 🔥 ОБНОВЛЯЕМ ТЕКСТ ПОСЛЕ ПОКУПКИ
        UpdateDigCostText();

        Debug.Log("Уровень ск. копания: "+ currDigLevel);
    }

    // 📝 Метод обновления текста затрат на скорость (3 строки в одном тексте)
    void UpdateSpeedCostText()
    {
        int coal = 0, travium = 0, vibranium = 0;

        switch (currCharSpeedLevel)
        {
            case 0:
                coal = 5;
                travium = 3;
                vibranium = 0;
                break;
            case 1:
                coal = 9;
                travium = 7;
                vibranium = 4;
                break;
            case 2:
                coal = 0;
                travium = 12;
                vibranium = 9;
                break;
            case 3:
                // Максимальный уровень
                if (speedCostText != null)
                    speedCostText.text = "MAX\n\nMAX\n\nMAX";
                return;
        }

        // Формируем текст с переносами строк
        if (speedCostText != null)
            speedCostText.text = $"{coal}\n\n{travium}\n\n{vibranium}";
    }

    // 📝 Метод обновления текста затрат на копание (3 строки в одном тексте)
    void UpdateDigCostText()
    {
        int coal = 0, travium = 0, vibranium = 0;

        switch (currDigLevel)
        {
            case 0:
                coal = 5;
                travium = 3;
                vibranium = 0;
                break;
            case 1:
                coal = 7;
                travium = 5;
                vibranium = 3;
                break;
            case 2:
                coal = 0;
                travium = 10;
                vibranium = 10;
                break;
            case 3:
                // Максимальный уровень
                if (digCostText != null)
                    digCostText.text = "MAX\n\nMAX\n\nMAX";
                return;
        }

        // Формируем текст с переносами строк
        if (digCostText != null)
            digCostText.text = $"{coal}\n\n{travium}\n\n{vibranium}";
    }
}