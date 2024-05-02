using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private TowerHealthBar healthBar; // Referencia al script TowerHealthBar

    private void Start()
    {
        // Obtener el componente TowerHealthBar adjunto al mismo GameObject
        healthBar = GameObject.FindGameObjectWithTag("TowerHealthBar").GetComponent<TowerHealthBar>();
    }

    // Método para manejar la colisión con enemigos
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            healthBar.ReduceHealth(5);
        } else if (other.CompareTag("Boss"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            healthBar.ReduceHealth(25);
        } else if (other.CompareTag("Runner"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            healthBar.ReduceHealth(3);
        } else if (other.CompareTag("Slime"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            healthBar.ReduceHealth(1);
        }
    }
}
