using UnityEngine;

namespace Gameplay.Legacy {
    // WARNING: This class is DEPRECATED. Use Core.DestructibleObject instead.
    // This file remains for backward compatibility during transition.
    // DO NOT MODIFY. Remove when all references are updated.
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
}
