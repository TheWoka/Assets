using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMining : MonoBehaviour
{

    [Header("Настройки добычи")]
    [SerializeField] private float mineRange = 1.5f, oreDamage = 5.0f;
    [SerializeField] private LayerMask oreLayer;
    

    // Кэш блока который долбим
    private Camera mainCamera;
    private Animator animator;
    private Ore currentOre;
    void Start()
    {
        animator = GetComponent<Animator>();
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Взятие координат мыши с фиксом глубины
        Vector3 mouseScreen = Input.mousePosition;
        mouseScreen.z = Mathf.Abs(mainCamera.transform.position.z);
        Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(mouseScreen);

        // Для дебага курсор тип
        Debug.DrawRay(mouseWorld, Vector2.up * 0.5f, Color.cyan);
        // Область короче где можем копать
        bool inRange = Vector2.Distance(transform.position, mouseWorld) <= mineRange;

        if (Input.GetMouseButton(0) && inRange)
        {
            // Луч из мыши хуярим, если попадет по руде кайф
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero, 0.1f, oreLayer);

            if (hit.collider != null)
            {
                Ore ore = hit.collider.GetComponent<Ore>();
                if (ore != null)
                {
                    currentOre = ore;
                    currentOre.ApplyDamage(oreDamage * Time.deltaTime);
                    PlayMiningAnimation();
                }
                else currentOre = null;
            }
            else currentOre = null;
        }
        else
        {
            currentOre = null;
            if (animator != null)
                animator.SetBool("IsMining", false);
        }
    }

    void PlayMiningAnimation()
    {
        if (animator == null) return;
        
        /* if (Time.time - lastMiningTime >= miningAnimationCooldown)
        {
            animator.SetTrigger("Mine");
            lastMiningTime = Time.time;
        } */
        
        animator.SetBool("IsMining", true);
    }

    // Дебаг отрисовка области копания
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0.6f, 0, 0.25f);
        Gizmos.DrawWireSphere(transform.position, mineRange);
    }

    // ГЕТТЕР
    public float GetOreDamage()
    {
        return oreDamage;
    }

    // СЕТТЕР
    public void SetOreDamage(float newValue)
    {
        if (newValue > 10f)
        {
            newValue = 10f;
        }

        oreDamage = newValue;
    }
}
