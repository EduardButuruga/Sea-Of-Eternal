using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text healthText; // Adăugat pentru a afișa viața curentă și maximă
    public BossHealth bossHealth; // Referință la componenta BossHealth

    void Start()
    {
        if (bossHealth != null)
        {
            SetMaxHealth((int)bossHealth.baseHealth);
            SetHealth((int)bossHealth.currentHealth);
        }
    }

    void Update()
    {
        if (bossHealth != null)
        {
            SetHealth((int)bossHealth.currentHealth);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);      
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);       
    }
   
}