using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // Player Stats
    public float luck;
    public float armor;
    public float lifeRegen;
    public float xpMultiplier;
    public float dmgMultiplier;
    public float pickupRadius;
    public int maxHealth;
    public int currentHealth;
    public float moveSpeed;

    // Double Cannon Stats
    public float damage;
    public float attackSpeed;
    public float criticalStrikeChance;
    public float criticalDamageMultiplier;
    public int bulletsPerSide;
    public float cannonballSpeed;

    // Cannon Stats
    public float HandCannonDamage;
    public float HandCannonAttackSpeed;
    public float HandCannonCannonballSpeed;
    public float HandCannonCriticalStrikeChance;
    public float HandCannonCriticalDamageMultiplier;

    // Barrel Stats
    public float BarrelDamage;
    public float BarrelExplosionRadius;
    public float barrelCooldown;

    // Harpoon Stats
    public float launchSpeed;
    public float returnSpeed;
    public float maxDistance;

    // Unlocked Weapons
    public List<string> unlockedWeapons = new List<string>();

    // Shop Prices
    public ArtilleryUpgradesData artilleryUpgradesData;
    public BarrelFactoryUpgradesData barrelFactoryUpgradesData;
    public HarpoonStationUpgradesData harpoonStationUpgradesData;
    public ArsenalHubUpgradesData arsenalHubUpgradesData;
}

[System.Serializable]
public class ArtilleryUpgradesData
{
    public int damagePrice;
    public int attackSpeedPrice;
    public int attackVelocityPrice;
    public int critChancePrice;
    public int critMultiplierPrice;
}

[System.Serializable]
public class BarrelFactoryUpgradesData
{
    public int damagePrice;
    public int boomRadiusPrice;
    public int barrelCooldownPrice;
}

[System.Serializable]
public class HarpoonStationUpgradesData
{
    public int launchSpeedPrice;
    public int returnSpeedPrice;
    public int maxDistancePrice;
}

[System.Serializable]
public class ArsenalHubUpgradesData
{
    public int damagePrice;
    public int attackSpeedPrice;
    public int critChancePrice;
    public int critMultiplierPrice;
    public int bulletsPerSidePrice;
    public int attackVelocityPrice;
}

