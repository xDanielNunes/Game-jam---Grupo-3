using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Jump Modifiers")]
    public float jumpForce = 8f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 10f;

    [Header("One Way Platform")]
    public LayerMask oneWayPlatformLayer;
    public float fallThroughTime = 0.3f;
    

    private Rigidbody2D rb;
    private PlayerInputHandler input;
    private InventoryUI inventoryUI;
    
    private bool isGrounded;
    private bool isFallingThrough;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInputHandler>();
        inventoryUI = Object.FindFirstObjectByType<InventoryUI>();
    }

    private void Update()
    {
        CheckGround();
        JumpControl();
        HandleFallThrough();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    void HandleMovement()
    {

        // Impede movimento quando o inventário estiver aberto
        if (inventoryUI != null && inventoryUI.isOpen)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(
            input.MoveInput.x * moveSpeed,
            rb.linearVelocity.y
        );
    }

    void JumpControl()
    {
        // Pulo inicial
        if (input.JumpPressed && isGrounded && input.MoveInput.y >= -0.5f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        // Pulo menor se soltar botão mais cedo
        if (rb.linearVelocity.y > 0 && !input.IsJumpHeld())
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

        // Queda mais rápida
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
    }


    private Collider2D currentOneWayPlatform;

    void CheckGround()
    {
        bool onGround = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        currentOneWayPlatform = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            oneWayPlatformLayer
        );

        isGrounded = onGround || currentOneWayPlatform != null;
    }

    // Lógica para cair através de plataformas one-way
    void HandleFallThrough()
    {
        if (input.MoveInput.y < -0.5f && input.JumpPressed && currentOneWayPlatform != null && !isFallingThrough)
        {
            StartCoroutine(FallThroughPlatform());
        }
    }

    // Coroutine para permitir que o jogador caia através de plataformas one-way por um curto período
    private IEnumerator FallThroughPlatform()
    {
        isFallingThrough = true;

        Collider2D playerCollider = GetComponent<Collider2D>();

        // GUARDA a referência antes de perder contato
        Collider2D platform = currentOneWayPlatform;

        if (platform == null)
        {
            isFallingThrough = false;
            yield break;
        }

        Physics2D.IgnoreCollision(playerCollider, platform, true);

        yield return new WaitForSeconds(fallThroughTime);

        Physics2D.IgnoreCollision(playerCollider, platform, false);

        isFallingThrough = false;
    }
}

