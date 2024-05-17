using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaves : MonoBehaviour
{
    public WaveManager waveManager; // Referință către scriptul WaveManager
    public bool Activ = true;
    public WaveTimer waveTimer;
    public PlayerCtrl playerController;
    

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

    private void OnMouseDown()
    {
        Debug.Log("Clopot");
        if (Activ == true)
        {
            if (waveManager != null)
            {
                waveManager.StartWaves();
            }

            if(waveTimer != null)
            {
                waveTimer.StartTimer();
            }

            if (playerController != null)
            {
                playerController.isInPort = false; // Setează că corabia nu mai este în port
            }

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            Activ = false;
        }
    }
}
