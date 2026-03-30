using UnityEngine;

public class FloatMotion : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float speed = 1f;

    [Header("Rotation")]
    public float rotationSpeed = 20f;
    public bool shouldRotate = false; // controlled externally

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        // float forever
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, offset, 0);

        // Rotation 4 enable
        if (shouldRotate)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}