using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fox : MonoBehaviour
{
    public Camera camaraJugador; // Referencia a la cámara del jugador
    public GameObject hudObjeto; // El HUD que quieres mostrar/ocultar
    public GameObject objetoNuevo; // El objeto adicional para la tecla F
    public GameObject objetoADesactivar; // El objeto que deseas desactivar al activar el objeto nuevo
    public TextMeshProUGUI mensajeTextMeshF; // TextMeshPro para el mensaje de error con la tecla F
    public KeyCode teclaActivarF = KeyCode.F; // Tecla para activar el nuevo objeto
    public int costoMonedas = 50; // Costo en monedas para activar el nuevo objeto
    public Color colorRojoParpadeante = Color.red; // Color rojo para el parpadeo
    public int parpadeos = 2; // Cantidad de veces que parpadeará el mensaje
    public Player playerScript; // Referencia al script del jugador
    public AudioSource audioSource; // Referencia al componente AudioSource
    public AudioClip compraSound; // Sonido de compra
    public AudioClip sinMonedasSound; // Sonido de falta de monedas

    private bool puedeCorrer = true; // Indica si el jugador puede correr al esprintar

    void Start()
    {
        // Obtener la referencia al script del jugador
        if (playerScript == null)
        {
            playerScript = FindObjectOfType<Player>();
        }
    }

    void Update()
    {
        // Si el nuevo objeto está activo, el jugador no puede correr al esprintar
        if (objetoNuevo.activeSelf)
        {
            puedeCorrer = false;
        }

        // Si el jugador no puede correr, no procesar el esprintar
        if (!puedeCorrer && Input.GetKey(KeyCode.LeftShift))
        {
            return;
        }

        // Verificar si se ha presionado la tecla para activar el nuevo objeto con la tecla F
        if (Input.GetKeyDown(teclaActivarF) && !objetoNuevo.activeSelf && hudObjeto.activeSelf)
        {
            if (playerScript != null && playerScript.totalCoinsCollected >= costoMonedas)
            {
                // Reproducir el sonido de compra
                if (audioSource != null && compraSound != null)
                {
                    audioSource.PlayOneShot(compraSound);
                }
                // Restar las monedas necesarias
                playerScript.totalCoinsCollected -= costoMonedas;
                playerScript.UpdateCoinCounter(); // Actualizar el contador de monedas del jugador
                // Activar el nuevo objeto
                objetoNuevo.SetActive(true);
                // Desactivar el otro objeto si está activo
                if (objetoADesactivar != null && objetoADesactivar.activeSelf)
                {
                    objetoADesactivar.SetActive(false);
                }
            }
            else
            {
                StartCoroutine(ParpadearMensajeRojo(mensajeTextMeshF));
                // Reproducir el sonido de falta de monedas
                if (audioSource != null && sinMonedasSound != null)
                {
                    audioSource.PlayOneShot(sinMonedasSound);
                }
            }
        }

        // Obtener la posición de origen del rayo ligeramente por encima de la cámara del jugador
        Vector3 posicionOrigen = camaraJugador.transform.position + camaraJugador.transform.up * 1.5f;

        // Lanzar un rayo desde la posición de origen en la dirección de la mirada de la cámara
        Ray rayo = new Ray(posicionOrigen, camaraJugador.transform.forward);

        RaycastHit hit;

        // Comprobar si el rayo intersecta con algún objeto
        if (Physics.Raycast(rayo, out hit))
        {
            // Verificar si el objeto intersectado es el objeto nuevo
            if (hit.collider.gameObject == gameObject)
            {
                // Mostrar el HUD del objeto nuevo si está mirando hacia él
                hudObjeto.SetActive(true);
            }
            else
            {
                // Ocultar el HUD del objeto nuevo si no lo está mirando
                hudObjeto.SetActive(false);
            }
        }
        else
        {
            // Si el rayo no intersecta con ningún objeto, ocultar el HUD del objeto nuevo
            hudObjeto.SetActive(false);
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
