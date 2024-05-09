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
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip movimientoSound; // Sonido de movimiento
    public AudioClip correrSound; // Sonido de correr (run)

    private Animator anim;
    public int totalCoinsCollected = 0;

    private Shop ShopScript; // Referencia al script Shop
    private Fox FoxScript; // Referencia al script FoxHUD

    private bool isRunning = false; // Indica si el jugador está corriendo

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Start()
    {
        // Obtener la referencia al script Shop
        ShopScript = FindObjectOfType<Shop>();
        // Obtener la referencia al script FoxHUD
        FoxScript = FindObjectOfType<Fox>();
    }

    void Update()
    {
        // Reproducir el sonido de movimiento si el jugador se está moviendo
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            if (audioSource != null && movimientoSound != null && !audioSource.isPlaying)
            {
                audioSource.clip = movimientoSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            // Detener el sonido de movimiento si el jugador deja de moverse
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        anim.SetFloat("VelY", moveVertical);
        anim.SetFloat("VelX", moveHorizontal);

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Translate(moveDirection * Time.deltaTime * moveSpeed * speedMult);

        // Verificar si el jugador puede correr al esprintar
        bool puedeCorrer = FoxScript == null || FoxScript.objetoNuevo.activeSelf;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && puedeCorrer)
        {
            moveVertical = 2;
            // Si el jugador no estaba corriendo previamente, reproducir el sonido de correr
            if (!isRunning && audioSource != null && correrSound != null)
            {
                if (audioSource.clip != correrSound || !audioSource.isPlaying)
                {
                    audioSource.clip = correrSound;
                    audioSource.loop = true;
                    audioSource.Play();
                }
                isRunning = true;
            }
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveVertical = 1;
            // Si el jugador estaba corriendo previamente, detener el sonido de correr
            if (isRunning && audioSource != null && audioSource.clip == correrSound && audioSource.isPlaying)
            {
                audioSource.Stop();
                isRunning = false;
            }
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveVertical = -1;
        }
        else
        {
            moveVertical = 0;
            // Si el jugador deja de correr, detener el sonido de correr
            if (isRunning && audioSource != null && audioSource.clip == correrSound && audioSource.isPlaying)
            {
                audioSource.Stop();
                isRunning = false;
            }
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
    public void UpdateCoinCounter()
    {
        coinCounter.text = "Coins: " + totalCoinsCollected;
    }
}
