using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFlight : MonoBehaviour
{
    public float speed = 2f; // Velocidad de vuelo de los pájaros
    public float maxDistanceChange = 5f; // Máxima distancia que los pájaros pueden cambiar en una sola actualización
    public float heightChange = 1f; // Cuánto pueden cambiar en altura

    private Vector3 targetPosition;

    void Start()
    {
        // Establecer una posición de destino aleatoria inicial
        SetNewTarget();
    }

    void Update()
    {
        // Moverse hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Rotar hacia el objetivo
        transform.LookAt(targetPosition);

        // Si llegamos al destino, elegir uno nuevo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
        }
    }

    void SetNewTarget()
    {
        // Elegir una nueva posición de destino aleatoria
        targetPosition = transform.position + Random.insideUnitSphere * maxDistanceChange;
        targetPosition.y = transform.position.y + Random.Range(-heightChange, heightChange);
    }
}
