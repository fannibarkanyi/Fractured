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
    public LayerMask playerLayer;     // set Rosemary to Player layer
    public LayerMask obstacleLayer;   // walls/platforms that block sight

    [Header("References")]
    public Transform rosemary; // drag Rosemary here

    private Rigidbody2D rb;
    private int direction = 1; // 1 = right, -1 = left
    private bool playerInRoom = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!playerInRoom || rosemary == null)
        {
            Patrol();
            return;
        }

        // ONLY run if he can currently "see" her (meaning she's in front of him)
        if (CanSeeRosemary())
            RunForward();   // IMPORTANT: does NOT turn toward her
        else
            Patrol();
    }

    void Patrol()
    {
        rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);

        // flip direction at bounds
        if (transform.position.x >= rightX) direction = -1;
        else if (transform.position.x <= leftX) direction = 1;

        FaceDirection();
    }

    void RunForward()
    {
        // Run in the CURRENT direction only (no turning)
        rb.linearVelocity = new Vector2(direction * runSpeed, rb.linearVelocity.y);
        FaceDirection();
    }

    bool CanSeeRosemary()
    {
        float dx = rosemary.position.x - transform.position.x;

        // Must be in front of him (walking toward her)
        if (Mathf.Sign(dx) != direction) return false;

        // Must be within distance
        if (Mathf.Abs(dx) > visionDistance) return false;

        // Line-of-sight check
        Vector2 origin = transform.position;
        Vector2 target = rosemary.position;
        Vector2 dir = (target - origin).normalized;
        float dist = Vector2.Distance(origin, target);

        // Blocked by wall?
        RaycastHit2D hitWall = Physics2D.Raycast(origin, dir, dist, obstacleLayer);
        if (hitWall.collider != null) return false;

        // Actually hit player?
        RaycastHit2D hitPlayer = Physics2D.Raycast(origin, dir, dist, playerLayer);
        return hitPlayer.collider != null;
    }

    void FaceDirection()
    {
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (direction == 1 ? 1 : -1);
        transform.localScale = s;
    }

    // Called by sensor script
    public void SetPlayerInRoom(bool inRoom)
    {
        playerInRoom = inRoom;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        Debug.Log("Rosemary got lobotomized. Game Over.");
        Time.timeScale = 0f;
    }
}
