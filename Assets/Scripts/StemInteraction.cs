using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class StemInteraction : MonoBehaviour
{
    UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    LayerController layerController;

    public bool isLocked = false;
    public float throwThreshold = 1.5f;

    void Awake()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        layerController = GetComponentInChildren<LayerController>();
        if (layerController == null)
            Debug.Log("LayerController NOT FOUND");
        else
            Debug.Log("LayerController FOUND");
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
        if (layerController != null)
        {
            layerController.ActivateLayer();
        }
    }

    void OnRelease(SelectExitEventArgs args)
    {
        if (isLocked) return;

        Rigidbody rb = GetComponent<Rigidbody>();

        float speed = rb.linearVelocity.magnitude;

        if (speed > throwThreshold)
        {
            LockObject();
        }
    }

    void LockObject()
    {
        isLocked = true;

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;

        if (layerController != null)
        {
            layerController.LockLayer();
        }

        Debug.Log("LOCKED");
    }
}