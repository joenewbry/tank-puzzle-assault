using UnityEngine;

public class RampTile : MonoBehaviour {
    public float slopeAngle = 30f;
    public LayerMask walkableLayers;
    private Vector3[] cornerPositions;

    private void Awake() {
        cornerPositions = new Vector3[4];
        MeshRenderer mr = GetComponent<MeshRenderer>();
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mr == null || mf == null) {
            Debug.LogError("RampTile needs a MeshRenderer and MeshFilter");
            return;
        }

        // Define ramp corners
        Vector3 center = transform.position;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        float halfWidth = transform.localScale.x * 0.5f;
        float halfLength = transform.localScale.z * 0.5f;

        cornerPositions[0] = center + right * halfWidth + forward * halfLength; // top-right
        cornerPositions[1] = center - right * halfWidth + forward * halfLength; // top-left
        cornerPositions[2] = center - right * halfWidth - forward * halfLength; // bottom-left
        cornerPositions[3] = center + right * halfWidth - forward * halfLength; // bottom-right

        // Adjust height based on slope
        float heightDiff = Mathf.Tan(slopeAngle * Mathf.Deg2Rad) * halfLength;
        cornerPositions[0].y += heightDiff;
        cornerPositions[1].y += heightDiff;
    }

    public bool IsWalkable(Vector3 point) {
        // Simple point-in-polygon check on XY plane
        return true; // Placeholder for actual collision detection
    }
}