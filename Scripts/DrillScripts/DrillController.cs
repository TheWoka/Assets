using UnityEngine;
using System;

public class DrillController : MonoBehaviour
{
    [Header("Остановки")]
    [SerializeField] private float[] stopHeights;

    [SerializeField] private float reachTolerance = 0.05f;

    [Header("Скорость")]
    [SerializeField] private float baseSpeed = 5.0f;

    [SerializeField] private float speedIncreasePerStop = 1.0f;

    [SerializeField] private float maxSpeed = 15.0f;
    public bool isActive = false;
    private bool isMoving = false;
    private float currentSpeed;
    private Rigidbody2D rb;
    private int currentStopIndex = 0;
    public bool IsMoving => isMoving;
    public bool HasMoreStops => currentStopIndex < stopHeights.Length;
    public float CurrentSpeed => currentSpeed;
    public event Action OnReachedStop;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = baseSpeed;
    }

    void FixedUpdate()
    {
        if (!isActive || !isMoving)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (currentStopIndex >= stopHeights.Length)
        {
            StopDrill();
            return;
        }

        float targetY = stopHeights[currentStopIndex];
        float currentY = transform.position.y;

        if (currentY >= targetY - reachTolerance)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
            isMoving = false;
            currentStopIndex++;

            currentSpeed += speedIncreasePerStop;

            OnReachedStop?.Invoke();
            return;
        }

        rb.linearVelocity = new Vector2(0f, currentSpeed);
    }

    public void StartRide()
    {
        if (!HasMoreStops) return;
        isActive = true;
        isMoving = true;
    }

    public void StopDrill()
    {
        isMoving = false;
        isActive = false;
        rb.linearVelocity = Vector2.zero;
    }
}