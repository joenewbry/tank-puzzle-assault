using UnityEngine;

namespace TankPuzzleAssault.Combat
{
    public class ProjectileShield : MonoBehaviour, IProjectileShield
    {
        [SerializeField] private float maxShield = 100f;
        [SerializeField] private bool startFull = true;

        private float currentShield;

        public float CurrentShield => currentShield;
        public float MaxShield => maxShield;

        private void Awake()
        {
            currentShield = startFull ? maxShield : 0f;
        }

        public bool TryAbsorb(ref float incomingDamage, ProjectileHitContext context)
        {
            if (incomingDamage <= 0f || currentShield <= 0f)
            {
                return false;
            }

            float absorbed = Mathf.Min(currentShield, incomingDamage);
            currentShield -= absorbed;
            incomingDamage -= absorbed;
            return absorbed > 0f;
        }

        public void Recharge(float amount)
        {
            if (amount <= 0f)
            {
                return;
            }

            currentShield = Mathf.Clamp(currentShield + amount, 0f, maxShield);
        }
    }
}
