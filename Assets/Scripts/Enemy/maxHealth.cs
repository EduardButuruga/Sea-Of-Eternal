using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maxHealth : MonoBehaviour
{
    public float baseHealth = 100f;
    public int xpValue = 20;
    public float healthGrowing = 1.2f;
    private PlayerXp playerXp;

    public float currentHealth;
    private float Health;
    public GameObject coinPrefab;
    public int minCoins = 1; // Numărul minim de monede droppate
    public int maxCoins = 5;
    public float dropRadius = 0.5f;
    public Vector3 damagePopupOffset = new Vector3(0, 1, 0); // Offset pentru poziția popup-ulu
    public Animator animator;
    public WaveManager waveManager;

    void Start()
    {
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        playerXp = GameObject.FindWithTag("Player").GetComponent<PlayerXp>();
        Health = baseHealth * Mathf.Pow(healthGrowing, waveManager.currentWave-1);
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
        animator.SetTrigger("Die");
        if (playerXp != null)
        {
            playerXp.AddXP(xpValue);
        }

        DropCoins();
        Destroy(gameObject, 0.5f);
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