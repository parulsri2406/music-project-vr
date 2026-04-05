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
    if (!other.name.Contains("Controller")) return;

    spawner.SpawnDrumstick();

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