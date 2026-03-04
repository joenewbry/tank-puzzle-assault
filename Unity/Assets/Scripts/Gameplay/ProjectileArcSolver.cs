// ProjectileArcSolver.cs
// Handles projectile trajectory calculation using physics-based arc prediction.
// Implements gravity, initial velocity, and target position to compute optimal launch angles.

using UnityEngine;

public class ProjectileArcSolver : MonoBehaviour
{
    public float gravity = 9.81f;
    public Vector3 launchVelocity;
    public Vector3 targetPosition;

    public Vector3 CalculateTrajectory(Vector3 start, Vector3 end, float speed)
    {
        Vector3 delta = end - start;
        float horizontalDistance = new Vector2(delta.x, delta.z).magnitude;
        float verticalDistance = delta.y;
        float a = (gravity * horizontalDistance * horizontalDistance) / (2 * speed * speed);
        float b = horizontalDistance;
        float c = -verticalDistance - a;

        float discriminant = b * b - 4 * (-1) * c;
        if (discriminant < 0) return Vector3.zero; // No solution

        float tanTheta = (-b + Mathf.Sqrt(discriminant)) / (-2);
        float theta = Mathf.Atan(tanTheta);

        Vector3 direction = new Vector3(delta.x, 0, delta.z).normalized;
        direction.y = Mathf.Tan(theta);
        return direction.normalized * speed;
    }
}
