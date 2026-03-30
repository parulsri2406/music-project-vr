using UnityEngine;
using UnityEngine.XR;

public class XRTriggerActivator : MonoBehaviour
{
    public LayerController layerController;

    private bool wasPressed = false;

    void Update()
    {
        var device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);

        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out bool pressed))
        {
            if (pressed && !wasPressed)
            {
                if (layerController != null)
                {
                    layerController.ActivateLayer();
                }
            }

            wasPressed = pressed;
        }
    }
}