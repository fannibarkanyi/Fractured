using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EvilDoc : MonoBehaviour
{
    [Header("Patrol bounds (inside room)")]
    public float leftX;
    public float rightX;

    [Header("Speeds")]
    public float walkSpeed = 2f;
    public float runSpeed = 5f;

    [Header("Vision")]
    public float visionDistance = 6f;
    public LayerMask playerLayer;
    public LayerMask obstacleLayer;

    [Header("References")]
    public Transform rosemary;

    [Header("Game Over UI")]
    public GameObject lobotomizedImage;
    public GameObject restartButton;   // 👈 ADD THIS

    private Rigidbody2D rb;
    private int direction = 1;
    private bool playerInRoom = false;
    private bool gameOver = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (lobotomizedImage != null)
            lobotomizedImage.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);   // 👈 HIDE AT START
    }

    void FixedUpdate()
    {
        if (gameOver) return;

        if (!playerInRoom || rosemary == null)
        {
            Patrol();
            return;
        }

        if (CanSeeRosemary())
            RunForward();
        else
            Patrol();
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);

        if (transform.position.x >= rightX) direction = -1;
        else if (transform.position.x <= leftX) direction = 1;

        FaceDirection();
    }

    void RunForward()
    {
        rb.linearVelocity = new Vector2(direction * runSpeed, rb.linearVelocity.y);
        FaceDirection();
    }

    bool CanSeeRosemary()
    {
        float dx = rosemary.position.x - transform.position.x;

        if (Mathf.Sign(dx) != direction) return false;
        if (Mathf.Abs(dx) > visionDistance) return false;

        Vector2 origin = transform.position;
        Vector2 target = rosemary.position;
        Vector2 dir = (target - origin).normalized;
        float dist = Vector2.Distance(origin, target);

        if (Physics2D.Raycast(origin, dir, dist, obstacleLayer)) return false;
        return Physics2D.Raycast(origin, dir, dist, playerLayer);
    }

    void FaceDirection()
    {
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (direction == 1 ? 1 : -1);
        transform.localScale = s;
    }

    public void SetPlayerInRoom(bool inRoom)
    {
        playerInRoom = inRoom;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (gameOver) return;
        if (!collision.collider.CompareTag("Player")) return;

        Debug.Log("Rosemary got lobotomized. Game Over.");

        gameOver = true;

        rb.linearVelocity = Vector2.zero;

        if (lobotomizedImage != null)
            lobotomizedImage.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);   // 👈 SHOW BUTTON

        Time.timeScale = 0f;
    }
}
