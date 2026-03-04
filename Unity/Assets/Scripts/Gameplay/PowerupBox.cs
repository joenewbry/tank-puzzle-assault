using UnityEngine;

public class PowerupBox : MonoBehaviour {
    public enum PowerupType {
        Health,
        Shield,
        SpeedBoost,
        MultiShot
    }

    public PowerupType powerupType;
    public float pickupRange = 1.5f;
    public float floatAmplitude = 0.3f;
    public float floatSpeed = 1f;
    private Vector3 originalPosition;
    private float timeOffset;

    private void Awake() {
        originalPosition = transform.position;
        timeOffset = Random.Range(0f, 2f * Mathf.PI);
    }

    private void Update() {
        // Bobbing animation
        float offset = Mathf.Sin(Time.time * floatSpeed + timeOffset) * floatAmplitude;
        transform.position = originalPosition + Vector3.up * offset;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Pickup(other.transform);
        }
    }

    private void Pickup(Transform player) {
        // Apply powerup effect
        switch (powerupType) {
            case PowerupType.Health:
                // Implement health restoration
                break;
            case PowerupType.Shield:
                // Implement shield
                break;
            case PowerupType.SpeedBoost:
                // Implement speed boost
                break;
            case PowerupType.MultiShot:
                // Implement multi-shot
                break;
        }
        Destroy(gameObject);
    }
}