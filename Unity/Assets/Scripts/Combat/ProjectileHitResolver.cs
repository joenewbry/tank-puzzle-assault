using System.Collections.Generic;
using UnityEngine;

namespace TankPuzzleAssault.Combat
{
    public static class ProjectileHitResolver
    {
        private static readonly List<IProjectileDamageModifier> ModifierBuffer = new List<IProjectileDamageModifier>(8);

        public static ProjectileHitResult ResolveHit(
            ProjectileHitContext context,
            IProjectileDamageable damageable,
            bool consumeProjectileOnHit)
        {
            float damage = Mathf.Max(0f, context.BaseDamage);
            bool shielded = false;

            CollectModifiers(context);
            for (int i = 0; i < ModifierBuffer.Count; i++)
            {
                damage = Mathf.Max(0f, ModifierBuffer[i].ModifyDamage(damage, context));
            }

            if (context.TargetRoot != null)
            {
                var shield = FindShield(context.TargetRoot);
                if (shield != null)
                {
                    shielded = shield.TryAbsorb(ref damage, context);
                }
            }

            if (damage > 0f && damageable.IsAlive)
            {
                damageable.ApplyProjectileDamage(damage, context);
            }

            ModifierBuffer.Clear();
            return new ProjectileHitResult(damageable.TargetKind, damage, shielded, consumeProjectileOnHit);
        }

        private static void CollectModifiers(ProjectileHitContext context)
        {
            ModifierBuffer.Clear();
            AddModifiersFrom(context.Projectile);
            AddModifiersFrom(context.Instigator);
            AddModifiersFrom(context.TargetRoot);
            ModifierBuffer.Sort((a, b) => a.Priority.CompareTo(b.Priority));
        }

        private static IProjectileShield FindShield(GameObject source)
        {
            var behaviours = source.GetComponentsInParent<MonoBehaviour>();
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] is IProjectileShield shield)
                {
                    return shield;
                }
            }

            return null;
        }

        private static void AddModifiersFrom(GameObject source)
        {
            if (source == null)
            {
                return;
            }

            var behaviours = source.GetComponents<MonoBehaviour>();
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (!(behaviours[i] is IProjectileDamageModifier modifier))
                {
                    continue;
                }

                if (ModifierBuffer.Contains(modifier))
                {
                    continue;
                }

                ModifierBuffer.Add(modifier);
            }
        }
    }
}
