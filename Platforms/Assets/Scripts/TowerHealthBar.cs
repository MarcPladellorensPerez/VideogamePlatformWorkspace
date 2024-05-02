using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerHealthBar : MonoBehaviour
{
    public Slider Healthbar;
    public Gradient Gradient;
    public Image Fill;

    public int MaxHp = 100;
    public int Hp = 100;

    public TextMeshProUGUI HpText;

    public void Awake()
    {
        Healthbar.maxValue = MaxHp;
        Healthbar.value = MaxHp;

        Fill.color = Gradient.Evaluate(1f);

        HpText.text = Hp.ToString() + "/" + MaxHp.ToString();
    }

    public void Update()
    {
        // Esto es solo para probar la reducción de vida manualmente
        if (Input.GetKeyDown(KeyCode.F))
        {
            ReduceHealth(5);
        }
    }

    // Reducir la salud de la torre
    public void ReduceHealth(int amount)
    {
        Hp -= amount;
        setHealth(Hp);
    }

    // Método para establecer la salud y actualizar la barra de vida
    public void setHealth(int health)
    {
        Healthbar.value = health;

        Fill.color = Gradient.Evaluate(Healthbar.normalizedValue);

        HpText.text = health.ToString() + "/" + MaxHp.ToString();

        // Verificar si la salud llega a cero
        if (health <= 0)
        {
            // Aquí puedes manejar la lógica cuando la torre se queda sin vida, como por ejemplo destruir la torre.
            Debug.Log("La torre ha sido destruida");
        }
    }

    // Método para manejar la colisión con enemigos
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tank"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            ReduceHealth(5);
        }else if (other.CompareTag("Boss"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            ReduceHealth(25);
        }else if (other.CompareTag("Runner"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            ReduceHealth(3);
        }else if (other.CompareTag("Slime"))
        {
            // Reducir la salud de la torre cuando un enemigo colisiona con ella
            ReduceHealth(1);
        }
    }
}
