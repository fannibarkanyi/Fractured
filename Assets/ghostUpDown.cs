using UnityEngine;

public class FloatY : MonoBehaviour
{
    public float amplitude = 0.2f;  
    public float frequency = 1f;   
    public float phaseOffset = 0f; 

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency + phaseOffset) * amplitude;
        transform.localPosition = startPos + new Vector3(0f, y, 0f);
    }
}
