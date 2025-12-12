using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isGrounded;

    public Transform groundCheck;           // Empty object under player
    public float groundCheckRadius = 0.1f;  // Size of the ground detector
    public LayerMask groundLayer;           // Set layer to "Ground"

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Left/Right input
        float moveX = Input.GetAxisRaw("Horizontal");
        movement = new Vector2(moveX, 0f);

        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        // Move left/right
        rb.linearVelocity = new Vector2(movement.x * speed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}