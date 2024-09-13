using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
    private bool isRunning = true;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        if (!isRunning) return;

        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = ((int)(t % 60)).ToString("00");
        string milliseconds = ((int)((t * 100) % 100)).ToString("00"); // Milisaniyeleri ekledik

        timerText.text = minutes + ":" + seconds + ":" + milliseconds;
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
