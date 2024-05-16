using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWaves : MonoBehaviour
{
    public WaveManager waveManager; // Referință către scriptul WaveManager
    public PlayerCtrl playerController;
    public bool Activ = true;

    private void OnMouseDown()
    {
        Debug.Log("Clopot");
        if (Activ == true)
        {
            if (waveManager != null)
            {
                waveManager.StartWaves();
            }

            if (playerController != null)
            {
                playerController.canMove = true; // Permite mișcarea jucătorului
            }
            Activ = false;
        }
    }
}
