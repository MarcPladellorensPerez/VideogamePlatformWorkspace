using UnityEngine;

public class MoverHaciaObjetivo : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    private Transform[] puntosDeCamino; // Array de puntos de camino
    private int indicePuntoActual = 0; // Índice del punto de camino actual

    void Update()
    {
        if (puntosDeCamino == null || puntosDeCamino.Length == 0)
        {
            Debug.LogWarning("No se han asignado puntos de camino.");
            return;
        }

        // Si no hemos alcanzado el último punto de camino
        if (indicePuntoActual < puntosDeCamino.Length)
        {
            // Obtener la dirección hacia el punto de camino actual
            Vector3 direccion = puntosDeCamino[indicePuntoActual].position - transform.position;
            direccion.Normalize();

            // Rotar hacia la dirección del movimiento
            if (direccion != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccion);
            }

            // Mover el objeto hacia el punto de camino actual
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);

            // Si estamos lo suficientemente cerca del punto de camino actual, avanzar al siguiente punto
            if (Vector3.Distance(transform.position, puntosDeCamino[indicePuntoActual].position) < 0.1f)
            {
                indicePuntoActual++;
            }
        }
        else
        {
            Debug.LogWarning("Objetivo no asignado. Asigna un objetivo desde el inspector o asegúrate de que haya un objeto con el tag 'Tower' en la escena.");
        }
    }

    // Método llamado cuando este objeto entra en colisión con otro objeto
    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el otro objeto tiene el tag "Tower"
        if (other.CompareTag("Tower"))
        {
            // Destruir este GameObject
            Destroy(gameObject);
        }
    }

    // Método para establecer los puntos de camino
    public void SetPuntosDeCamino(Transform[] puntos)
    {
        puntosDeCamino = puntos;
    }
}
