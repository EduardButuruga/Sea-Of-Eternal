using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public WaveTextController waveTextController;
    public Button testButton;
    private int currentWave = 0;
    // Start is called before the first frame update
    private int testWaveNumber = 0;
    void Start()
    {
        if (testButton != null)
        {
            testButton.onClick.AddListener(TestWaveText);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestWaveText()
    {
        waveTextController.DisplayWaveText(testWaveNumber);
        Debug.Log("Next wave");
        testWaveNumber++;
    }

    //void StartNewWave()
    //{
    //    currentWave++;
    //    waveTextController.DisplayWaveText(currentWave);

    //    // Logica pentru a începe un nou val de inamici
    //}
}
