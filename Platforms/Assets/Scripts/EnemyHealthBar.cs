using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Slider Healthbar;

    public Camera Camera;

    public void Awake()
    {
        Camera = Camera.main;
    }

    public void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }
    public void SetMaxHealth(int maxHealth)
    {
        Healthbar.maxValue = maxHealth;
        Healthbar.value = maxHealth;
    }

    public void setHealth(int health)
    {
        Healthbar.value = health;
    }
}