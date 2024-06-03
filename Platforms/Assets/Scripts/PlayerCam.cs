using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public bool paused = false;

    public Transform player; // Player's transform
    public float distance = 2.0f; // Distance from the player
    public float cameraHeight = 1.0f; // Height of the camera above the player
    public float sensitivity = 2.0f; // Mouse sensitivity
    public float maxVerticalAngle = 80f; // Maximum vertical angle

    private float verticalRotation = 0f; // Variable to store vertical rotation

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor to the center of the screen
        Cursor.visible = false; // Hide the cursor
    }

    private void Update()
    {
        if (paused) return;

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player horizontally based on mouse X movement
        player.Rotate(Vector3.up * mouseX);

        // Calculate vertical rotation and clamp it
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // Apply vertical rotation to camera
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0);

        // Calculate the new camera position based on player rotation and distance
        Vector3 cameraOffset = Quaternion.Euler(verticalRotation, player.eulerAngles.y, 0) * new Vector3(0, cameraHeight, -distance);
        transform.position = player.position + cameraOffset;

        // Make the camera look at the player
        transform.LookAt(player);
    }
}
