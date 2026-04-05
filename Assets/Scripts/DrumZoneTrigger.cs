using UnityEngine;

public class DrumZoneTrigger : MonoBehaviour
{
    public DrumstickSpawner spawner;
    public AudioSource loopSource;

    private bool hasSpawned = false;

    private void Start()
    {
        if (loopSource != null)
            loopSource.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered by: " + other.name);

        // Only react to controllers (safe check)
        if (!other.name.Contains("Controller")) return;

        if (!hasSpawned)
        {
            spawner.SpawnDrumstick();
            hasSpawned = true;
        }

        if (loopSource != null && !loopSource.isPlaying)
        {
            loopSource.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exited by: " + other.name);

        if (!other.name.Contains("Controller")) return;

        spawner.RemoveDrumsticks();
        hasSpawned = false;

        if (loopSource != null)
        {
            loopSource.Stop();
        }
    }
}