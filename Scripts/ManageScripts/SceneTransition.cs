using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private TMP_Text loadText;
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private float exitAnimDuration = 0.5f; 

    private Animator animator;
    private static SceneTransition instance;
    private AsyncOperation asyncOp;
    private bool isTransitioning;
    private bool isHidingPanel;
    private float hideTimer;

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
        if (loadingPanel != null) loadingPanel.SetActive(false);
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public static void SwitchToScene(string sceneName)
    {
        if (instance == null) return;
        instance.StartTransition(sceneName);
    }

    private void StartTransition(string sceneName)
    {
        if (isTransitioning) return;
        isTransitioning = true;
        isHidingPanel = false;

        if (loadingPanel != null) loadingPanel.SetActive(true);
        if (animator != null) animator.SetTrigger("sceneStart");

        asyncOp = SceneManager.LoadSceneAsync(sceneName);
        asyncOp.allowSceneActivation = false;
    }

    void Update()
    {
        if (!isTransitioning || asyncOp == null) return;

        float progress = Mathf.Clamp01(asyncOp.progress / 0.9f);
        loadText.text = $"Загружено: {Mathf.RoundToInt(progress * 100)}%";

        if (isHidingPanel)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
            {
                if (loadingPanel != null) loadingPanel.SetActive(false);
                isHidingPanel = false;
                isTransitioning = false;
            }
        }
    }

    public void OnAnimationOver()
    {
        if (asyncOp != null && !asyncOp.isDone)
        {
            asyncOp.allowSceneActivation = true;
            Debug.Log("[SceneTransition] Scene activation allowed.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (animator != null)
        {
            animator.SetTrigger("sceneEnd");
            Debug.Log("[SceneTransition] Exit animation triggered.");
        }

        isHidingPanel = true;
        hideTimer = exitAnimDuration;
    }
}