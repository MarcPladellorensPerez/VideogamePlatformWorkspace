using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class Spawner : MonoBehaviour
{
    public List<GameObject> monsterPrefabsround1 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 1
    public List<GameObject> monsterPrefabsround2 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 2
    public List<GameObject> monsterPrefabsround3 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 3
    public List<GameObject> monsterPrefabsround4 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 4
    public List<GameObject> monsterPrefabsround5 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 5
    public List<GameObject> monsterPrefabsround6 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 6
    public List<GameObject> monsterPrefabsround7 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 7
    public List<GameObject> monsterPrefabsround8 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 8
    public List<GameObject> monsterPrefabsround9 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 9
    public List<GameObject> monsterPrefabsround10 = new List<GameObject>(); // Lista de prefabs de monstruos para la ronda 10

    public float spawnInterval = 3f; // Intervalo de tiempo entre generaciones de monstruos
    public Vector3 spawnRange; // El rango dentro del cual quieres generar los monstruos
    public Transform[] puntosDeCamino; // Puntos de camino para los monstruos

    public Transform container; // Referencia al objeto contenedor donde se guardarán los prefabs spawnizados
    public TextMeshProUGUI roundText; // Referencia al TextMeshPro que muestra el número de ronda

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
        int currentRound = GetCurrentRound();
        List<GameObject> selectedMonsterPrefabs = GetSelectedMonsterPrefabs(currentRound);

        if (selectedMonsterPrefabs.Count == 0)
        {
            Debug.LogWarning("No se han asignado prefabs de monstruos al spawner para la ronda " + currentRound + ".");
            return;
        }

        // Elige aleatoriamente un prefab de monstruo de la lista seleccionada
        GameObject selectedMonsterPrefab = selectedMonsterPrefabs[Random.Range(0, selectedMonsterPrefabs.Count)];

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

        // Obtener el componente MoverHaciaObjetivo del prefab spawnizado
        MoverHaciaObjetivo moverHaciaObjetivo = spawnedMonster.GetComponent<MoverHaciaObjetivo>();
        if (moverHaciaObjetivo != null)
        {
            // Establecer los puntos de camino en el componente MoverHaciaObjetivo
            moverHaciaObjetivo.SetPuntosDeCamino(puntosDeCamino);
        }
        else
        {
            Debug.LogWarning("El prefab de monstruo no tiene el componente MoverHaciaObjetivo.");
        }
    }

    // Método para obtener la ronda actual
    public int GetCurrentRound()
    {
        int roundNumber = 1;
        int.TryParse(roundText.text, out roundNumber); // Intenta convertir el texto del TextMeshPro a un número entero
        return roundNumber;
    }

    // Método para obtener la lista de prefabs de monstruos basada en el número de ronda
    List<GameObject> GetSelectedMonsterPrefabs(int roundNumber)
    {
        switch (roundNumber)
        {
            case 1:
                return monsterPrefabsround1;
            case 2:
                return monsterPrefabsround2;
            case 3:
                return monsterPrefabsround3;
            case 4:
                return monsterPrefabsround4;
            case 5:
                return monsterPrefabsround5;
            case 6:
                return monsterPrefabsround6;
            case 7:
                return monsterPrefabsround7;
            case 8:
                return monsterPrefabsround8;
            case 9:
                return monsterPrefabsround9;
            case 10:
                return monsterPrefabsround10;
            default:
                return new List<GameObject>(); // Si no se ha definido una lista para la ronda actual, devuelve una lista vacía
        }
    }
}
