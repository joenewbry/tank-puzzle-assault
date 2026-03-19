using UnityEngine;

namespace TankPuzzleAssault.Combat
{
    public enum ProjectileTargetKind
    {
        Unknown = 0,
        Player = 1,
        Enemy = 2,
        Block = 3
    }

    public struct ProjectileHitContext
    {
        public GameObject Instigator;
        public GameObject Projectile;
        public GameObject TargetRoot;
        public Collider TargetCollider;
        public Vector3 HitPoint;
        public Vector3 HitNormal;
        public ProjectileTargetKind TargetKind;
        public float BaseDamage;

        public ProjectileHitContext(
            GameObject instigator,
            GameObject projectile,
            GameObject targetRoot,
            Collider targetCollider,
            Vector3 hitPoint,
            Vector3 hitNormal,
            ProjectileTargetKind targetKind,
            float baseDamage)
        {
            Instigator = instigator;
            Projectile = projectile;
            TargetRoot = targetRoot;
            TargetCollider = targetCollider;
            HitPoint = hitPoint;
            HitNormal = hitNormal;
            TargetKind = targetKind;
            BaseDamage = baseDamage;
        }
    }

    public struct ProjectileHitResult
    {
        public ProjectileTargetKind TargetKind;
        public float FinalDamage;
        public bool WasShielded;
        public bool ConsumedProjectile;

        public ProjectileHitResult(ProjectileTargetKind targetKind, float finalDamage, bool wasShielded, bool consumedProjectile)
        {
            TargetKind = targetKind;
            FinalDamage = finalDamage;
            WasShielded = wasShielded;
            ConsumedProjectile = consumedProjectile;
        }
    }

    public interface IProjectileDamageable
    {
        ProjectileTargetKind TargetKind { get; }
        bool IsAlive { get; }
        void ApplyProjectileDamage(float damage, ProjectileHitContext context);
    }

    public interface IProjectileShield
    {
        bool TryAbsorb(ref float incomingDamage, ProjectileHitContext context);
    }

    public interface IProjectileDamageModifier
    {
        int Priority { get; }
        float ModifyDamage(float currentDamage, ProjectileHitContext context);
    }

    public interface IProjectileTeamProvider
    {
        int TeamId { get; }
    }
}
