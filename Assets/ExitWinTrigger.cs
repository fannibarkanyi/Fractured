using UnityEngine;

public class ExitWinTrigger : MonoBehaviour
{
    public GameObject escapedImage;     // UI image for escape ending
    public GameObject restartButton;    // UI restart button

    private bool triggered;

    private void Start()
    {
        if (escapedImage != null)
            escapedImage.SetActive(false);

        if (restartButton != null)
            restartButton.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (escapedImage != null)
            escapedImage.SetActive(true);

        if (restartButton != null)
            restartButton.SetActive(true);

        Time.timeScale = 0f;
    }
}
