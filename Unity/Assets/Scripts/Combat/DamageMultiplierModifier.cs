using UnityEngine;

namespace TankPuzzleAssault.Combat
{
    public class DamageMultiplierModifier : MonoBehaviour, IProjectileDamageModifier
    {
        [SerializeField] private int priority = 100;
        [SerializeField] private float multiplier = 1f;

        public int Priority => priority;

        public float ModifyDamage(float currentDamage, ProjectileHitContext context)
        {
            return currentDamage * Mathf.Max(0f, multiplier);
        }
    }
}
