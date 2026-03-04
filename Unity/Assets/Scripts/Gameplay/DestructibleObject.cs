// DestructibleObject.cs
// Handles health, damage, and destruction logic for destructible terrain and objects.
// Supports particle effects, audio, and chunked debris on destruction.

using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("Destruction")]
    public GameObject debrisPrefab;
    public ParticleSystem destroyParticles;
    public AudioSource destroySound;

    private bool isDestroyed = false;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        isDestroyed = true;

        if (destroyParticles != null)
            destroyParticles.Play();

        if (destroySound != null)
            destroySound.Play();

        // Spawn debris chunks
        if (debrisPrefab != null)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject chunk = Instantiate(debrisPrefab, transform.position, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                chunk.GetComponent<Rigidbody>().AddExplosionForce(10f, transform.position, 3f);
            }
        }

        Destroy(gameObject);
    }
}
