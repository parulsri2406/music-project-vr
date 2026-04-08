using UnityEngine;
using System.Collections;

public class SynthManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] chords;

    private Coroutine fadeRoutine;

    void Start()
    {
        // Start with first chord
        audioSource.clip = chords[0];
        audioSource.Play();
        audioSource.volume = 1f;
    }

    public void SetChord(int index)
    {
        if (index < 0 || index >= chords.Length) return;

        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        fadeRoutine = StartCoroutine(FadeToChord(index));
    }

    IEnumerator FadeToChord(int newIndex)
    {
        float duration = 0.3f;
        float time = 0f;

        float startVolume = audioSource.volume;

        // Fade out
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 0f;

        // Switch clip
        audioSource.clip = chords[newIndex];
        audioSource.Play();

        // Fade in
        time = 0f;
        while (time < duration)
        {
            audioSource.volume = Mathf.Lerp(0f, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        audioSource.volume = 1f;
    }
}