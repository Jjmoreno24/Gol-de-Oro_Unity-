using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    public TMP_Text timerText; // Referencia al texto del cronómetro
    private float startTime;
    private bool timerActive = false;
    public float finalTime; // Almacenar el tiempo final para acceso externo

    void Start()
    {
        timerText.text = "00:00";
        finalTime = 0f; // Inicializar en cero
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }

    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = (t % 60).ToString("00");
            timerText.text = minutes + ":" + seconds;
        }
    }
}
