using UnityEngine;

public class AutoPace : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;

    private float timer;
    private int direction = 1; // 1 = right, -1 = left

    void Update()
    {
        // Move left/right
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        // Count time
        timer += Time.deltaTime;

        // Switch direction
        if (timer >= switchTime)
        {
            direction *= -1;
            timer = 0f;
        }
    }
}
