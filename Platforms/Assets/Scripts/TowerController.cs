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
        if (other.CompareTag("Enemy"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            healthBar.ReduceHealth(5);
        }
    }
}
