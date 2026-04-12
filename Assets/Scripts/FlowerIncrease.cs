using UnityEngine;

public class FlowerIncrease : MonoBehaviour
{
    public bool increase = true; // true = increase, false = decrease
    public Transform hand;
    public Renderer flowerRenderer;

    private Material flowerMaterial;

    private float[] volumeSteps = { 0f, 0.25f, 0.5f, 0.75f, 1f };

    // bounce variables
    private Vector3 originalScale;
    private float bounceTimer = 0f;

    private void Start()
    {
        flowerMaterial = flowerRenderer.material;
        originalScale = transform.localScale;
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

            // trigger bounce
            bounceTimer = 0.2f;
        }
    }

    private void Update()
    {
        // 🌸 proximity glow
        float proximity = 0f;

        if (hand != null)
        {
            float distance = Vector3.Distance(hand.position, transform.position);
            proximity = Mathf.Clamp01(1f - (distance / 2f));
        }

        // soft pink colors
        Color idleColor = new Color(0.6f, 0.3f, 0.5f);
        Color activeColor = new Color(1f, 0.85f, 0.92f);

        Color finalColor = Color.Lerp(idleColor, activeColor, proximity);
        flowerMaterial.color = finalColor;

        // 🌸 bounce feedback
        if (bounceTimer > 0)
        {
            bounceTimer -= Time.deltaTime;

            float scaleOffset = Mathf.Sin((bounceTimer / 0.2f) * Mathf.PI) * 0.2f;
            transform.localScale = originalScale * (1f + scaleOffset);
        }
        else
        {
            transform.localScale = originalScale;
        }
    }
}