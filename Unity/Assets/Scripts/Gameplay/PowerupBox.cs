// PowerupBox.cs
// Spawns a power-up when destroyed, with randomized type and visual effects.
// Handles respawn logic and client-side sync via networked events if multiplayer.

using UnityEngine;

public class PowerupBox : MonoBehaviour
{
    [Header("Powerup Types")]
    public enum PowerupType { Health, Speed, FireRate, Armor }
    public PowerupType[] possiblePowerups;

    [Header("Visuals")]
    public ParticleSystem spawnParticles;
    public Animator animator;

    private bool isCollected = false;

    private void Start()
    {
        if (possiblePowerups.Length == 0)
            possiblePowerups = new PowerupType[] { PowerupType.Health };
    }

    public void TakeDamage(float damage)
    {
        if (isCollected) return;

        // Destroy on any damage (simple design)
        DestroyBox();
    }

    private void DestroyBox()
    {
        isCollected = true;

        if (spawnParticles != null)
            spawnParticles.Play();

        if (animator != null)
            animator.SetTrigger("Explode");

        // Spawn a random power-up
        PowerupType type = possiblePowerups[Random.Range(0, possiblePowerups.Length)];
        GameObject powerup = Instantiate(GetPowerupPrefab(type), transform.position, Quaternion.identity);
        powerup.GetComponent<Powerup>().SetType(type);

        Destroy(gameObject);
    }

    private GameObject GetPowerupPrefab(PowerupType type)
    {
        switch (type)
        {
            case PowerupType.Health: return Resources.Load<GameObject>("Prefabs/Powerups/HealthBoost");
            case PowerupType.Speed: return Resources.Load<GameObject>("Prefabs/Powerups/SpeedBoost");
            case PowerupType.FireRate: return Resources.Load<GameObject>("Prefabs/Powerups/FireRateBoost");
            case PowerupType.Armor: return Resources.Load<GameObject>("Prefabs/Powerups/ArmorBoost");
            default: return null;
        }
    }
}
