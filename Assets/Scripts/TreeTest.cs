using UnityEngine;

public class TreeTest : MonoBehaviour
{
    public ParticleSystem energyParticles;
    public Transform hand;
    public Renderer treeRenderer;

    private bool isInside = false;
    private Material treeMaterial;

    private float currentGlow = 0.1f;
    private float boost = 0f;

    private void Start()
    {
        treeMaterial = treeRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && !isInside)
        {
            isInside = true;
            Debug.Log("Controller detected!");
            energyParticles.Play();

            // Quick noticeable glow boost
            boost = 0.8f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand") && isInside)
        {
            isInside = false;
            energyParticles.Stop();
        }
    }

    private void Update()
    {
        if (isInside && hand != null)
        {
            // Move particles to hand
            energyParticles.transform.position = hand.position;

            // Direction from hand to tree
            Vector3 dir = (transform.position - hand.position).normalized;

            // Apply velocity
            var velocity = energyParticles.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;
            velocity.x = dir.x;
            velocity.y = dir.y;
            velocity.z = dir.z;
        }

        // Smooth glow transition
        float targetGlow = isInside ? 1.2f : 0.1f;
        currentGlow = Mathf.Lerp(currentGlow, targetGlow, Time.deltaTime * 2f);

        // Slower pulse
        float pulse = isInside ? Mathf.Sin(Time.time * 1.5f) * 0.15f : 0f;

        // Decaying boost (makes interaction noticeable)
        boost = Mathf.Lerp(boost, 0f, Time.deltaTime * 2f);

        // Slightly warm green
        Color warmGreen = new Color(0.2f, 0.6f, 0.3f);

        // Final glow
        float finalGlow = currentGlow + pulse + boost;
        treeMaterial.SetColor("_EmissionColor", warmGreen * finalGlow);
    }
}