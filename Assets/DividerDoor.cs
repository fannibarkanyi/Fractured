using System.Collections;
using UnityEngine;

public class DividerDoorTeleport : MonoBehaviour
{
    public Transform exitLeft;
    public Transform exitRight;

    public float cooldown = 0.25f;

    private Transform player;
    private bool playerInside;
    private float lastUseTime;
    private bool isTeleporting;

    private CameraZoneController cam;

    void Awake()
    {
        if (Camera.main != null)
            cam = Camera.main.GetComponent<CameraZoneController>();
    }

    void Update()
    {
        if (!playerInside || player == null) return;
        if (isTeleporting) return;
        if (Time.time - lastUseTime < cooldown) return;

        bool goLeft = Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
        bool goRight = Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);

        if (goLeft && exitLeft != null)
        {
            StartCoroutine(Teleport(exitLeft, goToLeftSide: true));
            lastUseTime = Time.time;
        }
        else if (goRight && exitRight != null)
        {
            StartCoroutine(Teleport(exitRight, goToLeftSide: false));
            lastUseTime = Time.time;
        }
    }

    private IEnumerator Teleport(Transform exit, bool goToLeftSide)
    {
        isTeleporting = true;

        // Fade OUT
        var fade = player.GetComponent<PlayerFade>();
        if (fade != null) yield return fade.FadeOut();

        // Move camera FIRST (snap), so when she fades in, she's already in-frame
        if (cam != null)
        {
            if (goToLeftSide) cam.FocusLeft(true);
            else cam.FocusRight(true);
        }

        // Teleport player
        player.position = exit.position;

        // Reset physics
        var rb = player.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.02f);

        // Fade IN
        if (fade != null) yield return fade.FadeIn();

        isTeleporting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = true;
        player = other.transform;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        playerInside = false;
        player = null;
    }
}
