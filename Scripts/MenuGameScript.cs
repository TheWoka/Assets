using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameScript : MonoBehaviour
{
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private Texture2D cursorTexture;
    
    public GameObject panelGameMenu, panelGameSettings, menuButton;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        if (panelGameSettings != null)
            panelGameSettings.SetActive(false);
        if (panelGameMenu != null)
            panelGameMenu.SetActive(false);
    }

    // Реакция на эскейп
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Открытие и закрытие самого меню
            if (panelGameSettings.activeSelf == false) Continue();
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
            menuButton.SetActive(false);
        }
        else if (panelGameMenu.activeSelf == true)
        {
            panelGameMenu.SetActive(false);
            menuButton.SetActive(true);
        }
    }

    // Для свапа на сцену меню
    public void Menu()
    {
        SceneTransition.SwitchToScene("Menu");
        Debug.Log("Сцена меню");
    }

    // Для открытия кнопок настроек и скрытия панели стандартной
    public void Settings()
    {
        if (panelGameSettings.activeSelf == false)
        {
            panelGameSettings.SetActive(true);
            if (panelGameMenu != null) panelGameMenu.SetActive(false); 
            Debug.Log("Открыл настройки");
        }
        else if (panelGameSettings.activeSelf == true)
        {
            panelGameSettings.SetActive(false);
            if (panelGameMenu != null) panelGameMenu.SetActive(true); 
            Debug.Log("Закрыл настройки");
        }
    }

    // Для выхода 
    public void Exit()
    {
        Application.Quit();
    } 
}
