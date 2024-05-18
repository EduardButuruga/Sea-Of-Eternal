using UnityEngine;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    public PlayerStats playerStats;

    [Header("Hand Cannon UI Elements")]
    public GameObject handCannonPanel;
    public Text handCannonDamageText;
    public Text handCannonAttackSpeedText;
    public Text handCannonCannonballSpeedText;
    public Text handCannonCriticalStrikeChanceText;
    public Text handCannonCriticalDamageMultiplierText;

    [Header("Double Cannon UI Elements")]
    public GameObject doubleCannonPanel;
    public Text doubleCannonDamageText;
    public Text doubleCannonAttackSpeedText;
    public Text doubleCannonCannonballSpeedText;
    public Text doubleCannonCriticalStrikeChanceText;
    public Text doubleCannonCriticalDamageMultiplierText;
    public Text doubleCannonBulletsPerSideText;

    [Header("Barrel UI Elements")]
    public GameObject barrelPanel;
    public Text barrelDamageText;
    public Text barrelExplosionRadiusText;
    public Text barrelCooldownText;

    [Header("Harpoon UI Elements")]
    public GameObject harpoonPanel;
    public Text harpoonLaunchSpeedText;
    public Text harpoonReturnSpeedText;
    public Text harpoonMaxDistanceText;

    [Header("General UI Elements")]
    public GameObject generalPanel;
    public Text luckText;
    public Text armorText;
    public Text lifeRegenText;
    public Text xpMultiplierText;
    public Text dmgMultiplierText;
    public Text pickupRadiusText;
    public Text maxHealthText;
    public Text currentHealthText;
    public Text moveSpeedText;

    private GameObject activePanel;

    void Update()
    {
        UpdateAllStats();
    }

    public void UpdateAllStats()
    {
        UpdateHandCannonStats();
        UpdateDoubleCannonStats();
        UpdateBarrelStats();
        UpdateHarpoonStats();
        UpdateGeneralStats();
    }

    private void UpdateHandCannonStats()
    {
        handCannonDamageText.text = $"Damage: {playerStats.HandCannonDamage}";
        handCannonAttackSpeedText.text = $"Attack Speed: {playerStats.HandCannonAttackSpeed}";
        handCannonCannonballSpeedText.text = $"Cannonball Speed: {playerStats.HandCannonCannonballSpeed}";
        handCannonCriticalStrikeChanceText.text = $"Critical Chance: {playerStats.HandCannonCriticalStrikeChance * 100}%";
        handCannonCriticalDamageMultiplierText.text = $"Critical Damage Multiplier: {playerStats.HandCannonCriticalDamageMultiplier}";
    }

    private void UpdateDoubleCannonStats()
    {
        doubleCannonDamageText.text = $"Damage: {playerStats.damage}";
        doubleCannonAttackSpeedText.text = $"Attack Speed: {playerStats.attackSpeed}";
        doubleCannonCannonballSpeedText.text = $"Cannonball Speed: {playerStats.cannonballSpeed}";
        doubleCannonCriticalStrikeChanceText.text = $"Critical Chance: {playerStats.criticalStrikeChance * 100}%";
        doubleCannonCriticalDamageMultiplierText.text = $"Critical Damage Multiplier: {playerStats.criticalDamageMultiplier}";
        doubleCannonBulletsPerSideText.text = $"Bullets Per Side: {playerStats.bulletsPerSide}";
    }

    private void UpdateBarrelStats()
    {
        barrelDamageText.text = $"Damage: {playerStats.BarrelDamage}";
        barrelExplosionRadiusText.text = $"Explosion Radius: {playerStats.BarrelExplosionRadius}";
        barrelCooldownText.text = $"Cooldown: {playerStats.barrelCooldown}";
    }

    private void UpdateHarpoonStats()
    {
        harpoonLaunchSpeedText.text = $"Launch Speed: {playerStats.launchSpeed}";
        harpoonReturnSpeedText.text = $"Return Speed: {playerStats.returnSpeed}";
        harpoonMaxDistanceText.text = $"Max Distance: {playerStats.maxDistance}";
    }

    private void UpdateGeneralStats()
    {
        luckText.text = $"Luck: {playerStats.luck}";
        armorText.text = $"Armor: {playerStats.armor}";
        lifeRegenText.text = $"Life Regen: {playerStats.lifeRegen}";
        xpMultiplierText.text = $"XP Multiplier: {playerStats.xpMultiplier}";
        dmgMultiplierText.text = $"Damage Multiplier: {playerStats.dmgMultiplier}";
        pickupRadiusText.text = $"Pickup Radius: {playerStats.pickupRadius}";
        maxHealthText.text = $"Max Health: {playerStats.maxHealth}";
        currentHealthText.text = $"Current Health: {playerStats.currentHealth}";
        moveSpeedText.text = $"Move Speed: {playerStats.moveSpeed}";
    }

    public void ToggleHandCannonStats()
    {
        TogglePanel(handCannonPanel);
    }

    public void ToggleDoubleCannonStats()
    {
        TogglePanel(doubleCannonPanel);
    }

    public void ToggleBarrelStats()
    {
        TogglePanel(barrelPanel);
    }

    public void ToggleHarpoonStats()
    {
        TogglePanel(harpoonPanel);
    }

    public void ToggleGeneralStats()
    {
        TogglePanel(generalPanel);
    }

    private void TogglePanel(GameObject panel)
    {
        if (activePanel == panel)
        {
            panel.SetActive(!panel.activeSelf);
        }
        else
        {
            if (activePanel != null)
            {
                activePanel.SetActive(false);
            }
            panel.SetActive(true);
            activePanel = panel;
        }
    }
}