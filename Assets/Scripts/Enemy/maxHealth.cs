using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxHealth : MonoBehaviour
{
    public float baseHealth = 100f;
    private float currentHealth;
    private float Health;

    void Start()
    {
        Health = baseHealth;
        currentHealth = Health;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void IncreaseHealth(int waveNumber)
    {
        //20%
        Health = baseHealth * Mathf.Pow(1.2f, waveNumber);
        currentHealth = Health;
    }

    private void Die()
    {
        Destroy(gameObject);
        // Sau alte efecte la moarte, de exemplu: animații, particule etc.
    }
}
