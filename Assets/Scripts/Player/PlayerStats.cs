using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Permanent Stats Reference")]
    public PlayerStatsPort permanentStats;


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
        dmgMultiplier += amount;
    }

    public void IncreasePickupRadius(float amount)
    {
        pickupRadius += amount;
    }

    public void IncreaseMaxHealth(int amount)
    {
        maxHealth += amount;
        currentHealth += amount; // Opțional: Ajustați și sănătatea curentă
    }

    public void DecreaseBarrelCooldown(float amount)
    {
        barrelCooldown -= amount;
        if (barrelCooldown < 0) barrelCooldown = 0; // Asigurați-vă că cooldown-ul nu devine negativ
    }

    // Double Cannon Stats
    public void IncreaseDoubleCannonDamage(float amount)
    {
        damage += amount;
    }

    public void IncreaseDoubleCannonAttackSpeed(float amount)
    {
        attackSpeed += amount;
    }

    public void IncreaseDoubleCannonCriticalStrikeChance(float amount)
    {
        criticalStrikeChance += amount;
        if (criticalStrikeChance > 1f) criticalStrikeChance = 1f; // Asigurați-vă că șansa de critical nu depășește 100%
    }

    public void IncreaseDoubleCannonCriticalDamageMultiplier(float amount)
    {
        criticalDamageMultiplier += amount;
    }

    public void IncreaseDoubleCannonBulletsPerSide(int amount)
    {
        bulletsPerSide += amount;
    }

    public void IncreaseDoubleCannonCannonballSpeed(float amount)
    {
        cannonballSpeed += amount;
    }

    // Hand Cannon Stats
    public void IncreaseHandCannonDamage(float amount)
    {
        HandCannonDamage += amount;
    }

    public void IncreaseHandCannonAttackSpeed(float amount)
    {
        HandCannonAttackSpeed += amount;
    }

    public void IncreaseHandCannonCannonballSpeed(float amount)
    {
        HandCannonCannonballSpeed += amount;
    }

    public void IncreaseHandCannonCriticalStrikeChance(float amount)
    {
        HandCannonCriticalStrikeChance += amount;
        if (HandCannonCriticalStrikeChance > 1f) HandCannonCriticalStrikeChance = 1f; // Asigurați-vă că șansa de critical nu depășește 100%
    }

    public void IncreaseHandCannonCriticalDamageMultiplier(float amount)
    {
        HandCannonCriticalDamageMultiplier += amount;
    }

    public void IncreaseBarrelDamage(float amount)
    {
        BarrelDamage += amount;
    }

    public void IncreaseBarrelExplosionRadius(float amount)
    {
        BarrelExplosionRadius += amount;
    }

    public void IncreaseBarrelCooldown(float amount)
    {
        barrelCooldown += amount;
        if(barrelCooldown < 0f)
            barrelCooldown = 0.5f;
    }

    public void IncreaseMoveSpeed(float amount)
    {
        moveSpeed += amount;
        if(moveSpeed < 0f) 
            moveSpeed = 5f;
    }

    public void CopyStatsFromPermanent()
    {
        luck = permanentStats.luck;
        armor = permanentStats.armor;
        lifeRegen = permanentStats.lifeRegen;
        xpMultiplier = permanentStats.xpMultiplier;
        dmgMultiplier = permanentStats.dmgMultiplier;
        pickupRadius = permanentStats.pickupRadius;
        maxHealth = permanentStats.maxHealth;
        currentHealth = permanentStats.currentHealth;
        moveSpeed = permanentStats.moveSpeed;

        damage = permanentStats.damage;
        attackSpeed = permanentStats.attackSpeed;
        criticalStrikeChance = permanentStats.criticalStrikeChance;
        criticalDamageMultiplier = permanentStats.criticalDamageMultiplier;
        bulletsPerSide = permanentStats.bulletsPerSide;
        cannonballSpeed = permanentStats.cannonballSpeed;

        HandCannonDamage = permanentStats.HandCannonDamage;
        HandCannonAttackSpeed = permanentStats.HandCannonAttackSpeed;
        HandCannonCannonballSpeed = permanentStats.HandCannonCannonballSpeed;
        HandCannonCriticalStrikeChance = permanentStats.HandCannonCriticalStrikeChance;
        HandCannonCriticalDamageMultiplier = permanentStats.HandCannonCriticalDamageMultiplier;

        BarrelDamage = permanentStats.BarrelDamage;
        BarrelExplosionRadius = permanentStats.BarrelExplosionRadius;
        barrelCooldown = permanentStats.barrelCooldown;

        launchSpeed = permanentStats.launchSpeed;
        returnSpeed = permanentStats.returnSpeed;
        maxDistance = permanentStats.maxDistance;
    }
   
}
