using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NoThrowFix : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    void OnEnable()
    {
        grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        grab.selectExited.RemoveListener(OnRelease);
    }

    void OnRelease(SelectExitEventArgs args)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}