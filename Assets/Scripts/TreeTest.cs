using UnityEngine;

public class TreeTest : MonoBehaviour
{
    public ParticleSystem energyParticles;
    public Transform hand;
    public Renderer treeRenderer;

    private bool isInside = false;
    private Material treeMaterial;

    private float currentGlow = 0.05f;

    private void Start()
    {
        treeMaterial = treeRenderer.material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isInside = true;
            energyParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            isInside = false;
            energyParticles.Stop();
        }
    }

    private void Update()
    {
        if (isInside && hand != null)
        {
            energyParticles.transform.position = hand.position;

            Vector3 dir = (transform.position - hand.position).normalized;

            var velocity = energyParticles.velocityOverLifetime;
            velocity.enabled = true;
            velocity.space = ParticleSystemSimulationSpace.World;
            velocity.x = dir.x;
            velocity.y = dir.y;
            velocity.z = dir.z;
        }

  
        float targetGlow = isInside ? 1.8f : 0.02f;
        currentGlow = Mathf.Lerp(currentGlow, targetGlow, Time.deltaTime * 0.8f);

        //pulse effect?
        float pulse = isInside ? Mathf.Sin(Time.time * 1.2f) * 0.5f : 0f;

        float finalGlow = currentGlow + pulse;

        Color idleColor = new Color(0.08f, 0.25f, 0.12f);   // dark green
        Color activeColor = new Color(0.4f, 0.8f, 0.3f);    // brighter + slightly yellow-green

        Color finalColor = Color.Lerp(idleColor, activeColor, Mathf.Clamp01(currentGlow));

        treeMaterial.SetColor("_EmissionColor", finalColor * finalGlow);
    }
}