using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRGrabStabilizer : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private Rigidbody rb;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
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
        // Reset motion when grabbing
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        // Kill ALL motion on release
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // Optional: force stop any leftover physics jitter
        rb.Sleep();
    }
}