using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    public enum CameraSide { Left, Right }
    public CameraSide side;

    private CameraZoneController cam;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraZoneController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (side == CameraSide.Left)
            cam.FocusLeft();
        else
            cam.FocusRight();
    }
}
