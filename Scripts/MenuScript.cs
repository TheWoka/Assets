using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{

    public GameObject panelSettings, panelMenu;
    void Start()
    {
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
    public void Play()
    {
        SceneManager.LoadScene("Play");
        Debug.Log("Сцена игры");
    }

    // Для свапа на сцену меню
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Сцена меню");
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
