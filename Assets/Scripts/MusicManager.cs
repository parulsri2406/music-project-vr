using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] layers;

    void Start()
    {
        foreach (AudioSource layer in layers)
        {
            layer.Play();
        }
    }

    public AudioSource GetLayer(int index)
    {
        return layers[index];
    }
}