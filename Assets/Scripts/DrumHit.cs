using UnityEngine;
using System.Collections;

public class DrumHit : MonoBehaviour
{
    public AudioSource audioSource;

    public float pulseMultiplier = 1.5f;
    public float pitchMultiplier = 1.2f;
    public float pulseDuration = 0.08f;

    private float originalVolume;
    private float originalPitch;

    private void Start()
    {
        if (audioSource != null)
        {
            originalVolume = audioSource.volume;
            originalPitch = audioSource.pitch;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit by: " + other.name);

        // Use TAG instead of name (much safer)
        if (!other.CompareTag("Drumstick")) return;

        Debug.Log("Pulse triggered");

        // Only pulse if loop is already playing
        if (audioSource == null || !audioSource.isPlaying) return;

        StopAllCoroutines();
        StartCoroutine(Pulse());
    }

    private IEnumerator Pulse()
{
    // Stronger boost
    audioSource.pitch = originalPitch * 1.4f;
    audioSource.volume = originalVolume * 2f;

    yield return new WaitForSeconds(0.1f);

    audioSource.pitch = originalPitch;
    audioSource.volume = originalVolume;
}
}