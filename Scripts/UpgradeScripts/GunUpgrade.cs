using UnityEngine;
using TMPro;

public class GunUpgrade : MonoBehaviour
{
    [Header("Поля")]
    [SerializeField] public TurretController turret;
    [SerializeField] public PlayerResources playerInventory;

    [Header("UI Тексты затрат")]
    [SerializeField] private TMP_Text shootCostText;   // Скорострельность
    [SerializeField] private TMP_Text damageCostText;  // Урон
    [SerializeField] private TMP_Text reloadCostText;  // Перезарядка

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

    void Start()
    {
        // Инициализация текстов при запуске
        UpdateShootCostText();
        UpdateDamageCostText();
        UpdateReloadCostText();
    }

    public void BuyShootUpgrade()
    {
        if (currShLevel >= maxShLevel)
        {
            Debug.Log("Максимальный уровень скорострельности!");
            return;
        }

        switch (currShLevel)
        {
            case 0:
                if (playerInventory.GetOreCoalCount() < 3 || playerInventory.GetOreTravCount() < 2)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 3);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                break;
            case 1:
                if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreVibroCount() < 1 || playerInventory.GetOreTravCount() < 3)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 1);
                break;
            case 2:
                if (playerInventory.GetOreCoalCount() < 8 || playerInventory.GetOreVibroCount() < 4 || playerInventory.GetOreTravCount() < 6)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 8);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 6);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 4);
                break;
            case 3:
                if (playerInventory.GetOreVibroCount() < 5 || playerInventory.GetOreTravCount() < 8)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 5);
                break;
            case 4:
                if (playerInventory.GetOreVibroCount() < 8 || playerInventory.GetOreTravCount() < 10)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 10);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 8);
                break;
        }

        float oldShootTime = turret.GetTimeBtwShoots();
        float newShootTime = oldShootTime - upgradeShPerLevel;
        turret.SetNewTimeBtwShoots(newShootTime);
        currShLevel++;

        UpdateShootCostText();
        Debug.Log("Ур скорострельности: " + currShLevel);
    }

    public void BuyDamageUpgrade()
    {
        if (currDmLevel >= maxDmLevel)
        {
            Debug.Log("Максимальный уровень урона!");
            return;
        }

        switch (currDmLevel)
        {
            case 0:
                if (playerInventory.GetOreCoalCount() < 3 || playerInventory.GetOreTravCount() < 3)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 3);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                break;
            case 1:
                if (playerInventory.GetOreCoalCount() < 5 || playerInventory.GetOreVibroCount() < 2 || playerInventory.GetOreTravCount() < 3)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 5);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 3);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 2);
                break;
            case 2:
                if (playerInventory.GetOreCoalCount() < 9 || playerInventory.GetOreVibroCount() < 3 || playerInventory.GetOreTravCount() < 5)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 9);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 5);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 3);
                break;
            case 3:
                if (playerInventory.GetOreVibroCount() < 5 || playerInventory.GetOreTravCount() < 8)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 5);
                break;
            case 4:
                if (playerInventory.GetOreVibroCount() < 10 || playerInventory.GetOreTravCount() < 12)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 12);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 10);
                break;
        }

        float oldDamage = turret.GetDamage();
        float newDamage = oldDamage + upgradeDmPerLevel;
        turret.SetNewDmg(newDamage);
        currDmLevel++;

        UpdateDamageCostText();
        Debug.Log("Ур урона: " + currDmLevel);
    }

    public void BuyReloadUpgrade()
    {
        if (currReLevel >= maxReLevel)
        {
            Debug.Log("Максимальный уровень перезарядки!");
            return;
        }

        switch (currReLevel)
        {
            case 0:
                if (playerInventory.GetOreCoalCount() < 4 || playerInventory.GetOreTravCount() < 2)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 4);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 2);
                break;
            case 1:
                if (playerInventory.GetOreCoalCount() < 10 || playerInventory.GetOreVibroCount() < 5 || playerInventory.GetOreTravCount() < 8)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreCoalCount(playerInventory.GetOreCoalCount() - 10);
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 8);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 5);
                break;
            case 2:
                if (playerInventory.GetOreVibroCount() < 10 || playerInventory.GetOreTravCount() < 10)
                { Debug.Log("Недостаточно руды!"); return; }
                playerInventory.SetOreTravCount(playerInventory.GetOreTravCount() - 10);
                playerInventory.SetOreVibroCount(playerInventory.GetOreVibroCount() - 10);
                break;
        }

        float oldReload = turret.GetReload();
        float newReload = oldReload - upgradeRePerLevel;
        turret.SetNewReload(newReload);
        currReLevel++;

        UpdateReloadCostText();
        Debug.Log("Ур перезарядки: " + currReLevel); // 🔧 Исправил опечатку в логе
    }

    // ================= UI МЕТОДЫ ОБНОВЛЕНИЯ =================

    void UpdateShootCostText()
    {
        int coal = 0, trav = 0, vib = 0;
        switch (currShLevel)
        {
            case 0: coal = 3; trav = 2; vib = 0; break;
            case 1: coal = 5; trav = 3; vib = 1; break;
            case 2: coal = 8; trav = 6; vib = 4; break;
            case 3: coal = 0; trav = 8; vib = 5; break;
            case 4: coal = 0; trav = 10; vib = 8; break;
            case 5: if (shootCostText != null) shootCostText.text = "MAX\n\nMAX\n\nMAX"; return;
        }
        if (shootCostText != null) shootCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }

    void UpdateDamageCostText()
    {
        int coal = 0, trav = 0, vib = 0;
        switch (currDmLevel)
        {
            case 0: coal = 3; trav = 3; vib = 0; break;
            case 1: coal = 5; trav = 3; vib = 2; break;
            case 2: coal = 9; trav = 5; vib = 3; break;
            case 3: coal = 0; trav = 8; vib = 5; break;
            case 4: coal = 0; trav = 12; vib = 10; break;
            case 5: if (damageCostText != null) damageCostText.text = "MAX\n\nMAX\n\nMAX"; return;
        }
        if (damageCostText != null) damageCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }

    void UpdateReloadCostText()
    {
        int coal = 0, trav = 0, vib = 0;
        switch (currReLevel)
        {
            case 0: coal = 4; trav = 2; vib = 0; break;
            case 1: coal = 10; trav = 8; vib = 5; break;
            case 2: coal = 0; trav = 10; vib = 10; break;
            case 3: if (reloadCostText != null) reloadCostText.text = "MAX\n\nMAX\n\nMAX"; return;
        }
        if (reloadCostText != null) reloadCostText.text = $"{coal}\n\n{trav}\n\n{vib}";
    }
}