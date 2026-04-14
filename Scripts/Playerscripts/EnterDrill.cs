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
    
    [Header("Турель")]
    [SerializeField] private TurretController turretController;
    [SerializeField] private TMP_Text cooldownText;

    [Header("Кулдаун бура")]
    [SerializeField] private float reenterCooldown = 40f;

    [Header("Приколы с UI")]
    [SerializeField] private GameObject choosePanel;
    [SerializeField] private GameObject menuButton;

    private bool isInside = false;
    private bool canEnterDrill = true;
    private bool startInsideDrill = true;
    private Coroutine cooldownRoutine;
    private CharacterScript playerScript;
    private Rigidbody2D playerRb;
    private SpriteRenderer playerSprite;

    void Start()
    {
        playerScript = GetComponent<CharacterScript>();
        playerRb = GetComponent<Rigidbody2D>();
        playerSprite = GetComponent<SpriteRenderer>();

        drill.OnReachedStop += HandleReachedStop;

        if (cooldownText != null)
            cooldownText.gameObject.SetActive(false);

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

        if (cooldownText != null)
            cooldownText.gameObject.SetActive(true);

        while (timer > 0f)
        {
            if (cooldownText != null)
                cooldownText.text = $"Бур будет готов через {Mathf.CeilToInt(timer)} сек.";

            timer -= Time.deltaTime;
            yield return null;
        }

        canEnterDrill = true;

        if (cooldownText != null)
            cooldownText.gameObject.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (drill == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(drill.transform.position, enterDistance);
    }
}