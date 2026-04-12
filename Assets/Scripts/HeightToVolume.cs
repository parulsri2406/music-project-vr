using UnityEngine;

public class HeightToVolume : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audioSource;

    [Header("Height Settings")]
    public float minY = 1.4f;
    public float maxY = 5.5f;

    [Header("Volume Settings")]
    public float minVolume = 0f;
    public float maxVolume = 1f;
    public float activationThreshold = 0.1f;

    [Header("Smoothing")]
    public float smoothSpeed = 5f;

    [Header("Optional Glow")]
    public Renderer objectRenderer;
    public Color emissionColor = Color.white;
    public float maxEmission = 2f;

    private Material mat;

    void Start()
    {
        if (objectRenderer != null)
        {
            mat = objectRenderer.material;
        }
    }

    void Update()
    {
        if (audioSource == null || !audioSource.isPlaying)
            return;

        float currentY = transform.position.y;

        // normalizing height
        float normalizedY = Mathf.InverseLerp(minY, maxY, currentY);

        // threshold
        if (normalizedY < activationThreshold)
        {
            normalizedY = 0f;
        }

        // volume mapping
        float targetVolume = Mathf.Lerp(minVolume, maxVolume, normalizedY);

        // volume smoothing line
        audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * smoothSpeed);

        //glow effect synced with height
        if (mat != null)
        {
            float emissionStrength = normalizedY * maxEmission;
            Color finalColor = emissionColor * emissionStrength;
            mat.SetColor("_EmissionColor", finalColor);
        }
    }
}