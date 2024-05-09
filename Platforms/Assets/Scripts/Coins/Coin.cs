using UnityEngine;

public class Coin : MonoBehaviour
{
    private bool collected = false;
    public int coinValue = 0;

    public AudioClip collectSound; // Sonido de recolección de moneda

    private AudioSource audioSource; // Componente AudioSource para reproducir el sonido

    private void Start()
    {
        // Obtener el componente AudioSource del objeto "CoinsCollected"
        audioSource = GameObject.Find("CoinsCollected").GetComponent<AudioSource>();
    }

    public bool IsCollected()
    {
        return collected;
    }

    public void Collect()
    {
        collected = true;

        // Reproducir el sonido de recolección si está configurado y hay un AudioSource
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        Destroy(gameObject); // Destruye el objeto
    }

    public int GetCoinValue()
    {
        return coinValue;
    }
}
