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

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Start()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // if no grab interactable act normal
        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrab);
            grab.selectExited.AddListener(OnRelease);
        }

        startPos = transform.localPosition;
    }

    void OnDestroy()
    {
        if (grab != null)
        {
            grab.selectEntered.RemoveListener(OnGrab);
            grab.selectExited.RemoveListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        isGrabbed = false;
        startPos = transform.localPosition;
    }

    void Update()
    {
        // if object can be grabbed and is grabbed → do nothing
        if (grab != null && isGrabbed)
            return;

        // Otherwise float normally
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, offset, 0);

        if (shouldRotate)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
        }
    }
}