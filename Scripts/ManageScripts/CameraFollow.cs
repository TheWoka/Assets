using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] float minX, maxX, minY, maxY;
    [SerializeField] float follow_speed = 5f;

    [Header("Zoom")]
    public float normalSize = 5f;
    public float zoomedOutSize = 7f;

    [Header("Mouse Look")]
    [SerializeField] private float maxMouseOffset = 3f;
    [SerializeField] private float mouseInfluence = 1f;
    [SerializeField] private float mouseSmooth = 5f;
    [SerializeField] private bool useMouseLookInDrill = false;
    public Transform target;
    private Camera cam;
    private float currentZ;
    private Vector3 currentMouseOffset;

    void Awake()
    {
        cam = GetComponent<Camera>();
        currentZ = transform.position.z;
        cam.orthographicSize = normalSize;
    }

    void FixedUpdate()
    {
        if (!target) return;

        Vector3 targetPos = target.position;

        // Смещение камеры от мыши при включенном режиме
        if (useMouseLookInDrill)
        {
            Vector3 mouseWorld = cam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorld.z = 0f;

            Vector3 rawOffset = (mouseWorld - targetPos) * mouseInfluence;
            rawOffset.z = 0f;

            Vector3 limitedOffset = Vector3.ClampMagnitude(rawOffset, maxMouseOffset);

            currentMouseOffset = Vector3.Lerp(
                currentMouseOffset,
                limitedOffset,
                mouseSmooth * Time.deltaTime
            );

            targetPos += currentMouseOffset;
        }
        else
        {
            currentMouseOffset = Vector3.Lerp(
                currentMouseOffset,
                Vector3.zero,
                mouseSmooth * Time.deltaTime
            );
        }

        Vector3 desiredPosition = new Vector3(
            Mathf.Clamp(targetPos.x, minX, maxX),
            Mathf.Clamp(targetPos.y, minY, maxY),
            currentZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            follow_speed * Time.deltaTime
        );
    }

    public void SetZoom(bool inDrill)
    {
        cam.orthographicSize = inDrill ? zoomedOutSize : normalSize;
        useMouseLookInDrill = inDrill;
    }
}