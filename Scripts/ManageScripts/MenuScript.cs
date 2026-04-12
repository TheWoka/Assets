using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private Vector2 hotSpot = Vector2.zero;
    [SerializeField] private Texture2D cursorTexture;
    public GameObject panelSettings, panelMenu;
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        if (panelSettings != null)
            panelSettings.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Закрытие настроек на эскейп
            if (panelSettings.activeSelf == true) {
                panelSettings.SetActive(false); 
                panelMenu.SetActive(true); 
            }
        }
    }

    // Для запуска сцены игры
    public void Village()
    {
        SceneTransition.SwitchToScene("Village");
    }

    // Для свапа на сцену меню
    public void Menu()
    {
        SceneTransition.SwitchToScene("Menu");
    }

    // Для открытия кнопок настроек и скрытия панели стандартной
    public void Settings()
    {
        if (panelSettings.activeSelf == false)
        {
            panelSettings.SetActive(true);
            if (panelMenu != null) panelMenu.SetActive(false); 
            Debug.Log("Открыл настройки");
        }
        else if (panelSettings.activeSelf == true)
        {
            panelSettings.SetActive(false);
            if (panelMenu != null) panelMenu.SetActive(true); 
            Debug.Log("Закрыл настройки");
        }
    }

    // Для выхода 
    public void Exit()
    {
        Application.Quit();
    } 
}
