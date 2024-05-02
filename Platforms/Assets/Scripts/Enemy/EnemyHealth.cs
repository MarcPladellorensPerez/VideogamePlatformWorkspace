using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public EnemyHealthBar healthBar;

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
            Destroy(gameObject); // Destruye el objeto enemigo cuando la vida llega a 0 o menos
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
}
