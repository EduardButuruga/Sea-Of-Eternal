using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsPort : MonoBehaviour
{
    [Header("Player Stats")]
    public float luck = 0f;
    public float armor = 0f;
    public float lifeRegen = 0f;
    public float xpMultiplier = 1.0f;
    public float dmgMultiplier = 1.0f;
    public float pickupRadius = 5.0f;
    public int maxHealth = 10;
    public int currentHealth;
    public float moveSpeed = 2f;

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
    public float barrelCooldown = 10f;


    [Header("Harpon Stats")]
    public float launchSpeed = 10f;
    public float returnSpeed = 5f;
    public float maxDistance = 10f;
}
