using UnityEngine;
using CoreDestructibleObject = DestructibleObject;
using LegacyDestructibleObject = Gameplay.Legacy.DestructibleObject;

namespace TankPuzzleAssault.Combat
{
    public class ProjectileDamageReceiver : MonoBehaviour, IProjectileDamageable
    {
        [Header("Routing")]
        [SerializeField] private ProjectileTargetKind targetKind = ProjectileTargetKind.Unknown;
        [SerializeField] private EnemyTankAI enemyTarget;
        [SerializeField] private BossTankController bossTarget;
        [SerializeField] private CoreDestructibleObject destructibleTarget;
        [SerializeField] private LegacyDestructibleObject legacyDestructibleTarget;

        [Header("Fallback Health (player/default)")]
        [SerializeField] private bool useLocalHealthPool = true;
        [SerializeField] private float maxHealth = 100f;

        private float currentHealth;
        private bool isDead;

        public ProjectileTargetKind TargetKind => targetKind;
        public bool IsAlive => !isDead;

        private void Awake()
        {
            currentHealth = maxHealth;

            if (targetKind == ProjectileTargetKind.Unknown)
            {
                if (CompareTag("Player"))
                {
                    targetKind = ProjectileTargetKind.Player;
                }
                else if (CompareTag("Enemy"))
                {
                    targetKind = ProjectileTargetKind.Enemy;
                }
                else if (CompareTag("Block") || destructibleTarget != null || legacyDestructibleTarget != null)
                {
                    targetKind = ProjectileTargetKind.Block;
                }
            }
        }

        public void ApplyProjectileDamage(float damage, ProjectileHitContext context)
        {
            if (isDead || damage <= 0f)
            {
                return;
            }

            if (enemyTarget != null)
            {
                enemyTarget.TakeDamage(damage);
                isDead = enemyTarget.currentState == TankState.Dead;
                return;
            }

            if (bossTarget != null)
            {
                bossTarget.TakeDamage(damage);
                isDead = bossTarget.currentPhase == BossTankController.BossPhase.Defeat;
                return;
            }

            int roundedDamage = Mathf.Max(1, Mathf.CeilToInt(damage));

            if (destructibleTarget != null)
            {
                destructibleTarget.TakeDamage(roundedDamage);
                return;
            }

            if (legacyDestructibleTarget != null)
            {
                legacyDestructibleTarget.TakeDamage(roundedDamage);
                return;
            }

            if (useLocalHealthPool)
            {
                currentHealth -= damage;
                if (currentHealth <= 0f)
                {
                    currentHealth = 0f;
                    isDead = true;
                }
            }
        }

        public void RestoreHealth(float amount)
        {
            if (!useLocalHealthPool || amount <= 0f)
            {
                return;
            }

            currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
            if (currentHealth > 0f)
            {
                isDead = false;
            }
        }
    }
}
