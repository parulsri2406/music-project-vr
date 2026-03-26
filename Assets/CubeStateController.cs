using UnityEngine;

public class CubeStateController : MonoBehaviour
{
    private bool isHit = false;
    private bool inTrigger = false;

    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // Decide color based on state
        if (inTrigger)
        {
            rend.material.color = Color.green;
        }
        else if (isHit)
        {
            rend.material.color = Color.blue;
        }
        else
        {
            rend.material.color = Color.white;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            isHit = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
        }
    }
}