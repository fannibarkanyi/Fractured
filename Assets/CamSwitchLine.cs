using UnityEngine;

public class CameraSwitchLine : MonoBehaviour
{
    public enum SwitchTo { Left, Right }
    public SwitchTo switchTo = SwitchTo.Right;

    [Tooltip("Minimum horizontal speed required to count as 'moving'")]
    public float minMoveSpeed = 0.1f;

    [Tooltip("If switching to Right, player must be moving right. If switching to Left, player must be moving left.")]
    public bool requireCorrectDirection = true;

    private CameraZoneController cam;

    void Awake()
    {
        if (Camera.main != null)
            cam = Camera.main.GetComponent<CameraZoneController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (cam == null) return;

        // Get player's horizontal movement speed
        float vx = 0f;

        // Prefer Rigidbody2D velocity if available
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null) vx = rb.linearVelocity.x;
        else vx = other.GetComponent<PlayerMovement>() != null ? 1f : 0f; // fallback (rare)

        // Must be moving
        if (Mathf.Abs(vx) < minMoveSpeed) return;

        // Must be moving the correct direction (optional but recommended)
        if (requireCorrectDirection)
        {
            if (switchTo == SwitchTo.Right && vx <= 0f) return;
            if (switchTo == SwitchTo.Left && vx >= 0f) return;
        }

        // Switch camera (snap recommended)
        if (switchTo == SwitchTo.Left) cam.FocusLeft(true);
        else cam.FocusRight(true);
    }
}
