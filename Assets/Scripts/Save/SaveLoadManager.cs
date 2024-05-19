using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "gamedata.json");
    }

    public void SaveGame(PlayerStats playerStats, UnlockManager unlockManager, Prices prices)
    {
        GameData gameData = new GameData
        {
            luck = playerStats.luck,
            armor = playerStats.armor,
            lifeRegen = playerStats.lifeRegen,
            xpMultiplier = playerStats.xpMultiplier,
            dmgMultiplier = playerStats.dmgMultiplier,
            pickupRadius = playerStats.pickupRadius,
            maxHealth = playerStats.maxHealth,
            currentHealth = playerStats.currentHealth,
            moveSpeed = playerStats.moveSpeed,
            damage = playerStats.damage,
            attackSpeed = playerStats.attackSpeed,
            criticalStrikeChance = playerStats.criticalStrikeChance,
            criticalDamageMultiplier = playerStats.criticalDamageMultiplier,
            bulletsPerSide = playerStats.bulletsPerSide,
            cannonballSpeed = playerStats.cannonballSpeed,
            HandCannonDamage = playerStats.HandCannonDamage,
            HandCannonAttackSpeed = playerStats.HandCannonAttackSpeed,
            HandCannonCannonballSpeed = playerStats.HandCannonCannonballSpeed,
            HandCannonCriticalStrikeChance = playerStats.HandCannonCriticalStrikeChance,
            HandCannonCriticalDamageMultiplier = playerStats.HandCannonCriticalDamageMultiplier,
            BarrelDamage = playerStats.BarrelDamage,
            BarrelExplosionRadius = playerStats.BarrelExplosionRadius,
            barrelCooldown = playerStats.barrelCooldown,
            launchSpeed = playerStats.launchSpeed,
            returnSpeed = playerStats.returnSpeed,
            maxDistance = playerStats.maxDistance,
            artilleryUpgradesData = new ArtilleryUpgradesData
            {
                damagePrice = prices.artilleryUpgrades.damagePrice,
                attackSpeedPrice = prices.artilleryUpgrades.attackSpeedPrice,
                attackVelocityPrice = prices.artilleryUpgrades.attackVelocityPrice,
                critChancePrice = prices.artilleryUpgrades.critChancePrice,
                critMultiplierPrice = prices.artilleryUpgrades.critMultiplierPrice
            },
            barrelFactoryUpgradesData = new BarrelFactoryUpgradesData
            {
                damagePrice = prices.barrelFactoryUpgrades.damagePrice,
                boomRadiusPrice = prices.barrelFactoryUpgrades.boomRadiusPrice,
                barrelCooldownPrice = prices.barrelFactoryUpgrades.barrelCooldownPrice
            },
            harpoonStationUpgradesData = new HarpoonStationUpgradesData
            {
                launchSpeedPrice = prices.harpoonStationUpgrades.launchSpeedPrice,
                returnSpeedPrice = prices.harpoonStationUpgrades.returnSpeedPrice,
                maxDistancePrice = prices.harpoonStationUpgrades.maxDistancePrice
            },
            arsenalHubUpgradesData = new ArsenalHubUpgradesData
            {
                damagePrice = prices.arsenalHubUpgrades.damagePrice,
                attackSpeedPrice = prices.arsenalHubUpgrades.attackSpeedPrice,
                critChancePrice = prices.arsenalHubUpgrades.critChancePrice,
                critMultiplierPrice = prices.arsenalHubUpgrades.critMultiplierPrice,
                bulletsPerSidePrice = prices.arsenalHubUpgrades.bulletsPerSidePrice,
                attackVelocityPrice = prices.arsenalHubUpgrades.attackVelocityPrice
            }
        };

        foreach (var weapon in unlockManager.weapons)
        {
            if (!weapon.buyButton.gameObject.activeSelf)
            {
                gameData.unlockedWeapons.Add(weapon.name);
            }
        }

        string json = JsonUtility.ToJson(gameData, true);
        File.WriteAllText(filePath, json);
        Debug.Log("Game Saved");
    }

    public void LoadGame(PlayerStats playerStats, UnlockManager unlockManager, Prices prices)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            GameData gameData = JsonUtility.FromJson<GameData>(json);

            playerStats.luck = gameData.luck;
            playerStats.armor = gameData.armor;
            playerStats.lifeRegen = gameData.lifeRegen;
            playerStats.xpMultiplier = gameData.xpMultiplier;
            playerStats.dmgMultiplier = gameData.dmgMultiplier;
            playerStats.pickupRadius = gameData.pickupRadius;
            playerStats.maxHealth = gameData.maxHealth;
            playerStats.currentHealth = gameData.currentHealth;
            playerStats.moveSpeed = gameData.moveSpeed;
            playerStats.damage = gameData.damage;
            playerStats.attackSpeed = gameData.attackSpeed;
            playerStats.criticalStrikeChance = gameData.criticalStrikeChance;
            playerStats.criticalDamageMultiplier = gameData.criticalDamageMultiplier;
            playerStats.bulletsPerSide = gameData.bulletsPerSide;
            playerStats.cannonballSpeed = gameData.cannonballSpeed;
            playerStats.HandCannonDamage = gameData.HandCannonDamage;
            playerStats.HandCannonAttackSpeed = gameData.HandCannonAttackSpeed;
            playerStats.HandCannonCannonballSpeed = gameData.HandCannonCannonballSpeed;
            playerStats.HandCannonCriticalStrikeChance = gameData.HandCannonCriticalStrikeChance;
            playerStats.HandCannonCriticalDamageMultiplier = gameData.HandCannonCriticalDamageMultiplier;
            playerStats.BarrelDamage = gameData.BarrelDamage;
            playerStats.BarrelExplosionRadius = gameData.BarrelExplosionRadius;
            playerStats.barrelCooldown = gameData.barrelCooldown;
            playerStats.launchSpeed = gameData.launchSpeed;
            playerStats.returnSpeed = gameData.returnSpeed;
            playerStats.maxDistance = gameData.maxDistance;

            prices.artilleryUpgrades.damagePrice = gameData.artilleryUpgradesData.damagePrice;
            prices.artilleryUpgrades.attackSpeedPrice = gameData.artilleryUpgradesData.attackSpeedPrice;
            prices.artilleryUpgrades.attackVelocityPrice = gameData.artilleryUpgradesData.attackVelocityPrice;
            prices.artilleryUpgrades.critChancePrice = gameData.artilleryUpgradesData.critChancePrice;
            prices.artilleryUpgrades.critMultiplierPrice = gameData.artilleryUpgradesData.critMultiplierPrice;

            prices.barrelFactoryUpgrades.damagePrice = gameData.barrelFactoryUpgradesData.damagePrice;
            prices.barrelFactoryUpgrades.boomRadiusPrice = gameData.barrelFactoryUpgradesData.boomRadiusPrice;
            prices.barrelFactoryUpgrades.barrelCooldownPrice = gameData.barrelFactoryUpgradesData.barrelCooldownPrice;

            prices.harpoonStationUpgrades.launchSpeedPrice = gameData.harpoonStationUpgradesData.launchSpeedPrice;
            prices.harpoonStationUpgrades.returnSpeedPrice = gameData.harpoonStationUpgradesData.returnSpeedPrice;
            prices.harpoonStationUpgrades.maxDistancePrice = gameData.harpoonStationUpgradesData.maxDistancePrice;

            prices.arsenalHubUpgrades.damagePrice = gameData.arsenalHubUpgradesData.damagePrice;
            prices.arsenalHubUpgrades.attackSpeedPrice = gameData.arsenalHubUpgradesData.attackSpeedPrice;
            prices.arsenalHubUpgrades.critChancePrice = gameData.arsenalHubUpgradesData.critChancePrice;
            prices.arsenalHubUpgrades.critMultiplierPrice = gameData.arsenalHubUpgradesData.critMultiplierPrice;
            prices.arsenalHubUpgrades.bulletsPerSidePrice = gameData.arsenalHubUpgradesData.bulletsPerSidePrice;
            prices.arsenalHubUpgrades.attackVelocityPrice = gameData.arsenalHubUpgradesData.attackVelocityPrice;

            unlockManager.UpdateWeaponStates(gameData.unlockedWeapons);

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.LogError("Save file not found");
        }
    }
}