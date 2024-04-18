using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabs = new List<GameObject>(); // Lista de prefabs de monstruos
    public float spawnInterval = 3f; // Intervalo de tiempo entre generaciones de monstruos
    public Vector3 spawnRange; // El rango dentro del cual quieres generar los monstruos

    float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnMonster();
            timer = spawnInterval;
        }
    }

    void SpawnMonster()
    {
        if (monsterPrefabs.Count == 0)
        {
            Debug.LogWarning("No se han asignado prefabs de monstruos al spawner.");
            return;
        }

        // Elige aleatoriamente un prefab de monstruo de la lista
        GameObject selectedMonsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];

        // Genera una posición aleatoria dentro del rango especificado
        Vector3 spawnPosition = transform.position + new Vector3(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y),
            Random.Range(-spawnRange.z, spawnRange.z)
        );

        // Instancia el prefab de monstruo en la posición aleatoria
        Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);
    }
}
