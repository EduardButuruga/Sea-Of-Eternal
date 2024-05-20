using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public PlayerStats playerStats;
    public Prices prices;

    [Header("Upgrade Values")]
    public int artilleryDamageIncrease;
    public float artilleryAttackSpeedIncrease;
    public int artilleryAttackVelocityIncrease;
    public float artilleryCritChanceIncrease;
    public float artilleryCritMultiplierIncrease;

    public int barrelDamageIncrease;
    public float barrelExplosionRadiusIncrease;
    public float barrelCooldownDecrease;

    public int harpoonLaunchSpeedIncrease;
    public int harpoonReturnSpeedIncrease;
    public int harpoonMaxDistanceIncrease;

    public int arsenalDamageIncrease;
    public int arsenalAttackSpeedIncrease;
    public int arsenalCritChanceIncrease;
    public float arsenalCritMultiplierIncrease;
    public int arsenalBulletsPerSideIncrease;
    public int arsenalAttackVelocityIncrease;

    public void BuyArtilleryDamage()
    {
        if (CanAfford(prices.artilleryUpgrades.damagePrice))
        {
            CoinManager.instance.AddCoin(-prices.artilleryUpgrades.damagePrice);
            playerStats.HandCannonDamage += artilleryDamageIncrease;
            UpdateText(prices.artilleryUpgrades.damagePriceText, playerStats.HandCannonDamage);
            IncreasePrice(ref prices.artilleryUpgrades.damagePrice, prices.artilleryUpgrades.damagePriceIncrease, prices.artilleryUpgrades.isDamagePriceIncreasePercentage);
        }
    }

    public void BuyArtilleryAttackSpeed()
    {
        if (CanAfford(prices.artilleryUpgrades.attackSpeedPrice))
        {
            CoinManager.instance.AddCoin(-prices.artilleryUpgrades.attackSpeedPrice);
            playerStats.HandCannonAttackSpeed += artilleryAttackSpeedIncrease;
            UpdateText(prices.artilleryUpgrades.attackSpeedPriceText, playerStats.HandCannonAttackSpeed);
            IncreasePrice(ref prices.artilleryUpgrades.attackSpeedPrice, prices.artilleryUpgrades.attackSpeedPriceIncrease, prices.artilleryUpgrades.isAttackSpeedPriceIncreasePercentage);
        }
    }

    public void BuyArtilleryAttackVelocity()
    {
        if (CanAfford(prices.artilleryUpgrades.attackVelocityPrice))
        {
            CoinManager.instance.AddCoin(-prices.artilleryUpgrades.attackVelocityPrice);
            playerStats.HandCannonCannonballSpeed += artilleryAttackVelocityIncrease;
            UpdateText(prices.artilleryUpgrades.attackVelocityPriceText, playerStats.HandCannonCannonballSpeed);
            IncreasePrice(ref prices.artilleryUpgrades.attackVelocityPrice, prices.artilleryUpgrades.attackVelocityPriceIncrease, prices.artilleryUpgrades.isAttackVelocityPriceIncreasePercentage);
        }
    }

    public void BuyArtilleryCritChance()
    {
        if (CanAfford(prices.artilleryUpgrades.critChancePrice))
        {
            CoinManager.instance.AddCoin(-prices.artilleryUpgrades.critChancePrice);
            playerStats.HandCannonCriticalStrikeChance += artilleryCritChanceIncrease;
            UpdateText(prices.artilleryUpgrades.critChancePriceText, playerStats.HandCannonCriticalStrikeChance);
            IncreasePrice(ref prices.artilleryUpgrades.critChancePrice, prices.artilleryUpgrades.critChancePriceIncrease, prices.artilleryUpgrades.isCritChancePriceIncreasePercentage);
        }
    }

    public void BuyArtilleryCritMultiplier()
    {
        if (CanAfford(prices.artilleryUpgrades.critMultiplierPrice))
        {
            CoinManager.instance.AddCoin(-prices.artilleryUpgrades.critMultiplierPrice);
            playerStats.HandCannonCriticalDamageMultiplier += artilleryCritMultiplierIncrease;
            UpdateText(prices.artilleryUpgrades.critMultiplierPriceText, playerStats.HandCannonCriticalDamageMultiplier);
            IncreasePrice(ref prices.artilleryUpgrades.critMultiplierPrice, prices.artilleryUpgrades.critMultiplierPriceIncrease, prices.artilleryUpgrades.isCritMultiplierPriceIncreasePercentage);
        }
    }

    public void BuyBarrelDamage()
    {
        if (CanAfford(prices.barrelFactoryUpgrades.damagePrice))
        {
            CoinManager.instance.AddCoin(-prices.barrelFactoryUpgrades.damagePrice);
            playerStats.BarrelDamage += barrelDamageIncrease;
            UpdateText(prices.barrelFactoryUpgrades.damagePriceText, playerStats.BarrelDamage);
            IncreasePrice(ref prices.barrelFactoryUpgrades.damagePrice, prices.barrelFactoryUpgrades.damagePriceIncrease, prices.barrelFactoryUpgrades.isDamagePriceIncreasePercentage);
        }
    }

    public void BuyBarrelExplosionRadius()
    {
        if (CanAfford(prices.barrelFactoryUpgrades.boomRadiusPrice))
        {
            CoinManager.instance.AddCoin(-prices.barrelFactoryUpgrades.boomRadiusPrice);
            playerStats.BarrelExplosionRadius += barrelExplosionRadiusIncrease;
            UpdateText(prices.barrelFactoryUpgrades.boomRadiusPriceText, playerStats.BarrelExplosionRadius);
            IncreasePrice(ref prices.barrelFactoryUpgrades.boomRadiusPrice, prices.barrelFactoryUpgrades.boomRadiusPriceIncrease, prices.barrelFactoryUpgrades.isBoomRadiusPriceIncreasePercentage);
        }
    }

    public void BuyBarrelCooldown()
    {
        if (CanAfford(prices.barrelFactoryUpgrades.barrelCooldownPrice))
        {
            CoinManager.instance.AddCoin(-prices.barrelFactoryUpgrades.barrelCooldownPrice);
            playerStats.barrelCooldown -= barrelCooldownDecrease;
            UpdateText(prices.barrelFactoryUpgrades.barrelCooldownPriceText, playerStats.barrelCooldown);
            IncreasePrice(ref prices.barrelFactoryUpgrades.barrelCooldownPrice, prices.barrelFactoryUpgrades.barrelCooldownPriceIncrease, prices.barrelFactoryUpgrades.isBarrelCooldownPriceIncreasePercentage);
        }
    }

    public void BuyHarpoonLaunchSpeed()
    {
        if (CanAfford(prices.harpoonStationUpgrades.launchSpeedPrice))
        {
            CoinManager.instance.AddCoin(-prices.harpoonStationUpgrades.launchSpeedPrice);
            playerStats.launchSpeed += harpoonLaunchSpeedIncrease;
            UpdateText(prices.harpoonStationUpgrades.launchSpeedPriceText, playerStats.launchSpeed);
            IncreasePrice(ref prices.harpoonStationUpgrades.launchSpeedPrice, prices.harpoonStationUpgrades.launchSpeedPriceIncrease, prices.harpoonStationUpgrades.isLaunchSpeedPriceIncreasePercentage);
        }
    }

    public void BuyHarpoonReturnSpeed()
    {
        if (CanAfford(prices.harpoonStationUpgrades.returnSpeedPrice))
        {
            CoinManager.instance.AddCoin(-prices.harpoonStationUpgrades.returnSpeedPrice);
            playerStats.returnSpeed += harpoonReturnSpeedIncrease;
            UpdateText(prices.harpoonStationUpgrades.returnSpeedPriceText, playerStats.returnSpeed);
            IncreasePrice(ref prices.harpoonStationUpgrades.returnSpeedPrice, prices.harpoonStationUpgrades.returnSpeedPriceIncrease, prices.harpoonStationUpgrades.isReturnSpeedPriceIncreasePercentage);
        }
    }

    public void BuyHarpoonMaxDistance()
    {
        if (CanAfford(prices.harpoonStationUpgrades.maxDistancePrice))
        {
            CoinManager.instance.AddCoin(-prices.harpoonStationUpgrades.maxDistancePrice);
            playerStats.maxDistance += harpoonMaxDistanceIncrease;
            UpdateText(prices.harpoonStationUpgrades.maxDistancePriceText, playerStats.maxDistance);
            IncreasePrice(ref prices.harpoonStationUpgrades.maxDistancePrice, prices.harpoonStationUpgrades.maxDistancePriceIncrease, prices.harpoonStationUpgrades.isMaxDistancePriceIncreasePercentage);
        }
    }

    public void BuyArsenalDamage()
    {
        if (CanAfford(prices.arsenalHubUpgrades.damagePrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.damagePrice);
            playerStats.damage += arsenalDamageIncrease;
            UpdateText(prices.arsenalHubUpgrades.damagePriceText, playerStats.damage);
            IncreasePrice(ref prices.arsenalHubUpgrades.damagePrice, prices.arsenalHubUpgrades.damagePriceIncrease, prices.arsenalHubUpgrades.isDamagePriceIncreasePercentage);
        }
    }

    public void BuyArsenalAttackSpeed()
    {
        if (CanAfford(prices.arsenalHubUpgrades.attackSpeedPrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.attackSpeedPrice);
            playerStats.attackSpeed += arsenalAttackSpeedIncrease;
            UpdateText(prices.arsenalHubUpgrades.attackSpeedPriceText, playerStats.attackSpeed);
            IncreasePrice(ref prices.arsenalHubUpgrades.attackSpeedPrice, prices.arsenalHubUpgrades.attackSpeedPriceIncrease, prices.arsenalHubUpgrades.isAttackSpeedPriceIncreasePercentage);
        }
    }

    public void BuyArsenalCritChance()
    {
        if (CanAfford(prices.arsenalHubUpgrades.critChancePrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.critChancePrice);
            playerStats.criticalStrikeChance += arsenalCritChanceIncrease;
            UpdateText(prices.arsenalHubUpgrades.critChancePriceText, playerStats.criticalStrikeChance);
            IncreasePrice(ref prices.arsenalHubUpgrades.critChancePrice, prices.arsenalHubUpgrades.critChancePriceIncrease, prices.arsenalHubUpgrades.isCritChancePriceIncreasePercentage);
        }
    }

    public void BuyArsenalCritMultiplier()
    {
        if (CanAfford(prices.arsenalHubUpgrades.critMultiplierPrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.critMultiplierPrice);
            playerStats.criticalDamageMultiplier += arsenalCritMultiplierIncrease;
            UpdateText(prices.arsenalHubUpgrades.critMultiplierPriceText, playerStats.criticalDamageMultiplier);
            IncreasePrice(ref prices.arsenalHubUpgrades.critMultiplierPrice, prices.arsenalHubUpgrades.critMultiplierPriceIncrease, prices.arsenalHubUpgrades.isCritMultiplierPriceIncreasePercentage);
        }
    }

    public void BuyArsenalBulletsPerSide()
    {
        if (CanAfford(prices.arsenalHubUpgrades.bulletsPerSidePrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.bulletsPerSidePrice);
            playerStats.bulletsPerSide += arsenalBulletsPerSideIncrease;
            UpdateText(prices.arsenalHubUpgrades.bulletsPerSidePriceText, playerStats.bulletsPerSide);
            IncreasePrice(ref prices.arsenalHubUpgrades.bulletsPerSidePrice, prices.arsenalHubUpgrades.bulletsPerSidePriceIncrease, prices.arsenalHubUpgrades.isBulletsPerSidePriceIncreasePercentage);
        }
    }

    public void BuyArsenalAttackVelocity()
    {
        if (CanAfford(prices.arsenalHubUpgrades.attackVelocityPrice))
        {
            CoinManager.instance.AddCoin(-prices.arsenalHubUpgrades.attackVelocityPrice);
            playerStats.cannonballSpeed += arsenalAttackVelocityIncrease;
            UpdateText(prices.arsenalHubUpgrades.attackVelocityPriceText, playerStats.cannonballSpeed);
            IncreasePrice(ref prices.arsenalHubUpgrades.attackVelocityPrice, prices.arsenalHubUpgrades.attackVelocityPriceIncrease, prices.arsenalHubUpgrades.isAttackVelocityPriceIncreasePercentage);
        }
    }

    private bool CanAfford(int price)
    {
        return CoinManager.instance.GetCoins() >= price;
    }

    private void IncreasePrice(ref int price, float increase, bool isPercentage)
    {
        if (isPercentage)
        {
            price = Mathf.RoundToInt(price * (1 + increase / 100));
        }
        else
        {
            price += Mathf.RoundToInt(increase);
        }
    }

    private void UpdateText(Text textComponent, float value)
    {
        textComponent.text = value.ToString();
    }

    private void UpdateText(Text textComponent, int value)
    {
        textComponent.text = value.ToString();
    }
}