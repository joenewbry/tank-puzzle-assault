using TankPuzzleAssault.Combat;
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
        var receiver = player.GetComponentInParent<ProjectileDamageReceiver>();

        // Apply powerup effect
        switch (powerupType) {
            case PowerupType.Health:
                if (receiver != null) {
                    receiver.RestoreHealth(35f);
                }
                break;

            case PowerupType.Shield:
                var shield = player.GetComponentInParent<ProjectileShield>();
                if (shield == null) {
                    shield = player.gameObject.AddComponent<ProjectileShield>();
                }
                shield.Recharge(75f);
                break;

            case PowerupType.SpeedBoost:
                // Hook point for future move-speed modifier components.
                break;

            case PowerupType.MultiShot:
                // Hook point for future projectile count / spread modifiers.
                break;
        }

        Destroy(gameObject);
    }
}
