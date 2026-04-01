using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FloatMotion : MonoBehaviour
{
    public float amplitude = 0.1f;
    public float speed = 1f;

    [Header("Rotation")]
    public float rotationSpeed = 20f;
    public bool shouldRotate = false;

    private Vector3 startPos;
    private bool isGrabbed = false;

    private XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        startPos = transform.localPosition;
    }

    void OnEnable()
    {
        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectEntered.RemoveListener(OnGrab);
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;

        // Reset base position when released (so float resumes cleanly)
        startPos = transform.localPosition;
    }

    void Update()
    {
        // ONLY float when NOT grabbed
        if (!isGrabbed)
        {
            float offset = Mathf.Sin(Time.time * speed) * amplitude;
            transform.localPosition = startPos + new Vector3(0, offset, 0);

            if (shouldRotate)
            {
                transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            }
        }
    }
}
