using UnityEngine;

public class TriggerZoneScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            playerRenderer.material.color = Color.green;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Renderer playerRenderer = other.GetComponent<Renderer>();
            playerRenderer.material.color = Color.white;
        }
    }
}