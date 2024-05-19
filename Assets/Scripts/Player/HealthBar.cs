using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text healthText; // Adăugat pentru a afișa viața curentă și maximă
    public PlayerStats playerStats;
    public HealthBar healthBar;
    void Start()
    {
        if (playerStats != null && healthBar != null)
        {
            healthBar.SetMaxHealth(playerStats.maxHealth);
            healthBar.SetHealth(playerStats.currentHealth);
        }
    }

    void Update()
    {
        if (playerStats != null && healthBar != null)
        {
            healthBar.SetMaxHealth(playerStats.maxHealth);
            healthBar.SetHealth(playerStats.currentHealth);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
        UpdateHealthText(health, health);
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        UpdateHealthText(health, (int)slider.maxValue);
    }

    private void UpdateHealthText(int currentHealth, int maxHealth)
    {
        healthText.text = currentHealth + " / " + maxHealth;
    }
}