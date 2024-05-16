using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthBar healthBar;
    public GameObject destroyedPrefab; // Referencia al prefab que quieres instanciar cuando el enemigo se destruye
    public string containerTag = "CoinContainer"; // Etiqueta del contenedor
    public float spawnOffset = 0.1f; // La cantidad de unidades que el prefab estará por encima del enemigo

    public int baseHp = 100; // Vida base de los enemigos
    public int Hp;

    void Start()
    {
        AdjustEnemyHealth(); // Ajustar la vida del enemigo al inicio
        healthBar.SetMaxHealth(Hp);
    }

    void Update()
    {
        if (Hp <= 0)
        {
            DestroyEnemy();
        }
        // Debug para mostrar la vida del enemigo cada vez que se actualiza
        Debug.Log("Vida del enemigo: " + Hp);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Hp -= 10;
            healthBar.setHealth(Hp);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Hp -= 2;
            healthBar.setHealth(Hp);
        }
        else if (other.gameObject.CompareTag("Missile"))
        {
            Hp -= 5;
            healthBar.setHealth(Hp);
        }
    }

    private void DestroyEnemy()
    {
        // Buscar el contenedor por etiqueta
        GameObject container = GameObject.FindGameObjectWithTag(containerTag);
        if (container != null)
        {
            // Calcular la posición donde se instanciará el prefab
            Vector3 spawnPosition = transform.position + Vector3.up * spawnOffset;
            // Instanciar el prefab destruido como un hijo del contenedor
            GameObject destroyedObject = Instantiate(destroyedPrefab, spawnPosition, Quaternion.identity, container.transform);
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con la etiqueta " + containerTag);
        }
        // Destruir el enemigo actual
        Destroy(gameObject);
    }

    // Método para ajustar la vida de los enemigos según la ronda
    public void AdjustEnemyHealth()
    {
        int currentRound = GetCurrentRound();
        Hp = baseHp + ((currentRound - 1) * 5); // Incrementar la vida según la ronda
    }

    // Método para obtener la ronda actual
    int GetCurrentRound()
    {
        GameObject spawnerObject = GameObject.FindWithTag("Spawner"); // Encontrar el objeto del Spawner por etiqueta
        if (spawnerObject != null)
        {
            Spawner spawner = spawnerObject.GetComponent<Spawner>();
            return spawner.GetCurrentRound();
        }
        else
        {
            Debug.LogWarning("No se encontró el objeto del Spawner.");
            return 1; // Devolver 1 si no se encuentra el Spawner
        }
    }
}
