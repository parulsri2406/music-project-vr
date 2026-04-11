using UnityEngine;

public class LanternDrag : MonoBehaviour
{
    public Transform hand;
    private bool isHolding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isHolding = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isHolding = false;
        }
    }

    private void Update()
    {
        if (isHolding && hand != null)
        {
            transform.position = hand.position;
        }
    }
}