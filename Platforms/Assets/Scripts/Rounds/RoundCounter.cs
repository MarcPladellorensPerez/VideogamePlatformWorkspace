using UnityEngine;
using TMPro;

public class RoundCounter : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    public TimeCounter timeCounterScript;
    private int round = 1;
    private float lastRoundTime;

    void Start()
    {
        lastRoundTime = Time.time;
        UpdateRoundText();
    }

    void Update()
    {
        // Verificar si han pasado 3 minutos desde la última actualización de ronda
        if (Time.time - lastRoundTime >= 10)
        {
            // Incrementar la ronda y actualizar el texto de la ronda
            round++;
            UpdateRoundText();
            // Actualizar el tiempo de la última ronda
            lastRoundTime = Time.time;
        }
    }

    void UpdateRoundText()
    {
        roundText.text = round.ToString(); // Mostrar solo el número de la ronda sin el texto "Round"
    }
}
