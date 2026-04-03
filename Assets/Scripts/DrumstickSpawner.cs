using UnityEngine;

public class DrumstickSpawner : MonoBehaviour
{
    [SerializeField] private GameObject drumstickPrefab;
    [SerializeField] private Transform handAttachPoint;

    public void SpawnDrumstick()
    {
        Debug.Log("Spawn function called");

        if (drumstickPrefab == null || handAttachPoint == null)
        {
            Debug.LogWarning("DrumstickSpawner: Missing references!");
            return;
        }

        GameObject stick = Instantiate(drumstickPrefab);

        // Set position & rotation
        stick.transform.position = handAttachPoint.position;
        stick.transform.rotation = handAttachPoint.rotation;

        // Parent to hand
        stick.transform.SetParent(handAttachPoint, true);

        // Disable physics conflicts
        Rigidbody rb = stick.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }
    }
}