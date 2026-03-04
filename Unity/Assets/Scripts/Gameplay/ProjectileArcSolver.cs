using System;
using UnityEngine;

public class ProjectileArcSolver {
    public Vector3 CalculateTrajectory(Vector3 launchPosition, Vector3 targetPosition, float launchSpeed, float gravity = -9.81f) {
        Vector3 delta = targetPosition - launchPosition;
        float horizontalDistance = new Vector2(delta.x, delta.z).magnitude;
        float verticalDistance = delta.y;

        float a = (gravity * horizontalDistance * horizontalDistance) / (2 * launchSpeed * launchSpeed);
        float b = horizontalDistance;
        float c = a + verticalDistance;

        // Solve quadratic: ax^2 + bx + c = 0
        float discriminant = b * b - 4 * a * c;
        if (discriminant < 0) return Vector3.zero; // No solution

        float sqrtDiscriminant = Mathf.Sqrt(discriminant);
        float tanTheta1 = (-b + sqrtDiscriminant) / (2 * a);
        float tanTheta2 = (-b - sqrtDiscriminant) / (2 * a);

        // Use the lower angle
        float theta = Mathf.Atan(tanTheta1);
        float directionX = Mathf.Cos(theta);
        float directionZ = Mathf.Sin(theta);

        return new Vector3(directionX, Mathf.Sin(theta), directionZ).normalized * launchSpeed;
    }

    public Vector3 CalculateImpactPoint(Vector3 launchPosition, Vector3 velocity, float gravity = -9.81f) {
        // Simple parabolic trajectory with gravity
        // This is a placeholder for a full physics simulation
        Vector3 position = launchPosition;
        Vector3 currentVelocity = velocity;
        float timeStep = 0.1f;
        float totalTime = 0f;

        while (position.y >= 0) {
            position += currentVelocity * timeStep;
            currentVelocity += new Vector3(0, gravity * timeStep, 0);
            totalTime += timeStep;

            // Limit to avoid infinite loops
            if (totalTime > 10f) break;
        }

        return position;
    }
}