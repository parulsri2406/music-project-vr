using UnityEngine;
using System.Collections;

public class LayerController : MonoBehaviour
{
    public MusicManager musicManager;
    public int layerIndex;

    private bool isOn = false;
    private Coroutine fadeCoroutine;

    public void ToggleLayer()
    {
        isOn = !isOn;

        AudioSource layer = musicManager.GetLayer(layerIndex);

        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        if (isOn)
            fadeCoroutine = StartCoroutine(FadeIn(layer));
        else
            fadeCoroutine = StartCoroutine(FadeOut(layer));
    }

    IEnumerator FadeIn(AudioSource layer)
    {
        float time = 0f;
        float duration = 3f;

        while (time < duration)
        {
            layer.volume = Mathf.Lerp(0, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        layer.volume = 1f;
    }

    IEnumerator FadeOut(AudioSource layer)
    {
        float time = 0f;
        float duration = 1f;

        while (time < duration)
        {
            layer.volume = Mathf.Lerp(1, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        layer.volume = 0f;
    }
}