using UnityEngine;

public class FlowerIncrease : MonoBehaviour
{
    public bool increase = true; 

    private float[] volumeSteps = { 0f, 0.25f, 0.5f, 0.75f, 1f };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            float current = AudioListener.volume;

            int closestIndex = 0;

            // find closest step
            for (int i = 0; i < volumeSteps.Length; i++)
            {
                if (Mathf.Abs(volumeSteps[i] - current) < Mathf.Abs(volumeSteps[closestIndex] - current))
                {
                    closestIndex = i;
                }
            }

            if (increase && closestIndex < volumeSteps.Length - 1)
                closestIndex++;
            else if (!increase && closestIndex > 0)
                closestIndex--;

            AudioListener.volume = volumeSteps[closestIndex];
        }
    }
}