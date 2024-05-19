using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    public Button saveButton;
    public Button loadButton;
    public PlayerStats playerStats;
    public UnlockManager unlockManager;
    public Prices prices;
    public SaveLoadManager saveLoadManager;
    public Text confirmText;
    public float confirmTextDuration = 2f;

    private void Start()
    {
        confirmText.gameObject.SetActive(false);
    }
    public void SaveGame()
    {
        saveLoadManager.SaveGame(playerStats, unlockManager, prices);
        confirmText.text = "Game Saved!";
        StartCoroutine(ShowConfirmText());
    }
    public void LoadGame()
    {
        saveLoadManager.LoadGame(playerStats, unlockManager, prices);
        confirmText.text = "Game Loaded!";
        StartCoroutine(ShowConfirmText());
    }

    private IEnumerator ShowConfirmText()
    {
        confirmText.gameObject.SetActive(true);
        yield return new WaitForSeconds(confirmTextDuration);
        confirmText.gameObject.SetActive(false);
    }
}