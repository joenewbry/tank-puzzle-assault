using UnityEngine;

public class DestructibleObject : MonoBehaviour {
    public int maxHealth = 100;
    private int currentHealth;
    public GameObject destructionEffect;
    public AudioClip destroySound;
    private AudioSource audioSource;

    private void Awake() {
        currentHealth = maxHealth;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            DestroyObject();
        }
    }

    private void DestroyObject() {
        if (destructionEffect != null) {
            Instantiate(destructionEffect, transform.position, transform.rotation);
        }
        if (destroySound != null) {
            audioSource.PlayOneShot(destroySound);
        }
        Destroy(gameObject);
    }
}