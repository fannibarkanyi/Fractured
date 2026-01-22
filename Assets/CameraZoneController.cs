using UnityEngine;

public class CameraZoneController : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float moveSpeed = 8f;

    private Transform target;

    void Start()
    {
        target = leftPoint;
        SnapToTarget();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, desired, Time.deltaTime * moveSpeed);
    }

    public void FocusLeft(bool snap = false)
    {
        target = leftPoint;
        if (snap) SnapToTarget();
    }

    public void FocusRight(bool snap = false)
    {
        target = rightPoint;
        if (snap) SnapToTarget();
    }

    private void SnapToTarget()
    {
        if (target == null) return;
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }
}
