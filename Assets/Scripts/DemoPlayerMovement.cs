using UnityEngine;

public class DemoPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public Transform cameraTransform;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Get camera directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Remove vertical tilt
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Calculate movement direction
        Vector3 moveDirection = forward * vertical + right * horizontal;

        // Normalize to avoid faster diagonal movement
        if (moveDirection.magnitude > 1)
            moveDirection.Normalize();
        animator.SetFloat("Velocity", moveDirection.magnitude);
        // Move player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // Rotate player toward movement
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
