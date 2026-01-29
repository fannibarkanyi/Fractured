using UnityEngine;

public class LockedBarrier : MonoBehaviour
{
    private Collider2D col;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    public void Unlock()
    {
        col.enabled = false;   
        gameObject.SetActive(false); 
    }
}
