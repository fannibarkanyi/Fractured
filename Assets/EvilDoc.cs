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
    public LayerMask playerLayer;     // set to Player layer
    public LayerMask obstacleLayer;   // walls/platforms that block sight

    [Header("References")]
    public Transform rosemary; // drag Rosemary here in inspector

    private Rigidbody2D rb;
    private int direction = 1; // 1 right, -1 left
    private bool playerInRoom = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // If Rosemary isn't in this room, just patrol
        if (!playerInRoom || rosemary == null)
        {
            Patrol();
            return;
        }

        // If she's in the room, only chase if "seen"
        if (CanSeeRosemary())
            Chase();
        else
            Patrol();
    }

    void Patrol()
    {
        // move
        rb.linearVelocity = new Vector2(direction * walkSpeed, rb.linearVelocity.y);

        // flip direction at bounds
        if (transform.position.x >= rightX) direction = -1;
        else if (transform.position.x <= leftX) direction = 1;

        FaceDirection();
    }

    void Chase()
    {
        float dirToPlayer = Mathf.Sign(rosemary.position.x - transform.position.x);
        direction = (dirToPlayer >= 0) ? 1 : -1;

        rb.linearVelocity = new Vector2(direction * runSpeed, rb.linearVelocity.y);
        FaceDirection();
    }

    bool CanSeeRosemary()
    {
        // Only see if she's generally in front of him
        float dx = rosemary.position.x - transform.position.x;
        if (Mathf.Sign(dx) != direction) return false;

        // Also must be within distance
        if (Mathf.Abs(dx) > visionDistance) return false;

        // Line of sight check (raycast)
        Vector2 origin = transform.position;
        Vector2 target = rosemary.position;
        Vector2 dir = (target - origin).normalized;
        float dist = Vector2.Distance(origin, target);

        // If a wall is between them, can't see
        RaycastHit2D hitWall = Physics2D.Raycast(origin, dir, dist, obstacleLayer);
        if (hitWall.collider != null) return false;

        // Confirm player is actually there (helps if multiple colliders)
        RaycastHit2D hitPlayer = Physics2D.Raycast(origin, dir, dist, playerLayer);
        return hitPlayer.collider != null;
    }

    void FaceDirection()
    {
        Vector3 s = transform.localScale;
        s.x = Mathf.Abs(s.x) * (direction == 1 ? 1 : -1);
        transform.localScale = s;
    }

    // ROOM trigger tells us if Rosemary is in the same room
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRoom = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRoom = false;
    }

    // KILL on touch
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player")) return;

        // Replace with your game over method
        Debug.Log("Rosemary got lobotomized. Game Over.");
        Time.timeScale = 0f;
    }
}
