using System.Collections;
using UnityEngine;

public class AngeGivesKeyOnce : MonoBehaviour
{
    public GameObject keyVisualAnge;
    public GameObject speechBubble;
    public float bubbleTime = 5f;

    [Header("Unlock this when key is given")]
    public LockedBarrier lockedBarrier;   

    private bool alreadyGiven = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (alreadyGiven) return;
        if (!other.CompareTag("Player")) return;

        var inv = other.GetComponent<PlayerInventory>();
        if (inv == null) return;

      
        inv.GiveKey();

       
        if (lockedBarrier != null)
            lockedBarrier.Unlock();


        if (keyVisualAnge != null)
            keyVisualAnge.SetActive(false);

   
        if (speechBubble != null)
            StartCoroutine(ShowBubble());

        alreadyGiven = true;

  
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
