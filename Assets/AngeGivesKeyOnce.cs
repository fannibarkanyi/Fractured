using System.Collections;
using UnityEngine;

public class AngeGivesKeyOnce : MonoBehaviour
{
    public GameObject keyVisualAnge;
    public GameObject speechBubble;
    public float bubbleTime = 5f;

    [Header("Unlock this when key is given")]
    public LockedBarrier lockedBarrier;   // drag your barrier object here

    private bool alreadyGiven = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyGiven) return;
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        // Give key (your existing method)
        inv.GiveKey();

        // Unlock the exit barrier
        if (lockedBarrier != null)
            lockedBarrier.Unlock();

        // Hide key in Ange's hand
        if (keyVisualAnge != null)
            keyVisualAnge.SetActive(false);

        // Speech bubble
        if (speechBubble != null)
            StartCoroutine(ShowBubble());

        alreadyGiven = true;

        // Disable this trigger so it never fires again
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    private IEnumerator ShowBubble()
    {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(bubbleTime);
        speechBubble.SetActive(false);
    }
}
