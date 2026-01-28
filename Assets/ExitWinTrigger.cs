using UnityEngine;

public class ExitWinTrigger : MonoBehaviour
{
    public GameObject escapedImage; // drag your UI Image (escaped) here

    private bool triggered;

    private void Start()
    {
        if (escapedImage != null)
            escapedImage.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;
        if (!other.CompareTag("Player")) return;

        triggered = true;

        if (escapedImage != null)
            escapedImage.SetActive(true);

        Time.timeScale = 0f;
    }
}
