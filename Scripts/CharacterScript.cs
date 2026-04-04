using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    // Переменные физические, счетчики и тп
    [SerializeField] public float speed = 5.0f, JumpForce = 5.0f;
    [SerializeField] private float groundRadius = 0.25f;
    public float height;
    private Vector2 moveInput;
    private bool jumpRequested = false;

    // Переменные компонентов (скриптов)
    private Rigidbody2D body;
    private Transform transform;
    private Animator animator;
    private SpriteRenderer spr;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;

    // Булевы
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);
    }
    private void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = IsGrounded() ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }

    void Start()
    {
        moveInput.y = 0;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        HandleInput();
        Animations();
    }

    void FixedUpdate() {
        Move();
    }

    // Движение персонажа
    void Move() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        body.linearVelocity = new Vector2(moveInput.x * speed, body.linearVelocity.y);

        if (jumpRequested && IsGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, JumpForce);
            Debug.Log("Прыжок!");
        }
        
        // Сброс флага после прыжка
        jumpRequested = false;
    }

    // Проверка нажатия кнопки
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
        }
    }

    // Анимации персонажа
    void Animations()
    {
        bool isMoving = moveInput != Vector2.zero;
        animator.SetBool("IsMoving", isMoving);

        if (moveInput.x > 0.01f)
            spr.flipX = false;
        else if (moveInput.x < -0.01f)
            spr.flipX = true;
    }
}
