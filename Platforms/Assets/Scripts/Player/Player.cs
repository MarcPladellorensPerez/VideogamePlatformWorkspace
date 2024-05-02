using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform player; 
    public float moveSpeed = 1f;
    public int speedMult = 1;
    public int moveHorizontal;
    public int moveVertical;
    public TextMeshProUGUI coinCounter; 

    private Animator anim;
    private int totalCoinsCollected = 0;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Update()
    {
        anim.SetFloat("VelY", moveVertical);
        anim.SetFloat("VelX", moveHorizontal);

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Translate(moveDirection * Time.deltaTime * moveSpeed * speedMult);

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
        {
            moveVertical = 2;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        else
        {
            moveVertical = 0;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveHorizontal = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveHorizontal = -1;
        }
        else
        {
            moveHorizontal = 0;
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetInteger("Attack", anim.GetInteger("Attack") + 1);
        }
    }

    // Método para recolectar monedas
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GoldCoin") || other.gameObject.CompareTag("SilverCoin") || other.gameObject.CompareTag("BronzeCoin"))
        {
            Coin coinScript = other.gameObject.GetComponent<Coin>();
            if(coinScript != null && !coinScript.IsCollected())
            {
                totalCoinsCollected += coinScript.GetCoinValue();
                coinScript.Collect();
                UpdateCoinCounter();
            }
        }
    }

    // Método para actualizar el contador de monedas en el HUD
    private void UpdateCoinCounter()
    {
        coinCounter.text = "Coins: " + totalCoinsCollected;
    }
}
