using System.Collections.Generic;
using UnityEngine;

public class ProjectileArcSolver : IProjectileArcSolver {
    public Vector3 CalculateArc(Vector3 start, Vector3 target, float initialSpeed) {
        Vector3 delta = target - start;
        Vector3 deltaXZ = new Vector3(delta.x, 0f, delta.z);
        float horizontalDistance = deltaXZ.magnitude;

        if (horizontalDistance < 0.001f || initialSpeed <= 0f) {
            return Vector3.zero;
        }

        float gravity = Mathf.Abs(Physics.gravity.y);
        float speed2 = initialSpeed * initialSpeed;
        float speed4 = speed2 * speed2;
        float y = delta.y;

        float discriminant = speed4 - gravity * (gravity * horizontalDistance * horizontalDistance + 2f * y * speed2);
        if (discriminant < 0f) {
            return Vector3.zero;
        }

        float sqrt = Mathf.Sqrt(discriminant);
        float angle = Mathf.Atan((speed2 - sqrt) / (gravity * horizontalDistance)); // lower arc

        Vector3 horizontalDirection = deltaXZ / horizontalDistance;
        Vector3 velocity = horizontalDirection * (Mathf.Cos(angle) * initialSpeed);
        velocity.y = Mathf.Sin(angle) * initialSpeed;

        return velocity;
    }

    public bool IsValidLaunch(Vector3 start, Vector3 target, float initialSpeed, float gravity) {
        if (initialSpeed <= 0f) {
            return false;
        }

        Vector3 delta = target - start;
        float horizontalDistance = new Vector2(delta.x, delta.z).magnitude;
        float speed2 = initialSpeed * initialSpeed;
        float speed4 = speed2 * speed2;
        float g = Mathf.Abs(gravity);
        float discriminant = speed4 - g * (g * horizontalDistance * horizontalDistance + 2f * delta.y * speed2);
        return discriminant >= 0f;
    }

    public List<Vector3> GeneratePathPoints(Vector3 start, Vector3 target, float initialSpeed, int segments) {
        int clampedSegments = Mathf.Max(1, segments);
        List<Vector3> points = new List<Vector3>(clampedSegments + 1);

        Vector3 velocity = CalculateArc(start, target, initialSpeed);
        if (velocity == Vector3.zero) {
            points.Add(start);
            return points;
        }

        float maxTime = Mathf.Max(0.1f, Vector3.Distance(start, target) / Mathf.Max(0.1f, new Vector2(velocity.x, velocity.z).magnitude));
        float step = maxTime / clampedSegments;

        for (int i = 0; i <= clampedSegments; i++) {
            float t = step * i;
            Vector3 point = start + velocity * t + 0.5f * Physics.gravity * t * t;
            points.Add(point);
        }

        return points;
    }
}
