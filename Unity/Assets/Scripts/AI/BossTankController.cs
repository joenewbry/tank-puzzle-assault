using System;
using UnityEngine;

public class BossTankController : MonoBehaviour {
    public enum BossPhase {
        Intro,
        Phase1,
        Phase2,
        Phase3,
        Defeat
    }
    
    public BossPhase currentPhase = BossPhase.Intro;
    public float healthThresholdPhase1 = 0.7f;  // 70% health
    public float healthThresholdPhase2 = 0.4f;  // 40% health
    public float healthThresholdPhase3 = 0.1f;  // 10% health
    
    public float maxHealth;
    private float currentHealth;
    
    public float phaseDuration = 15f;  // Time between phase transitions
    private float phaseStartTime;
    
    public EnemyVariantConfig bossConfig;
    public EnemyTankAI enemyAI;
    
    // Phase-specific behaviors
    public float phase1SpeedMultiplier = 1.0f;
    public float phase2SpeedMultiplier = 1.5f;
    public float phase3SpeedMultiplier = 2.0f;
    
    public bool phase1HasShield = false;
    public bool phase2HasAoE = true;
    public bool phase3HasDash = true;
    
    // Audio and visuals
    public string[] phaseChangeSounds;
    public string[] phaseVisualEffects;
    
    void Start() {
        currentHealth = maxHealth;
        phaseStartTime = Time.time;
        
        if (enemyAI != null) {
            enemyAI.moveSpeed = bossConfig.moveSpeed;
            enemyAI.attackCooldown = bossConfig.attackCooldown;
        }
    }
    
    void Update() {
        UpdateHealthPhase();
        UpdatePhaseTiming();
    }
    
    void UpdateHealthPhase() {
        float healthRatio = currentHealth / maxHealth;
        
        if (currentPhase == BossPhase.Intro) {
            if (healthRatio <= healthThresholdPhase1) {
                EnterPhase(BossPhase.Phase1);
            }
        } else if (currentPhase == BossPhase.Phase1) {
            if (healthRatio <= healthThresholdPhase2) {
                EnterPhase(BossPhase.Phase2);
            }
        } else if (currentPhase == BossPhase.Phase2) {
            if (healthRatio <= healthThresholdPhase3) {
                EnterPhase(BossPhase.Phase3);
            }
        } else if (currentPhase == BossPhase.Phase3) {
            if (healthRatio <= 0f) {
                EnterPhase(BossPhase.Defeat);
            }
        }
    }
    
    void UpdatePhaseTiming() {
        // Optional: enforce minimum phase duration
        if (Time.time - phaseStartTime > phaseDuration) {
            // Allow phase to change based on health even if timer not done
            // This is handled in UpdateHealthPhase
        }
    }
    
    void EnterPhase(BossPhase newPhase) {
        currentPhase = newPhase;
        phaseStartTime = Time.time;
        
        // Apply phase-specific modifications
        ApplyPhaseBehavior();
        
        // Play sound and effect
        PlayPhaseTransition();
    }
    
    void ApplyPhaseBehavior() {
        if (enemyAI == null) return;
        
        switch (currentPhase) {
            case BossPhase.Phase1:
                enemyAI.moveSpeed *= phase1SpeedMultiplier;
                enemyAI.patrolRadius *= 1.2f;
                enemyAI.chaseRadius *= 1.2f;
                enemyAI.attackRadius *= 1.1f;
                break;
            
            case BossPhase.Phase2:
                enemyAI.moveSpeed *= phase2SpeedMultiplier;
                enemyAI.patrolRadius *= 1.5f;
                enemyAI.chaseRadius *= 1.5f;
                enemyAI.attackRadius *= 1.3f;
                if (phase2HasAoE) {
                    // Activate AoE attack capability
                }
                break;
            
            case BossPhase.Phase3:
                enemyAI.moveSpeed *= phase3SpeedMultiplier;
                enemyAI.patrolRadius *= 2.0f;
                enemyAI.chaseRadius *= 2.0f;
                enemyAI.attackRadius *= 1.5f;
                if (phase3HasDash) {
                    // Activate dash behavior
                }
                break;
            
            case BossPhase.Defeat:
                enemyAI.currentState = TankState.Dead;
                break;
        }
    }
    
    void PlayPhaseTransition() {
        // Play sound
        // Play visual effect (particle system, screen shake)
    }
    
    public void TakeDamage(float damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            currentHealth = 0;
            EnterPhase(BossPhase.Defeat);
        }
    }
}