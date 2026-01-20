using System.Collections;
using UnityEngine;

public class AngeGivesKeyOnce : MonoBehaviour
{
    public GameObject keyVisualAnge;   
    public GameObject speechBubble;      
    public float bubbleTime = 5f;

    private bool alreadyGiven = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyGiven) return;
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

        inv.GiveKey();

        if (keyVisualAnge != null)
            keyVisualAnge.SetActive(false);

        if (speechBubble != null)
            StartCoroutine(ShowBubble());

        alreadyGiven = true;
        GetComponent<Collider2D>().enabled = false;
    }

    private IEnumerator ShowBubble()
    {
        speechBubble.SetActive(true);
        yield return new WaitForSeconds(bubbleTime);
        speechBubble.SetActive(false);
    }
}
