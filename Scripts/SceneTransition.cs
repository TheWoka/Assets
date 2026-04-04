using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private TMP_Text loadText;
    [SerializeField] private GameObject loadingPanel;
    private Animator animator;
    private static SceneTransition instance;
    private AsyncOperation loadingSceneOperation;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        animator = GetComponent<Animator>();
        loadingPanel.SetActive(false);
    }

    public static void SwitchToScene(string sceneName)
    {
        if (instance == null) return;
        instance.StartTransition(sceneName);
    }
    
    private void StartTransition(string sceneName)
    {
        if (loadingPanel != null)
            loadingPanel.SetActive(true);
        if (animator != null)
            animator.SetTrigger("sceneStart");
        
        loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName);
        loadingSceneOperation.allowSceneActivation = false;
    }

    void Update()
    {
        loadText.text = $"Загружено: {Mathf.RoundToInt(loadingSceneOperation.progress * 100)}%";
    }

    public void OnAnimationOver()
    {
        if (loadingSceneOperation != null) loadingSceneOperation.allowSceneActivation = true;
    }
    
    private void OnLevelWasLoaded(int level)
    {
        loadingPanel.SetActive(false);
    }
}
