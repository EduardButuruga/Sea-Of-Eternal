using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxHealth : MonoBehaviour
{
    public float baseHealth = 100f;
    public int xpValue = 20;
    private PlayerXp playerXp;

    private float currentHealth;
    private float Health;
    public GameObject coinPrefab;
    public int minCoins = 1; // Numărul minim de monede droppate
    public int maxCoins = 5;// Numărul de monede droppate
    public float dropRadius = 0.5f;
    public Vector3 damagePopupOffset = new Vector3(0, 1, 0); // Offset pentru poziția popup-ului

    void Start()
    {
        playerXp = GameObject.FindWithTag("Player").GetComponent<PlayerXp>();
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
        if (playerXp != null)
        {
            playerXp.AddXP(xpValue);
        }

        DropCoins();
        Destroy(gameObject);
        // Sau alte efecte la moarte, de exemplu: animații, particule etc.
    }

    void DropCoins()
    {
        int coinDropCount = Random.Range(minCoins, maxCoins + 1);
        for (int i = 0; i < coinDropCount; i++)
        {
            Vector2 dropPosition = GetRandomDropPosition();
            Instantiate(coinPrefab, dropPosition, Quaternion.identity);
        }
    }

    Vector2 GetRandomDropPosition()
    {
        // Adaugă un offset random în jurul poziției inamicului
        Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
        return (Vector2)transform.position + randomOffset;
    }
}