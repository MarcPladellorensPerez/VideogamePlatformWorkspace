using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public Camera camaraJugador; // Referencia a la cámara del jugador
    public GameObject hudBloque; // El HUD que quieres mostrar/ocultar
    public GameObject SingleTurret; // El objeto adicional para la tecla E
    public GameObject DoubleTurret; // El objeto adicional para la tecla Q
    public TextMeshProUGUI mensajeTextMeshE; // TextMeshPro para el mensaje de error con la tecla E
    public TextMeshProUGUI mensajeTextMeshQ; // TextMeshPro para el mensaje de error con la tecla Q
    public KeyCode teclaDesactivarE = KeyCode.E; // Tecla para desactivar el HUD permanentemente con la tecla E
    public KeyCode teclaDesactivarQ = KeyCode.Q; // Tecla para desactivar el HUD permanentemente con la tecla Q
    public float alturaDesplazamiento = 1.5f; // Altura de desplazamiento del rayo desde la cámara
    public Color colorRojoParpadeante = Color.red; // Color rojo para el parpadeo
    public int parpadeos = 2; // Cantidad de veces que parpadeará el mensaje
    public Player playerScript; // Referencia al script del jugador
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip compraSound; // Sonido de compra
    public AudioClip sinMonedasSound; // Sonido de falta de monedas

    private bool desactivadoPermanentemente = false; // Indica si el HUD se ha desactivado permanentemente

    void Start()
    {
        // Obtener la referencia al script del jugador
        playerScript = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Si alguna de las torretas está activa, no mostrar el HUD
        if (SingleTurret.activeSelf || DoubleTurret.activeSelf)
        {
            hudBloque.SetActive(false);
            return;
        }

        // Verificar si se ha presionado la tecla para desactivar permanentemente el HUD con la tecla E
        if (Input.GetKeyDown(teclaDesactivarE) && hudBloque.activeSelf)
        {
            if (playerScript != null && playerScript.totalCoinsCollected >= 10)
            {
                // Reproducir el sonido de compra
                if (audioSource != null && compraSound != null)
                {
                    audioSource.PlayOneShot(compraSound);
                }
                // Restar las monedas necesarias
                playerScript.totalCoinsCollected -= 10;
                playerScript.UpdateCoinCounter(); // Actualizar el contador de monedas del jugador
                // Activar la SingleTurret
                SingleTurret.SetActive(true);
            }
            else
            {
                StartCoroutine(ParpadearMensajeRojo(mensajeTextMeshE));
                // Reproducir el sonido de falta de monedas
                if (audioSource != null && sinMonedasSound != null)
                {
                    audioSource.PlayOneShot(sinMonedasSound);
                }
            }
        }

        // Verificar si se ha presionado la tecla para desactivar permanentemente el HUD con la tecla Q
        if (Input.GetKeyDown(teclaDesactivarQ) && hudBloque.activeSelf)
        {
            if (playerScript != null && playerScript.totalCoinsCollected >= 40)
            {
                // Reproducir el sonido de compra
                if (audioSource != null && compraSound != null)
                {
                    audioSource.PlayOneShot(compraSound);
                }
                // Restar las monedas necesarias
                playerScript.totalCoinsCollected -= 40;
                playerScript.UpdateCoinCounter(); // Actualizar el contador de monedas del jugador
                // Activar la DoubleTurret
                DoubleTurret.SetActive(true);
            }
            else
            {
                StartCoroutine(ParpadearMensajeRojo(mensajeTextMeshQ));
                // Reproducir el sonido de falta de monedas
                if (audioSource != null && sinMonedasSound != null)
                {
                    audioSource.PlayOneShot(sinMonedasSound);
                }
            }
        }

        // Si el HUD está desactivado permanentemente, no realizar más comprobaciones
        if (desactivadoPermanentemente)
            return;

        // Obtener la posición de origen del rayo ligeramente por encima de la cámara del jugador
        Vector3 posicionOrigen = camaraJugador.transform.position + camaraJugador.transform.up * alturaDesplazamiento;

        // Lanzar un rayo desde la posición de origen en la dirección de la mirada de la cámara
        Ray rayo = new Ray(posicionOrigen, camaraJugador.transform.forward);

        // Debug.DrawRay() para visualizar el rayo
        Debug.DrawRay(rayo.origin, rayo.direction * 10f, Color.red);

        RaycastHit hit;

        // Comprobar si el rayo intersecta con algún objeto
        if (Physics.Raycast(rayo, out hit))
        {
            // Verificar si el objeto intersectado es el bloque
            if (hit.collider.gameObject == gameObject)
            {
                // Mostrar el HUD del bloque si está mirando hacia él y no está desactivado permanentemente
                if (!desactivadoPermanentemente)
                {
                    hudBloque.SetActive(true);
                }
            }
            else
            {
                // Ocultar el HUD del bloque si no lo está mirando
                hudBloque.SetActive(false);
            }
        }
        else
        {
            // Si el rayo no intersecta con ningún objeto, ocultar el HUD del bloque
            hudBloque.SetActive(false);
        }
    }

    IEnumerator ParpadearMensajeRojo(TextMeshProUGUI mensajeTextMesh)
    {
        // Ciclo para el parpadeo
        for (int i = 0; i < parpadeos; i++)
        {
            // Cambiar el color del mensaje a rojo
            mensajeTextMesh.color = colorRojoParpadeante;
            // Esperar un momento corto
            yield return new WaitForSeconds(0.1f);
            // Cambiar el color del mensaje a blanco
            mensajeTextMesh.color = Color.white;
            // Esperar otro momento corto
            yield return new WaitForSeconds(0.1f);
        }
    }
}
