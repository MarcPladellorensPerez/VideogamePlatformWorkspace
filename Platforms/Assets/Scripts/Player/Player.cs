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
    public GameObject SingleTurret; // Referencia a la SingleTurret
    public GameObject DoubleTurret; // Referencia a la DoubleTurret

    private Animator anim;
    public int totalCoinsCollected = 0;
    private bool singleTurretActivated = false; // Bandera para indicar si la SingleTurret se ha activado
    private bool doubleTurretActivated = false; // Bandera para indicar si la DoubleTurret se ha activado

    private Shop ShopScript; // Referencia al script Shop

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Start()
    {
        // Obtener la referencia al script Shop
        ShopScript = FindObjectOfType<Shop>();
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

        // Verificar si se ha presionado la tecla para activar la SingleTurret y si el jugador tiene suficientes monedas y la SingleTurret no se ha activado previamente ni la DoubleTurret está activada
        if (Input.GetKeyDown(KeyCode.E) && totalCoinsCollected >= 10 && !singleTurretActivated && !doubleTurretActivated)
        {
            Debug.Log("Activando la SingleTurret.");
            // Restar las monedas necesarias
            totalCoinsCollected -= 10;
            UpdateCoinCounter();
            // Activar la SingleTurret
            SingleTurret.SetActive(true);
            singleTurretActivated = true; // Marcar la SingleTurret como activada
            // Desactivar el HUD en el Shop
            if (ShopScript != null)
            {
                ShopScript.hudBloque.SetActive(false);
            }
        }

        // Verificar si se ha presionado la tecla para activar la DoubleTurret y si el jugador tiene suficientes monedas y la DoubleTurret no se ha activado previamente ni la SingleTurret está activada
        if (Input.GetKeyDown(KeyCode.Q) && totalCoinsCollected >= 40 && !doubleTurretActivated && !singleTurretActivated)
        {
            Debug.Log("Activando la DoubleTurret.");
            // Restar las monedas necesarias
            totalCoinsCollected -= 40;
            UpdateCoinCounter();
            // Activar la DoubleTurret
            DoubleTurret.SetActive(true);
            doubleTurretActivated = true; // Marcar la DoubleTurret como activada
            // Desactivar el HUD en el Shop
            if (ShopScript != null)
            {
                ShopScript.hudBloque.SetActive(false);
            }
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
