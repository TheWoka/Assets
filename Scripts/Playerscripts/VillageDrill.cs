using UnityEngine;
using TMPro;

public class VillageDrill : MonoBehaviour
{
    public DrillController drill;
    public float enterDistance = 2f;

    [Header("Подсказка входа")]
    [SerializeField] private RectTransform promptText;
    [SerializeField] private Canvas uiCanvas;
    [SerializeField] private TMP_Text promptTextComponent;
    [SerializeField] private float followSmooth = 6f;
    [SerializeField] private float fadeSpeed = 5f;
    [SerializeField] private Vector2 textOffset = new Vector2(0, 60);
    [SerializeField] private GameObject lightChar;

    private float currentAlpha = 0f;
    private Color baseTextColor;

    void Start()
    {
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
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, drill.transform.position);

        UpdateEnterPrompt(distance);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Попытка входа в бур");
            TryEnter();
        }
    }

    void UpdateEnterPrompt(float distance)
    {
        if (promptText == null || uiCanvas == null || promptTextComponent == null) return;

        bool isNear = distance <= enterDistance;
        float targetAlpha = isNear ? 1f : 0f;

        currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

        Color textColor = baseTextColor;
        textColor.a = currentAlpha;
        promptTextComponent.color = textColor;

        if (currentAlpha > 0.01f)
            promptText.gameObject.SetActive(true);
        else
            promptText.gameObject.SetActive(false);

        // Позиционирование
        if (currentAlpha > 0.01f && drill != null)
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

    void TryEnter()
    {
        float distance = Vector2.Distance(transform.position, drill.transform.position);
        if (distance > enterDistance) return;

        promptText.gameObject.SetActive(false);
        promptTextComponent.gameObject.SetActive(false);
        lightChar.SetActive(false);

        Debug.Log("Запуск смены сцены");
        SceneTransition.SwitchToScene("Play");
    }

    // Дебаг отрисовка
    void OnDrawGizmosSelected()
    {
        if (drill == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(drill.transform.position, enterDistance);
    }
}