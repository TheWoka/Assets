using UnityEngine;

public class ParalaxScript : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float followSpeed = 10f; 
    [SerializeField] private float referenceOrthoSize = 5f; 
    [SerializeField] private Vector2 baseScale = Vector2.one; 

    private Transform camTransform;
    private float startZ;

    void Awake()
    {
        // Если забуду в сцене поставить нужную
        if (targetCamera == null) 
        {
            targetCamera = Camera.main; 
            Debug.Log("Установил стандартную камеру. ParalaxScript");
        }
        camTransform = targetCamera.transform;
        startZ = transform.position.z;
        
        ApplyZoom();
    }

    void LateUpdate()
    {
        if (camTransform == null) return;

        Vector3 targetPos = camTransform.position;
        targetPos.z = startZ;
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
        ApplyZoom();
    }

    private void ApplyZoom()
    {
        if (targetCamera == null) return;
        float ratio = targetCamera.orthographicSize / referenceOrthoSize;
        transform.localScale = new Vector3(baseScale.x, baseScale.y, 1f) * ratio;
    }
}