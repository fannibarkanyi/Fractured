using UnityEngine;

public class EvilDocRoomSensor : MonoBehaviour
{
    public EvilDoc doc;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        doc.SetPlayerInRoom(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        doc.SetPlayerInRoom(false);
    }
}
