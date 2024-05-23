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
    public AudioSource audioSource;
    public AudioClip movimientoSound;
    public AudioClip correrSound;

    public GameObject vfxPrefab; // Prefab del efecto VFX
    public float vfxDuration = 2f; // Duración del VFX en segundos

    private Animator anim;
    public int totalCoinsCollected = 0;

    private Shop ShopScript;
    private Fox FoxScript;

    private bool isRunning = false;

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Start()
    {
        ShopScript = FindObjectOfType<Shop>();
        FoxScript = FindObjectOfType<Fox>();
    }

    void Update()
    {
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
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        anim.SetFloat("VelY", moveVertical);
        anim.SetFloat("VelX", moveHorizontal);

        Vector3 moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        player.Translate(moveDirection * Time.deltaTime * moveSpeed * speedMult);

        bool puedeCorrer = FoxScript == null || FoxScript.objetoNuevo.activeSelf;

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && puedeCorrer)
        {
            moveVertical = 2;
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

        if (Input.GetMouseButtonDown(1))
        {
            ShootVFX();
        }
    }

    private void ShootVFX()
    {
        if (vfxPrefab != null)
        {
            // Instanciar el VFX en la posición del jugador mirando en la dirección del jugador
            GameObject vfxInstance = Instantiate(vfxPrefab, player.position + player.forward, player.rotation);
            // Destruir el VFX después de la duración especificada
            Destroy(vfxInstance, vfxDuration);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GoldCoin") || other.gameObject.CompareTag("SilverCoin") || other.gameObject.CompareTag("BronzeCoin"))
        {
            Coin coinScript = other.gameObject.GetComponent<Coin>();
            if (coinScript != null && !coinScript.IsCollected())
            {
                totalCoinsCollected += coinScript.GetCoinValue();
                coinScript.Collect();
                UpdateCoinCounter();
            }
        }
    }

    public void UpdateCoinCounter()
    {
        coinCounter.text = "Coins: " + totalCoinsCollected;
    }
}
