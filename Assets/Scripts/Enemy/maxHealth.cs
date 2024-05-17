using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxHealth : MonoBehaviour
{
    public float baseHealth = 100f;
    private float currentHealth;
    private float Health;
    public Vector3 damagePopupOffset = new Vector3(0, 1, 0); // Offset pentru poziția popup-ului

    void Start()
    {
        Health = baseHealth;
        currentHealth = Health;
    }

    public void TakeDamage(float amount, bool isCriticalHit)
    {
        currentHealth -= amount;
        DamagePopUP.Create(transform.position, (int)amount, isCriticalHit, damagePopupOffset);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeFlatDamage(float amount)
    {
        currentHealth -= amount;
        DamagePopUP.Create(transform.position, (int)amount, damagePopupOffset);

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