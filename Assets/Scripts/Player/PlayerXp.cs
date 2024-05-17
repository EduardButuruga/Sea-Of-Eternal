using UnityEngine;
using UnityEngine.UI;

public class PlayerXp : MonoBehaviour
{
    public int currentXP = 0;
    public int currentLevel = 1;
    public int xpToNextLevel = 100; // XP necesar pentru a trece la nivelul următor
    public Slider xpSlider; // Referință către bara de XP
    public Text levelText; // Referință către textul pentru nivel
    public RewardSelection rewardSelection;
    public PlayerStats playerStats;

    public AudioSource audioSource; // Componenta AudioSource
    public AudioClip shootSound; // Sunetul de împușcare

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
        }
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        UpdateXPUI();
    }

    public void AddXP(int xp)
    {
        float xpp = xp * playerStats.xpMultiplier;
        currentXP += (int)xpp;

        if (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }

        UpdateXPUI();
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;
        xpToNextLevel = Mathf.RoundToInt(xpToNextLevel * 1.20f); // Crește cerința de XP pentru următorul nivel

        // Actualizează UI-ul pentru nivel
        levelText.text = "Level: " + currentLevel;

        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        rewardSelection.ShowRewardSelection();
    }

    private void UpdateXPUI()
    {
        xpSlider.value = (float)currentXP / xpToNextLevel;
    }
}
