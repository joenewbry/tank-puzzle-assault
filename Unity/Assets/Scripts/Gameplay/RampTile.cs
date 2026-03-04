// RampTile.cs
// Defines a tile that slopes upward, modifying projectile physics and tank movement friction.
// Used in terrain generation to create dynamic elevation changes.

using UnityEngine;

public class RampTile : MonoBehaviour
{
    [Header("Ramp Properties")]
    public float slopeAngle = 30f; // degrees
    public float frictionReduction = 0.5f; // multiplier on ground friction

    private void Start()
    {
        // Optionally assign a mesh collider or modify terrain height
        // For now, just mark as ramp for physics queries
    }

    public float GetSlopeMultiplier()
    {
        return Mathf.Cos(slopeAngle * Mathf.Deg2Rad);
    }

    public float GetFrictionMultiplier()
    {
        return frictionReduction;
    }

    // Called by TankControllerBase to adjust movement on this tile
    public virtual void ApplyRampEffect(Rigidbody tankRb)
    {
        // Apply custom physics: reduce friction, add slope vector
        // This could be extended to modify velocity based on direction
    }
}
