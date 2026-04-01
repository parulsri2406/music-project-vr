using UnityEngine;
using System.Collections;

public class LayerController : MonoBehaviour
{
    public MusicManager musicManager;
    public int layerIndex;

    [Header("Glow Settings")]
    public Renderer glowRenderer;
    public float glowOffAlpha = 0.03f;
    public float glowOnAlpha = 0.9f;
    public float glowTransitionSpeed = 3f;
    public FloatMotion floatMotion;

    private bool isOn = false;
    private Coroutine fadeCoroutine;
    private Coroutine glowCoroutine;
    private Quaternion initialRotation;
    private Material glowMat;

    void Start()
{
    initialRotation = transform.rotation;

    if (glowRenderer != null)
    {
        glowMat = glowRenderer.material;

        Color c = glowMat.color;
        c.a = glowOffAlpha;
        glowMat.color = c;
    }
}

    public void ToggleLayer()
{
    Debug.Log("TOGGLE LAYER CALLED");

    AudioSource layer = musicManager.GetLayer(layerIndex);

    if (fadeCoroutine != null)
        StopCoroutine(fadeCoroutine);

    if (glowCoroutine != null)
        StopCoroutine(glowCoroutine);

    if (!isOn)
    {
        // TURN ON
        isOn = true;

        fadeCoroutine = StartCoroutine(FadeIn(layer));
        glowCoroutine = StartCoroutine(GlowTo(glowOnAlpha));

        if (floatMotion != null)
            floatMotion.shouldRotate = true;
    }
    else
    {
        // TURN OFF
        isOn = false;

        fadeCoroutine = StartCoroutine(FadeOut(layer));
        glowCoroutine = StartCoroutine(GlowTo(glowOffAlpha));

        if (floatMotion != null)
            floatMotion.shouldRotate = false;

        // 👇 Snap back to original rotation
        StartCoroutine(ResetRotation());
    }
}

    IEnumerator FadeIn(AudioSource layer)
    {
        float time = 0f;
        float duration = 5f;

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
    IEnumerator ResetRotation()
{
    Quaternion startRot = transform.rotation;
    float time = 0f;
    float duration = 0.5f;

    while (time < duration)
    {
        transform.rotation = Quaternion.Slerp(startRot, initialRotation, time / duration);
        time += Time.deltaTime;
        yield return null;
    }

    transform.rotation = initialRotation;
}
    IEnumerator GlowTo(float targetAlpha)
    {
        if (glowMat == null) yield break;

        Color currentColor = glowMat.color;
        float startAlpha = currentColor.a;

        float time = 0f;
        float duration = 0.3f;

        while (time < duration)
        {
            float t = time / duration;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);

            currentColor.a = newAlpha;
            glowMat.color = currentColor;

            time += Time.deltaTime;
            yield return null;
        }

        currentColor.a = targetAlpha;
        glowMat.color = currentColor;
    }
    public void ActivateLayer()
{
    ToggleLayer();
}

public void LockLayer()
{
    // optional, keep empty or reuse toggle
}
}