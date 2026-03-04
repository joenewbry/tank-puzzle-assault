using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TankControllerBase : MonoBehaviour {
    public float moveSpeed = 5f;
    public float turnSpeed = 100f;
    public float shootCooldown = 1f;
    protected float lastShotTime = 0f;
    protected Rigidbody rb;
    protected Camera cam;

    protected virtual void Awake() {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    protected virtual void Update() {
        HandleInput();
    }

    protected virtual void HandleInput() {
        // Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        rb.velocity = movement * moveSpeed;

        // Rotation
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        direction.y = 0;
        if (direction.magnitude > 0.1f) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }

    public virtual void Shoot() {
        if (Time.time - lastShotTime < shootCooldown) return;
        lastShotTime = Time.time;
    }
}