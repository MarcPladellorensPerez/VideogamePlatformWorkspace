using UnityEngine;
using TMPro;

public class TimeCounter : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        float elapsedTime = Time.time - startTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        string timeString = string.Format("Time {0:00}:{1:00}", minutes, seconds);
        timeText.text = timeString;
    }
}
