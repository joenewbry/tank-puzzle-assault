using System;

[System.Serializable]
public class EnemyVariantConfig {
    public string variantName;
    public float health;
    public float moveSpeed;
    public float attackDamage;
    public float attackCooldown;
    public float patrolRadius;
    public float chaseRadius;
    public float attackRadius;
    public float rotationSpeed;
    public int scoreValue;
    public string attackAnimation;
    public string deathAnimation;
    public float spawnWeight;
    
    // AI behavior overrides
    public bool canFlee = false;
    public bool isBoss = false;
    
    // Visual and audio
    public string spriteName;
    public string[] attackSoundClips;
    public string[] deathSoundClips;
    
    // Special abilities
    public bool hasShield = false;
    public float shieldRechargeRate = 0f;
    public bool hasAoEAttack = false;
    public float aoERadius = 0f;
    
    // Progression metadata
    public int unlockLevel = 1;
    public int unlockWave = 1;
    
    public EnemyVariantConfig() {
        // Default values
        moveSpeed = 2f;
        attackCooldown = 2f;
        patrolRadius = 10f;
        chaseRadius = 20f;
        attackRadius = 5f;
        rotationSpeed = 5f;
        scoreValue = 100;
        spawnWeight = 1f;
    }
}