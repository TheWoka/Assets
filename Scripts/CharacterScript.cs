using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    // Переменные физические, счетчики и тп
    [SerializeField] public float speed = 5.0f, JumpForce = 2.0f;
    public float height;
    private Vector2 moveInput;

    // Переменные компонентов (скриптов)
    private Rigidbody2D body;
    private Transform transform;
    private Animator animator;
    private SpriteRenderer spr;

    // Булевы
    private bool isGrounded = true;

    void Start()
    {
        moveInput.y = 0; // Убрать мусор
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Использовать это для ОСТАЛЬНОГО ЧТО НЕ ФИЗИКА
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        Animations();
        Move();
    }

    // Движение персонажа
    void Move() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        /* moveInput.y = Input.GetAxisRaw("Vertical"); */ // ПОКА НЕ МОЖЕТ вверх-вниз. Только вправо и влево
        body.MovePosition(body.position + moveInput.normalized * speed * Time.fixedDeltaTime);
    }

    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, 0);
        body.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        // Нет обратного включения. ТРЕБУЕТСЯ проверка того когда чел приземляется обратно короче.
        Debug.Log("Прыжок выполнен");
    }

    // Анимации персонажа
    void Animations()
    {
        bool isMoving = moveInput != Vector2.zero;

        // Далее появятся строчки для передачи переменных в анимации
    }
}
