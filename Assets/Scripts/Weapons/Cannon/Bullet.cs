using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    private float damage;
    private bool isCriticalHit;
    public int maxPierceCount = 1; // Max enemies to pierce through for normal hits
    public int maxPierceCountCritical = 3; // Max enemies to pierce through for critical hits
    private int pierceCount = 0; // Counter for pierced enemies

    private void Awake()
    {
        Destroy(gameObject, life);
    }

    public void SetDamage(float damage, bool isCriticalHit)
    {
        this.damage = damage;
        this.isCriticalHit = isCriticalHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Debug.Log("Bullet hit enemy with damage: " + this.damage); // Debug message

            maxHealth enemyHealth = collision.GetComponent<maxHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(this.damage, isCriticalHit);
            }
            else
            {
                BossHealth bossHealth = collision.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(this.damage, isCriticalHit);
                }
                GhostBossHealth GbossHealth = collision.GetComponent<GhostBossHealth>();
                if (GbossHealth != null)
                {
                    GbossHealth.TakeDamage(this.damage, isCriticalHit);
                }
            }

            pierceCount++;

            // Determine the max number of enemies the bullet can pierce through
            int currentMaxPierceCount = isCriticalHit ? maxPierceCountCritical : maxPierceCount;

            // Destroy the bullet if it has reached the max number of pierces
            if (pierceCount >= currentMaxPierceCount)
            {
                Destroy(gameObject);
            }
        }
    }
}