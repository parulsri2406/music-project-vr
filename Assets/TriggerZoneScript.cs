using UnityEngine;

public class TriggerZoneScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Transform player = other.transform.root;

        if (player.CompareTag("Player"))
        {
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
            playerRenderer.material.color = Color.green;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Transform player = other.transform.root;

        if (player.CompareTag("Player"))
        {
            Renderer playerRenderer = player.GetComponentInChildren<Renderer>();
            playerRenderer.material.color = Color.white;
        }
    }
}