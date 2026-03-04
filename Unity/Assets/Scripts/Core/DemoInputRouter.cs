using UnityEngine;

public class DemoInputRouter : MonoBehaviour
{
    public float MoveSpeed = 5f;
    public float TurnSpeed = 100f;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        float vertical = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) vertical += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) vertical -= 1f;

        transform.Translate(Vector3.forward * vertical * MoveSpeed * Time.deltaTime);
    }

    private void HandleRotation()
    {
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) horizontal += 1f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) horizontal -= 1f;

        transform.Rotate(Vector3.up, horizontal * TurnSpeed * Time.deltaTime);
    }
}