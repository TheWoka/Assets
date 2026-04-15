using UnityEngine;
using TMPro;
using System.Collections;

public class EnterDrill : MonoBehaviour
{
    [Header("Управления момент")]
    public DrillController drill;
    public float enterDistance = 2f;
    public CameraFollow cameraFollow;
    public bool IsInside => isInside;
    public GameObject inventoryInter;
    
    [Header("Турель")]
    [SerializeField] private TurretController turretController;

    [Header("Кулдаун бура")]
    [SerializeField] private float reenterCooldown = 40f;
    
    [Header("UI Кулдауна")]
    [SerializeField] private RectTransform cooldownPanel; 
    [SerializeField] private TMP_Text cooldownText; 
    [SerializeField] private float animSpeed = 5f;

    [Header("Приколы с UI")]
    [SerializeField] private GameObject choosePanel;
    [SerializeField] private GameObject menuButton;

    [Header("Подсказка входа")]
    [SerializeField] private RectTransform promptText;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private float followSmooth = 3f; 
    [SerializeField] private Vector2 textOffset = new Vector2(0, 30); 
    [SerializeField] private TMP_Text promptTextComponent;
    [SerializeField] private float fadeSpeed = 5f;

    private bool isInside = false;
    private float currentAlpha = 0f;
    private Color baseTextColor;
    private bool canEnterDrill = true;
    private bool startInsideDrill = true;
    private Coroutine cooldownRoutine;
    private CharacterScript playerScript;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;
    
    private Color originalTextColor; 

    void Start()
    {
        playerScript = GetComponent<CharacterScript>();
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        drill.OnReachedStop += HandleReachedStop;

        if (cooldownText != null)
        {
            originalTextColor = cooldownText.color;
            Color transparent = originalTextColor;
            transparent.a = 0f;
            cooldownText.color = transparent;
        }

        if (cooldownPanel != null)
            cooldownPanel.gameObject.SetActive(false);

        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
            if (promptTextComponent == null) 
                promptTextComponent = promptText.GetComponent<TMP_Text>();
            
            if (promptTextComponent != null)
            {
                baseTextColor = promptTextComponent.color;
                Color transparent = baseTextColor;
                transparent.a = 0f;
                promptTextComponent.color = transparent;
            }
        }

        if (startInsideDrill)
        {
            Enter();
            startInsideDrill = false;
        }
    }

    void OnDestroy()
    {
        if (drill != null)
            drill.OnReachedStop -= HandleReachedStop;
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, drill.transform.position);
        if (Input.GetKeyDown(KeyCode.E) && !isInside)
        {
            ChooseEnter();
        }
        UpdateEnterPrompt();
    }

    void ChooseEnter()
    {
        Debug.Log("Попытка войти");
        if (!canEnterDrill) return;

        float distance = Vector2.Distance(transform.position, drill.transform.position);
        if (distance > enterDistance) return;
        if (!drill.HasMoreStops) return;

        if (choosePanel.activeSelf == false) 
        {
            choosePanel.SetActive(true);
            menuButton.SetActive(false);
            Debug.Log("Активация панели");
        }
    }

    public void Enter()
    {
        isInside = true;
        choosePanel.SetActive(false);
        inventoryInter.SetActive(false);

        playerScript.enabled = false;
        playerRb.linearVelocity = Vector2.zero;
        playerRb.simulated = false;
        playerSprite.enabled = false;
        transform.position = drill.transform.position;
        cameraFollow.target = drill.transform;
        cameraFollow.SetZoom(true);

        if (turretController != null)
            turretController.SetControl(true);

        Debug.Log("Запуск бура");
        drill.StartRide();
    }

    void HandleReachedStop()
    {
        if (!isInside) return;
        ForceExitAtStop();
    }

    void ForceExitAtStop()
    {
        isInside = false;
        if (turretController != null)
            turretController.SetControl(false);

        playerScript.enabled = true;
        playerRb.simulated = true;
        playerSprite.enabled = true;
        inventoryInter.SetActive(true);

        transform.position = drill.transform.position + Vector3.right * 1.5f;

        cameraFollow.target = transform;
        cameraFollow.SetZoom(false);
        Debug.Log("Сбросил камеру в false");

        StartReenterCooldown();
    }

    void StartReenterCooldown()
    {
        canEnterDrill = false;

        if (cooldownRoutine != null)
            StopCoroutine(cooldownRoutine);

        cooldownRoutine = StartCoroutine(ReenterCooldownRoutine());
    }

    IEnumerator ReenterCooldownRoutine()
    {
        float timer = reenterCooldown;

        if (cooldownPanel != null)
            cooldownPanel.gameObject.SetActive(true);
        
        yield return StartCoroutine(AnimateCooldownText(show: true));

        while (timer > 0f)
        {
            if (cooldownText != null)
                cooldownText.text = $"Бур перегрет: {Mathf.CeilToInt(timer)} сек";

            timer -= Time.deltaTime;
            yield return null;
        }

        yield return StartCoroutine(AnimateCooldownText(show: false));
        
        canEnterDrill = true;
    }

    IEnumerator AnimateCooldownText(bool show)
    {
        float duration = 0.3f;
        float elapsed = 0f;
        
        float startAlpha = show ? 0f : 1f;
        float targetAlpha = show ? 1f : 0f;
        
        Vector2 startPos = cooldownPanel != null ? cooldownPanel.anchoredPosition : Vector2.zero;
        Vector2 endPos = show ? startPos + new Vector2(0, 8f) : startPos + new Vector2(0, -8f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float easedT = 1f - (1f - t) * (1f - t);

            if (cooldownText != null)
            {
                Color c = originalTextColor;
                c.a = Mathf.Lerp(startAlpha, targetAlpha, easedT);
                cooldownText.color = c;
            }

            if (cooldownPanel != null)
            {
                cooldownPanel.anchoredPosition = Vector2.Lerp(startPos, endPos, easedT);
            }

            yield return null;
        }

        if (cooldownText != null)
        {
            Color c = originalTextColor;
            c.a = targetAlpha;
            cooldownText.color = c;
        }

        if (!show && cooldownPanel != null)
            cooldownPanel.gameObject.SetActive(false);
    }

    void UpdateEnterPrompt()
    {
        if (promptText == null || uiCanvas == null || promptTextComponent == null) return;

        bool canShow = !isInside && canEnterDrill && 
                    Vector2.Distance(transform.position, drill.transform.position) <= enterDistance;

        float targetAlpha = canShow ? 1f : 0f;

        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

        Color textColor = baseTextColor;
        textColor.a = currentAlpha;
        promptTextComponent.color = textColor;

        if (currentAlpha > 0.01f)
            promptText.gameObject.SetActive(true);
        else
            promptText.gameObject.SetActive(false);

        if (currentAlpha > 0.01f)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                uiCanvas.transform as RectTransform,
                screenPos,
                Camera.main,
                out Vector2 localPos))
            {
                localPos += textOffset;
                promptText.anchoredPosition = Vector2.Lerp(
                    promptText.anchoredPosition, 
                    localPos, 
                    followSmooth * Time.deltaTime
                );
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (drill == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(drill.transform.position, enterDistance);
    }
}