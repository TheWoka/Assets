using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform drillCenter;
    [SerializeField] private Transform shootPos;
    [SerializeField] private GameObject bulletPrefab;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 12f;
    [SerializeField] private float maxAngleFromDown = 60f;
    [SerializeField] private float spriteAngleOffset = 0f;
    [SerializeField] private float gizmoRadius = 2.5f;

    [Header("Shoot")]
    [SerializeField] private float timeBtwShots = 0.25f;

    private float shootTimer;
    private bool canControl = false;

    void Start()
    {
        shootTimer = timeBtwShots;
    }

    void Update()
    {
        if (!canControl) return;

        shootTimer += Time.deltaTime;

        RotateToMouse();

        if (Input.GetMouseButton(0) && shootTimer >= timeBtwShots)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    public void SetControl(bool value)
    {
        canControl = value;
    }

    void RotateToMouse()
    {
        if (drillCenter == null) return;

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        Vector2 dir = mouseWorld - drillCenter.position;
        if (dir.sqrMagnitude < 0.001f) return;

        // Если мышь выше центра бура — не крутим
        if (mouseWorld.y > drillCenter.position.y)
            return;

        float rawAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // "Вниз" делаем центральным направлением
        float angleFromDown = Mathf.DeltaAngle(-90f, rawAngle);

        // Ограничиваем сектор симметрично
        float clamped = Mathf.Clamp(angleFromDown, -maxAngleFromDown, maxAngleFromDown);

        // Итоговый локальный угол
        float finalLocalZ = 90f + clamped + spriteAngleOffset;

        Quaternion targetLocalRotation = Quaternion.Euler(0f, 0f, finalLocalZ);

        transform.localRotation = Quaternion.Lerp(
            transform.localRotation,
            targetLocalRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    void Shoot()
    {
        if (bulletPrefab == null || shootPos == null) return;

        Instantiate(bulletPrefab, shootPos.position, shootPos.rotation);
    }

    void OnDrawGizmosSelected()
    {
        if (drillCenter == null) return;

        Vector3 center = drillCenter.position;

        // Центр
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center, 0.08f);

        // Центральное направление вниз
        Gizmos.color = Color.green;
        Vector3 downDir = AngleToDir(-90f);
        Gizmos.DrawLine(center, center + downDir * gizmoRadius);

        // Левая и правая границы сектора
        Gizmos.color = Color.red;
        Vector3 leftLimit = AngleToDir(-90f - maxAngleFromDown);
        Vector3 rightLimit = AngleToDir(-90f + maxAngleFromDown);

        Gizmos.DrawLine(center, center + leftLimit * gizmoRadius);
        Gizmos.DrawLine(center, center + rightLimit * gizmoRadius);

        // Для наглядности — линия от центра к самой пушке
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(center, transform.position);
    }

    Vector3 AngleToDir(float angleDeg)
    {
        float rad = angleDeg * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f);
    }
}