using UnityEngine;

public class DemoCameraScript : MonoBehaviour
{
    public Transform player;

    // Distance from player
    public Vector3 offset = new Vector3(0, 3, -6);

    public float smoothSpeed = 10f;

    public float mouseSensitivity = 200f;

    float yaw;
    float pitch;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // Mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        // Limit vertical rotation
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        // Rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Desired camera position
        Vector3 desiredPosition = player.position + rotation * offset;

        // Smooth follow
        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );

        // Look at player
        transform.LookAt(player.position + Vector3.up * 1.5f);
    }
}