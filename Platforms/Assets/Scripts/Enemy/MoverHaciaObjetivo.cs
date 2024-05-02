using UnityEngine;

public class MoverHaciaObjetivo : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento
    private Transform objetivo; // El objeto al que quieres moverte

    void Start()
    {
        // Buscar el objeto con el tag "Tower" al inicio
        objetivo = GameObject.FindGameObjectWithTag("Tower").transform;

        // Verificar si se encontró el objetivo
        if (objetivo == null)
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Tower'.");
        }
    }

    private void Update()
    {
        // Verificar si se ha encontrado un objetivo
        if (objetivo != null)
        {
            // Calcular la dirección hacia el objetivo
            Vector3 direccion = objetivo.position - transform.position;
            direccion.Normalize();

            // Rotar hacia la dirección del movimiento
            if (direccion != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direccion);
            }

            // Mover el objeto hacia el objetivo
            transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
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
}
