using UnityEngine;

public class AutoPace : MonoBehaviour
{
    public float speed = 2f;
    public float switchTime = 2f;

    private float timer;
    private int direction = 1; 

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        timer += Time.deltaTime;

        if (timer >= switchTime)
        {
            direction *= -1;
            timer = 0f;
        }
    }
}
