using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthBar healthBar;
    public GameObject destroyedPrefab; // Referencia al prefab que quieres instanciar cuando el enemigo se destruye
    public float spawnOffset = 0.1f; // La cantidad de unidades que el prefab estará por encima del enemigo

    public int MaxHp = 100;
    public int Hp;

    // Start is called before the first frame update
    void Start()
    {
        Hp = MaxHp;
        healthBar.SetMaxHealth(MaxHp);
    }

    // Update is called once per frame
    void Update()
    {
        if (Hp <= 0)
        {
            DestroyEnemy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Weapon"))
        {
            Hp -= 10;
            healthBar.setHealth(Hp);
            Debug.Log("Vida del enemigo: " + Hp + "/" + MaxHp);
        }
    }

    private void DestroyEnemy()
    {
        // Calcular la posición donde se instanciará el prefab
        Vector3 spawnPosition = transform.position + Vector3.up * spawnOffset;
        // Instanciar el prefab destruido en la posición ajustada
        Instantiate(destroyedPrefab, spawnPosition, Quaternion.identity);
        // Destruir el enemigo actual
        Destroy(gameObject);
    }
}
