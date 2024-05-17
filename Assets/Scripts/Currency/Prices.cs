using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ArtilleryUpgrades
{
    [Header("Artillery Upgrades")]
    public int damagePrice;
    public Text damagePriceText;
    public float damagePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isDamagePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int attackSpeedPrice;
    public Text attackSpeedPriceText;
    public float attackSpeedPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isAttackSpeedPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int attackVelocityPrice;
    public Text attackVelocityPriceText;
    public float attackVelocityPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isAttackVelocityPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int critChancePrice;
    public Text critChancePriceText;
    public float critChancePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isCritChancePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int critMultiplierPrice;
    public Text critMultiplierPriceText;
    public float critMultiplierPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isCritMultiplierPriceIncreasePercentage; // Determină dacă creșterea este procentuală
}

[System.Serializable]
public class BarrelFactoryUpgrades
{
    [Header("Barrel Factory Upgrades")]
    public int damagePrice;
    public Text damagePriceText;
    public float damagePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isDamagePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int boomRadiusPrice;
    public Text boomRadiusPriceText;
    public float boomRadiusPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isBoomRadiusPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int barrelCooldownPrice;
    public Text barrelCooldownPriceText;
    public float barrelCooldownPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isBarrelCooldownPriceIncreasePercentage; // Determină dacă creșterea este procentuală
}

[System.Serializable]
public class HarpoonStationUpgrades
{
    [Header("Harpoon Station Upgrades")]
    public int launchSpeedPrice;
    public Text launchSpeedPriceText;
    public float launchSpeedPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isLaunchSpeedPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int returnSpeedPrice;
    public Text returnSpeedPriceText;
    public float returnSpeedPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isReturnSpeedPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int maxDistancePrice;
    public Text maxDistancePriceText;
    public float maxDistancePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isMaxDistancePriceIncreasePercentage; // Determină dacă creșterea este procentuală
}

[System.Serializable]
public class ArsenalHubUpgrades
{
    [Header("Arsenal Hub Upgrades")]
    public int damagePrice;
    public Text damagePriceText;
    public float damagePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isDamagePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int attackSpeedPrice;
    public Text attackSpeedPriceText;
    public float attackSpeedPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isAttackSpeedPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int critChancePrice;
    public Text critChancePriceText;
    public float critChancePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isCritChancePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int critMultiplierPrice;
    public Text critMultiplierPriceText;
    public float critMultiplierPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isCritMultiplierPriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int bulletsPerSidePrice;
    public Text bulletsPerSidePriceText;
    public float bulletsPerSidePriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isBulletsPerSidePriceIncreasePercentage; // Determină dacă creșterea este procentuală
    public int attackVelocityPrice;
    public Text attackVelocityPriceText;
    public float attackVelocityPriceIncrease; // Suma sau procentul cu care crește prețul după achiziție
    public bool isAttackVelocityPriceIncreasePercentage; // Determină dacă creșterea este procentuală
}

public class Prices : MonoBehaviour
{
    public ArtilleryUpgrades artilleryUpgrades;
    public BarrelFactoryUpgrades barrelFactoryUpgrades;
    public HarpoonStationUpgrades harpoonStationUpgrades;
    public ArsenalHubUpgrades arsenalHubUpgrades;

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Artillery Upgrades
        artilleryUpgrades.damagePriceText.text = artilleryUpgrades.damagePrice.ToString();
        artilleryUpgrades.attackSpeedPriceText.text = artilleryUpgrades.attackSpeedPrice.ToString();
        artilleryUpgrades.attackVelocityPriceText.text = artilleryUpgrades.attackVelocityPrice.ToString();
        artilleryUpgrades.critChancePriceText.text = artilleryUpgrades.critChancePrice.ToString();
        artilleryUpgrades.critMultiplierPriceText.text = artilleryUpgrades.critMultiplierPrice.ToString();

        // Barrel Factory Upgrades
        barrelFactoryUpgrades.damagePriceText.text = barrelFactoryUpgrades.damagePrice.ToString();
        barrelFactoryUpgrades.boomRadiusPriceText.text = barrelFactoryUpgrades.boomRadiusPrice.ToString();
        barrelFactoryUpgrades.barrelCooldownPriceText.text = barrelFactoryUpgrades.barrelCooldownPrice.ToString();

        // Harpoon Station Upgrades
        harpoonStationUpgrades.launchSpeedPriceText.text = harpoonStationUpgrades.launchSpeedPrice.ToString();
        harpoonStationUpgrades.returnSpeedPriceText.text = harpoonStationUpgrades.returnSpeedPrice.ToString();
        harpoonStationUpgrades.maxDistancePriceText.text = harpoonStationUpgrades.maxDistancePrice.ToString();

        // Arsenal Hub Upgrades
        arsenalHubUpgrades.damagePriceText.text = arsenalHubUpgrades.damagePrice.ToString();
        arsenalHubUpgrades.attackSpeedPriceText.text = arsenalHubUpgrades.attackSpeedPrice.ToString();
        arsenalHubUpgrades.critChancePriceText.text = arsenalHubUpgrades.critChancePrice.ToString();
        arsenalHubUpgrades.critMultiplierPriceText.text = arsenalHubUpgrades.critMultiplierPrice.ToString();
        arsenalHubUpgrades.bulletsPerSidePriceText.text = arsenalHubUpgrades.bulletsPerSidePrice.ToString();
        arsenalHubUpgrades.attackVelocityPriceText.text = arsenalHubUpgrades.attackVelocityPrice.ToString();
    }
}