using UnityEngine;
using System.Collections;

public class ButtonPress : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    private Vector3 originalPosition;
    public float pressDepth = 0.1f;
    public float pressSpeed = 10f;
    private bool isPressed = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void SpawnObject()
    {
        if (!isPressed)
        {
            StartCoroutine(PressButton());
        }
    }

    IEnumerator PressButton()
    {
        isPressed = true;

        Vector3 pressedPosition = originalPosition + Vector3.down * pressDepth;
        GetComponent<Renderer>().material.color = Color.gray;

        // Move down
        while (Vector3.Distance(transform.localPosition, pressedPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, pressedPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }

        // Spawn object
        Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);

        yield return new WaitForSeconds(0.2f);

        // Move back up
        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * pressSpeed);
            yield return null;
        }
        GetComponent<Renderer>().material.color = Color.white;

        isPressed = false;
    }
}