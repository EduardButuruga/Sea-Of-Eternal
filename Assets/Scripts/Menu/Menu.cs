using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsUI;
    public static bool GameIsPaused = false;
    void Start()
    {
        mainMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                StartGame();
            }
            else
            {
                Pause();
            }
        }
    }
    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    void Pause()
    {
        mainMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OpenOptions()
    {
        Debug.Log("Open Options");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void OptionsUIOpen()
    {
        settingsUI.SetActive(true);
    }
    public void OptionsUIClose()
    {
        settingsUI.SetActive(false);
    }
}
