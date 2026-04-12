using UnityEngine;

public class VolumeStepButton : MonoBehaviour
{
    public bool increase = true; // true = increase, false = decrease
    public Transform hand;
    public Renderer flowerRenderer;

    private Material flowerMaterial;

    private float[] volumeSteps = { 0f, 0.25f, 0.5f, 0.75f, 1f };

    private void Start()
    {
        flowerMaterial = flowerRenderer.material;
    }

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

    private void Update()
    {
        float proximity = 0f;

        if (hand != null)
        {
            float distance = Vector3.Distance(hand.position, transform.position);
            proximity = Mathf.Clamp01(1f - (distance / 2f)); 
        }

        // color change instead of emission (guaranteed to work)
        Color idleColor = new Color(0.5f, 0.2f, 0.4f);   // darker pink
        Color activeColor = new Color(1f, 0.85f, 0.92f);   // bright pink

        Color finalColor = Color.Lerp(idleColor, activeColor, proximity);

        flowerMaterial.color = finalColor;
    }
}