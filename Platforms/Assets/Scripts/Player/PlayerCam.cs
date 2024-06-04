using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public bool paused = false;

    public Transform player; // Player's transform
    public float minDistance = 1.0f; // Mínima distancia desde el jugador
    public float maxDistance = 5.0f; // Máxima distancia desde el jugador
    public float initialDistance = 3.0f; // Distancia inicial desde el jugador
    public float cameraHeight = 1.0f; // Altura de la cámara sobre el jugador
    public float sensitivity = 2.0f; // Sensibilidad del ratón
    public float maxVerticalAngle = 80f; // Máximo ángulo vertical
    public float zoomSpeed = 1.0f; // Velocidad de zoom

    private float distance; // Distancia inicial desde el jugador
    private float verticalRotation = 0f; // Variable para almacenar la rotación vertical

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
        Cursor.visible = false; // Oculta el cursor

        // Establecer la distancia inicial
        distance = Mathf.Clamp(initialDistance, minDistance, maxDistance);
    }

    private void Update()
    {
        if (paused) return;

        // Obtener la entrada del ratón
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotar el jugador horizontalmente según el movimiento del ratón X
        player.Rotate(Vector3.up * mouseX);

        // Calcular la rotación vertical y limitarla
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -maxVerticalAngle, maxVerticalAngle);

        // Aplicar la rotación vertical a la cámara
        transform.localEulerAngles = new Vector3(verticalRotation, transform.localEulerAngles.y, 0);

        // Obtener el input del zoom del ratón
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        // Ajustar la distancia de la cámara según la dirección de la rueda hacia arriba o hacia abajo
        distance = Mathf.Clamp(distance - scrollWheel * zoomSpeed, minDistance, maxDistance);

        // Calcular la nueva posición de la cámara basada en la rotación del jugador y la distancia
        Vector3 cameraOffset = Quaternion.Euler(verticalRotation, player.eulerAngles.y, 0) * new Vector3(0, cameraHeight, -distance);
        transform.position = player.position + cameraOffset;

        // Hacer que la cámara mire al jugador
        transform.LookAt(player);
    }
}
