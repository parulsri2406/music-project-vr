using UnityEngine;

public class DrumstickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject drumstickPrefab;

    [SerializeField] private Transform leftHandAttach;
    [SerializeField] private Transform rightHandAttach;

    private GameObject leftStick;
    private GameObject rightStick;

    public void SpawnDrumstick()
    {
        if (drumstickPrefab == null || leftHandAttach == null || rightHandAttach == null)
        {
            Debug.LogWarning("Missing references in DrumstickSpawner!");
            return;
        }

        // LEFT HAND
        if (leftStick == null)
        {
            leftStick = Instantiate(drumstickPrefab);
            leftStick.transform.SetParent(leftHandAttach, false);
            leftStick.transform.localPosition = Vector3.zero;
            leftStick.transform.localRotation = Quaternion.Euler(60f, 0f, 0f);

            Rigidbody rb = leftStick.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }

        // RIGHT HAND
        if (rightStick == null)
        {
            rightStick = Instantiate(drumstickPrefab);
            rightStick.transform.SetParent(rightHandAttach, false);
            rightStick.transform.localPosition = Vector3.zero;
            rightStick.transform.localRotation = Quaternion.Euler(60f, 0f, 0f);

            Rigidbody rb = rightStick.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
                rb.useGravity = false;
            }
        }
    }

    public void RemoveDrumsticks()
    {
        if (leftStick != null)
        {
            Destroy(leftStick);
            leftStick = null;
        }

        if (rightStick != null)
        {
            Destroy(rightStick);
            rightStick = null;
        }
    }
}