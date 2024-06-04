using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 1f;
    public int moveHorizontal;
    public int moveVertical;
    public TextMeshProUGUI coinCounter;
    public AudioSource audioSource;
    public AudioClip movimientoSound;
    public AudioClip correrSound;

    public GameObject vfxPrefab; // Prefab del efecto VFX
    public float vfxDuration = 2f; // Duración del VFX en segundos
    public AudioClip vfxSound; // Sonido del VFX
    public float vfxCooldown = 2f; // Tiempo de espera entre lanzamientos del VFX

    public Transform vfxContainer; // Contenedor de VFX

    private Animator anim;
    public BoxCollider swordCollider;
    private float attackTimer = 0;
    public int totalCoinsCollected = 0;

    private Shop ShopScript;
    private Fox FoxScript;

    private bool isRunning = false;
    private float lastVfxTime = -Mathf.Infinity; // Tiempo del último lanzamiento del VFX

    void Awake()
    {
        anim = GetComponentInParent<Animator>();
    }

    void Start()
    {
        swordCollider = GameObject.FindWithTag("Weapon").GetComponent<BoxCollider>();
        swordCollider.enabled = false;
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
        player.Translate(moveDirection * Time.deltaTime * moveSpeed);

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
            if (moveVertical <= 1)
            {
                swordCollider.enabled = true;
                anim.SetInteger("AttackNum", Random.Range(1, 4));
                anim.SetTrigger("Attack");
            }
        }

        if (swordCollider.enabled == true)
        {
            attackTimer += Time.deltaTime;

            if (attackTimer >= 0.533)
            {
                swordCollider.enabled = false;
                attackTimer = 0;
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            ShootVFX();
        }
    }

    private void ShootVFX()
    {
        // Comprobar si ha pasado suficiente tiempo desde el último lanzamiento del VFX
        if (Time.time - lastVfxTime >= vfxCooldown)
        {
            if (vfxPrefab != null)
            {
                // Instanciar el VFX en la posición del jugador mirando en la dirección del jugador
                GameObject vfxInstance = Instantiate(vfxPrefab, player.position + player.forward, player.rotation);
                // Establecer el VFX como hijo del contenedor
                if (vfxContainer != null)
                {
                    vfxInstance.transform.SetParent(vfxContainer);
                }
                // Destruir el VFX después de la duración especificada
                Destroy(vfxInstance, vfxDuration);

                // Reproducir el sonido del VFX
                if (audioSource != null && vfxSound != null)
                {
                    audioSource.PlayOneShot(vfxSound);
                }

                // Actualizar el tiempo del último lanzamiento del VFX
                lastVfxTime = Time.time;
            }
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
