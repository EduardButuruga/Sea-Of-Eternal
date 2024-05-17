using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveTimer : MonoBehaviour
{
    public float timeBetweenWaves = 60f; // Timpul între valuri
    private float timer;
    public Text timerText; // Textul pentru afișarea timerului
    public WaveManager showWaveText; // Referință către scriptul ShowWaveText
    private bool isTimerRunning = false;

    void Start()
    {
        timer = timeBetweenWaves;
        UpdateTimerText();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                showWaveText.ShowWaveText(); // Apelează scriptul ShowWaveText
                timer = timeBetweenWaves; // Resetează timerul
            }

            UpdateTimerText();
        }
    }
    public void StartTimer()
    {
        isTimerRunning = true;
        showWaveText.ShowWaveText();
    }

    void UpdateTimerText()
    {
        timerText.text = "Next Wave: " + Mathf.Ceil(timer).ToString() + "s";
    }
}
