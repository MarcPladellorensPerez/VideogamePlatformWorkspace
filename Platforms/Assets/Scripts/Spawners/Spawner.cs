using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabs = new List<GameObject>(); // Lista de prefabs de monstruos
    public float spawnInterval = 3f; // Intervalo de tiempo entre generaciones de monstruos
    public Vector3 spawnRange; // El rango dentro del cual quieres generar los monstruos

    public Transform container; // Referencia al objeto contenedor donde se guardarán los prefabs spawnizados

    float timer;

    void Start()
    {
        timer = spawnInterval;

        // Crear un objeto vacío como contenedor si no se ha asignado ninguno
        if (container == null)
        {
            container = new GameObject("MonsterContainer").transform;
        }
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
        GameObject spawnedMonster = Instantiate(selectedMonsterPrefab, spawnPosition, Quaternion.identity);

        // Asignar el objeto contenedor como padre del monstruo spawnizado
        spawnedMonster.transform.SetParent(container);
    }
}
