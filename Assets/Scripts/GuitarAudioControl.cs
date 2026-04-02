using UnityEngine;

public class GuitarAudioControl : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Height Volume Control")]
    public float minY = 1.4f;   // floor level
    public float maxY = 5.5f;   // max

    public float smoothSpeed = 5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource NOT found!");
            return;
        }

        audioSource.volume = 0f;
        audioSource.Play(); // play once, never restart
    }

   void Update()
{
    float y = transform.position.y;

    float normalized = Mathf.InverseLerp(minY, maxY, y);
    if (normalized < 0.2f)
    {
        audioSource.volume = 0f;
        return;
    }
    float adjusted = (normalized - 0.2f) / (1f - 0.2f);

    float targetVolume = adjusted;

    audioSource.volume = Mathf.Lerp(
        audioSource.volume,
        targetVolume,
        Time.deltaTime * smoothSpeed
    );
}
}