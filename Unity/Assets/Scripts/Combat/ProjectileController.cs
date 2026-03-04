using UnityEngine;

namespace TankPuzzleAssault.Combat
{
    [RequireComponent(typeof(Collider))]
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] private float baseDamage = 20f;
        [SerializeField] private bool consumeOnHit = true;
        [SerializeField] private float lifeSeconds = 8f;
        [SerializeField] private GameObject instigator;

        private bool consumed;

        public void Initialize(GameObject source, float damage)
        {
            instigator = source;
            baseDamage = damage;
        }

        private void Awake()
        {
            if (lifeSeconds > 0f)
            {
                Destroy(gameObject, lifeSeconds);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision == null || collision.contactCount <= 0)
            {
                return;
            }

            ContactPoint contact = collision.GetContact(0);
            HandleHit(collision.collider, contact.point, contact.normal);
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleHit(other, transform.position, -transform.forward);
        }

        private void HandleHit(Collider other, Vector3 hitPoint, Vector3 hitNormal)
        {
            if (consumed || other == null)
            {
                return;
            }

            if (instigator != null && other.transform.root.gameObject == instigator.transform.root.gameObject)
            {
                return;
            }

            var damageable = FindDamageable(other.transform);
            if (damageable == null)
            {
                return;
            }

            var damageableBehaviour = damageable as MonoBehaviour;
            var targetRoot = damageableBehaviour != null ? damageableBehaviour.gameObject : other.transform.root.gameObject;

            var context = new ProjectileHitContext(
                instigator,
                gameObject,
                targetRoot,
                other,
                hitPoint,
                hitNormal,
                damageable.TargetKind,
                baseDamage);

            ProjectileHitResult result = ProjectileHitResolver.ResolveHit(context, damageable, consumeOnHit);
            if (result.ConsumedProjectile)
            {
                consumed = true;
                Destroy(gameObject);
            }
        }

        private static IProjectileDamageable FindDamageable(Transform transformRoot)
        {
            if (transformRoot == null)
            {
                return null;
            }

            var behaviours = transformRoot.GetComponentsInParent<MonoBehaviour>();
            for (int i = 0; i < behaviours.Length; i++)
            {
                if (behaviours[i] is IProjectileDamageable damageable)
                {
                    return damageable;
                }
            }

            return null;
        }
    }
}
