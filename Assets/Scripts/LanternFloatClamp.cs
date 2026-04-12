using UnityEngine;

public class LanternFloatClamp : MonoBehaviour
{
    public float minY = 0.115f; // set this to your pond surface height

    void Update()
    {
        if (transform.position.y < minY)
        {
            Vector3 pos = transform.position;
            pos.y = minY;
            transform.position = pos;
        }
    }
}