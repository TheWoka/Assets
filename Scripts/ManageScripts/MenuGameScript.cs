using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameScript : MonoBehaviour
{
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private EnterDrill enterDrill;

    
    [Header("Интерфейс")]
    public GameObject panelGameMenu;
    public GameObject panelGameSettings;
    public GameObject gameOverPanel;
    public GameObject choosePanel;

    [Header("Улучшения")]
    public GameObject choosenPanel;
    public GameObject drillPanel;
    public GameObject gunPanel;
    public GameObject characterPanel;
    public GunUpgrade gunUpgradeScript;
    public DrillUpgrade drillUpgradeScript;
    public CharacterUpgrade charUpgradeScript;

    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        panelGameSettings.SetActive(false);
        panelGameMenu.SetActive(false);
    }

    // Реакция на эскейп
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelGameSettings.activeSelf == false)
            {
                if (characterPanel.activeSelf == true)
                {
                    characterPanel.SetActive(false);
                }

                else if (drillPanel.activeSelf == true)
                {
                    drillPanel.SetActive(false);
                }

                else if (gunPanel.activeSelf == true)
                {
                    gunPanel.SetActive(false);
                }

                else if (choosenPanel.activeSelf == true)
                {
                    choosenPanel.SetActive(false);
                    choosePanel.SetActive(true);
                }

                else if (choosePanel.activeSelf == true)
                {
                    choosePanel.SetActive(false);
                }

                else 
                    Continue();
                    
            }
            else if (panelGameSettings.activeSelf == false) Continue();

            // Закрытие настроек на эскейп
            if (panelGameSettings.activeSelf == true) {
                panelGameSettings.SetActive(false);
                panelGameMenu.SetActive(true);
            }
        }
    }

    // Для закрытия меню в игре
    public void Continue()
    {
        if (panelGameMenu.activeSelf == false)
        {
            panelGameMenu.SetActive(true);
        }
        else if (panelGameMenu.activeSelf == true)
        {
            panelGameMenu.SetActive(false);
        }
    }

    // Переход в игру
    public void Game()
    {
        SceneTransition.SwitchToScene("Play");
    }

    // В меню
    public void Menu()
    {
        SceneTransition.SwitchToScene("Menu");
    }

    // При выборе апргрейда активировать там короче выбор улучшений
    public void UpgradeDrill()
    {
        if (choosePanel.activeSelf == true) choosePanel.SetActive(false);
        Debug.Log("Выбор улучшения");

        choosenPanel.SetActive(true);
    }


    // Панель улучшения бура
    public void drillUpMenu()
    {
        drillPanel.SetActive(true);
        gunPanel.SetActive(false);
        characterPanel.SetActive(false);
    }
    // Покупка скрипта улучшения БУРА
        public void BuySpeedUp()
        {
            drillUpgradeScript.BuyDrSpeedUpgrade();
        }
        public void BuyHpUp()
        {
            drillUpgradeScript.BuyHpUpgrade();
        }
        public void BuyRegenUp()
        {
            drillUpgradeScript.BuyRegUpgrade();
        }

    public void gunUpMenu()
    {
        drillPanel.SetActive(false);
        gunPanel.SetActive(true);
        characterPanel.SetActive(false);
    }

        // Покупка скрипта улучшения ПУШКИ
        public void BuyShootUp()
        {
            gunUpgradeScript.BuyShootUpgrade();
        }
        public void BuyDamageUp()
        {
            gunUpgradeScript.BuyDamageUpgrade();
        }
        public void BuyReloadUp()
        {
            gunUpgradeScript.BuyReloadUpgrade();
        }

    public void characterUpMenu()
    {
        drillPanel.SetActive(false);
        gunPanel.SetActive(false);
        characterPanel.SetActive(true); 
    }

    // Покупка скрипта улучшения ПЕРСОНАЖА
        public void BuySpeedCharUp()
        {
            charUpgradeScript.BuySpeedCharUpgrade();
        }
        public void BuyDigUp()
        {
            charUpgradeScript.BuyDigUpgrade();
        }

    // При выборе входа снова там активировать вход
    public void EnterDrill()
    {
        enterDrill.Enter(); 
    }

    // Для открытия кнопок настроек и скрытия панели стандартной
    public void Settings()
    {
        if (panelGameSettings.activeSelf == false)
        {
            panelGameSettings.SetActive(true);
            panelGameMenu.SetActive(false); 
            Debug.Log("Открыл настройки");
        }
        else if (panelGameSettings.activeSelf == true)
        {
            panelGameSettings.SetActive(false);
            panelGameMenu.SetActive(true); 
            Debug.Log("Закрыл настройки");
        }
    }

    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            panelGameMenu.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }

    // Для выхода 
    public void Exit()
    {
        Application.Quit();
    } 
}