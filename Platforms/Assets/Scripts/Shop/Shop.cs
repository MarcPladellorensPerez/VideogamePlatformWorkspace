using UnityEngine;

public class BloqueHUD : MonoBehaviour
{
    public Camera camaraJugador; // Referencia a la cámara del jugador
    public GameObject hudBloque; // El HUD que quieres mostrar/ocultar
    public float alturaDesplazamiento = 1.5f; // Altura de desplazamiento del rayo desde la cámara

    void Update()
    {
        // Obtener la posición de origen del rayo ligeramente por encima de la cámara del jugador
        Vector3 posicionOrigen = camaraJugador.transform.position + camaraJugador.transform.up * alturaDesplazamiento;

        // Lanzar un rayo desde la posición de origen en la dirección de la mirada de la cámara
        Ray rayo = new Ray(posicionOrigen, camaraJugador.transform.forward);

        // Debug.DrawRay() para visualizar el rayo
        Debug.DrawRay(rayo.origin, rayo.direction * 10f, Color.red);

        RaycastHit hit;

        // Comprobar si el rayo intersecta con algún objeto
        if (Physics.Raycast(rayo, out hit))
        {
            // Verificar si el objeto intersectado es el bloque
            if (hit.collider.gameObject == gameObject)
            {
                Debug.Log("La cámara del jugador está mirando el bloque.");
                // Mostrar el HUD del bloque si está mirando hacia él
                hudBloque.SetActive(true);
            }
            else
            {
                // Ocultar el HUD del bloque si no lo está mirando
                hudBloque.SetActive(false);
            }
        }
        else
        {
            // Si el rayo no intersecta con ningún objeto, ocultar el HUD del bloque
            hudBloque.SetActive(false);
        }
    }
}
