// TankControllerBase.cs
// Abstract base class for tank movement, targeting, and shooting logic.
// Implements shared physics-based controls and state machine.

using UnityEngine;

public abstract class TankControllerBase : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;

    [Header("Shooting")]
    public float shootCooldown = 1.5f;
    public Transform barrelEnd;
    public GameObject projectilePrefab;

    protected float lastShootTime;
    protected Rigidbody rb;
    protected Transform target;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        HandleInput();
        RotateTowardTarget();
    }

    protected virtual void HandleInput()
    {
        // Override in derived classes
    }

    protected virtual void RotateTowardTarget()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public virtual void Shoot()
    {
        if (Time.time - lastShootTime < shootCooldown) return;

        GameObject projectile = Instantiate(projectilePrefab, barrelEnd.position, barrelEnd.rotation);
        projectile.GetComponent<Projectile>().SetVelocity(GetComponent<Rigidbody>().velocity + barrelEnd.forward * 20f);
        lastShootTime = Time.time;
    }

    public virtual void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
