using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Stats")]
    public float luck = 0f;
    public float armor = 0f;
    public float lifeRegen = 0f;
    public float xpMultiplier = 1.0f;
    public float dmgMultiplier = 1.0f;
    public float pickupRadius = 5.0f;
    public int maxHealth = 10;
    public float barrelCooldown = 3f;
    public int currentHealth;

    [Header("Double Cannon Stats")]
    public float damage = 10f; 
    public float attackSpeed = 1f; 
    public float criticalStrikeChance = 0.1f; 
    public float criticalDamageMultiplier = 2f; 
    public int bulletsPerSide = 1; 
    public float cannonballSpeed = 10f;

    [Header("Cannon Stats")]
    public float HandCannonDamage = 10f;
    public float HandCannonAttackSpeed = 1f;
    public float HandCannonCannonballSpeed = 10f;
    public float HandCannonCriticalStrikeChance = 0.1f;
    public float HandCannonCriticalDamageMultiplier = 2f;

    [Header("Barrel Stats")]
    public float BarrelDamage = 50f;
    public float BarrelExplosionRadius = 2f;

    public void IncreaseLuck(float amount)
    {
        luck += amount;
    }

    public void IncreaseArmor(float amount)
    {
        armor += amount;
    }

    public void IncreaseLifeRegen(float amount)
    {
        lifeRegen += amount;
    }

    public void IncreaseXpMultiplier(float amount)
    {
        xpMultiplier += amount;
    }

    public void IncreaseDmgMultiplier(float amount)
    {
        xpMultiplier += amount;
    }
}
