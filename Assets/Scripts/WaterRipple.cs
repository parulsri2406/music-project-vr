using UnityEngine;

public class WaterRipple : MonoBehaviour
{
    public GameObject ripplePrefab;
    public Transform ripplePoint;

    private bool isTouchingWater = false;
    private float cooldown = 0.1f;
    private float timer = 0f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isTouchingWater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isTouchingWater = false;
        }
    }

    private void Update()
    {
        if (isTouchingWater)
        {
            timer += Time.deltaTime;

            if (timer >= cooldown)
            {
                SpawnRipple();
                timer = 0f;
            }
        }
    }

    void SpawnRipple()
    {
        Vector3 spawnPos = ripplePoint.position;
        spawnPos.y = transform.position.y + 0.05f;

        Instantiate(ripplePrefab, spawnPos, Quaternion.Euler(90, 0, 0));
    }
}