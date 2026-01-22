using System.Collections;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{
    public Transform targetPoint;

    public enum RequiredInput { Up, Down }
    public RequiredInput requiredInput = RequiredInput.Down;

    public float cooldown = 0.25f;

    private Transform player;
    private bool playerInside;
    private float lastUseTime;
    private bool isTeleporting;

    void Update()
    {
        if (!playerInside || player == null) return;
        if (targetPoint == null) return;
        if (isTeleporting) return;
        if (Time.time - lastUseTime < cooldown) return;

        bool pressedUp = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
        bool pressedDown = Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);

        bool ok =
            (requiredInput == RequiredInput.Up && pressedUp) ||
            (requiredInput == RequiredInput.Down && pressedDown);

        if (!ok) return;

        StartCoroutine(TeleportWithFade());
        lastUseTime = Time.time;
    }

    private IEnumerator TeleportWithFade()
    {
        isTeleporting = true;

        // Fade out
        PlayerFade fade = player.GetComponent<PlayerFade>();
        if (fade != null)
            yield return fade.FadeOut();

        // Small pause (feels better)
        yield return new WaitForSeconds(0.1f);

        // Teleport
        player.position = targetPoint.position;

        // Reset physics
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        // Fade in
        if (fade != null)
            yield return fade.FadeIn();

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
