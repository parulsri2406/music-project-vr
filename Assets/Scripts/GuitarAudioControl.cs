using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GuitarAudioController : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;
    private AudioSource audioSource;

    void Start()
    {
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = GetComponent<AudioSource>();

        Debug.Log("Audio script started on " + gameObject.name);

        if (grab != null)
        {
            grab.selectEntered.AddListener(OnGrab);
        }
        else
        {
            Debug.LogError("XRGrabInteractable NOT found!");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource NOT found!");
        }
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        Debug.Log("GUITAR GRABBED");

        if (audioSource != null)
        {
            audioSource.volume = 1f;
            audioSource.Play();
        }
    }
}