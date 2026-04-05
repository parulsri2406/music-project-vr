using UnityEngine;

public class LeftHandTest : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something touched me: " + other.name);
    }
}