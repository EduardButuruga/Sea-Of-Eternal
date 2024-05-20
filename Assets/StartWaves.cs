using UnityEngine;

public class StartWaves : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerStatsPort playerStatsPort;
    public WaveManager waveManager;
    public bool Activ = true;
    public WaveTimer waveTimer;
    public PlayerCtrl playerController;
    public GameObject port;
    public float activationDistance = 5f;

    public GameObject player;
    public AudioSource audioSource;
    public AudioClip shootSound;

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= activationDistance)
        {
            TogglePort();
        }
    }

    void TogglePort()
    {
        if (port != null)
        {
            port.SetActive(!port.activeSelf);
        }
    }
    private void OnMouseDown()
    {
        SaveStatsToPort();
        Debug.Log("Stats Saved");
        Debug.Log("Clopot");
        if (Activ == true)
        {
            if (waveManager != null)
            {
                waveManager.StartWaves();
            }

            if (waveTimer != null)
            {
                waveTimer.StartTimer();
            }

            if (playerController != null)
            {
                playerController.isInPort = false;
            }

            if (audioSource != null && shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }
            Activ = false;
        }
    }
    public void ResetActiv()
    {
        Activ = true;
    }
    void SaveStatsToPort()
    {
        playerStatsPort.luck = playerStats.luck;
        playerStatsPort.armor = playerStats.armor;
        playerStatsPort.lifeRegen = playerStats.lifeRegen;
        playerStatsPort.xpMultiplier = playerStats.xpMultiplier;
        playerStatsPort.dmgMultiplier = playerStats.dmgMultiplier;
        playerStatsPort.pickupRadius = playerStats.pickupRadius;
        playerStatsPort.maxHealth = playerStats.maxHealth;
        playerStatsPort.currentHealth = playerStats.currentHealth;
        playerStatsPort.moveSpeed = playerStats.moveSpeed;

        playerStatsPort.damage = playerStats.damage;
        playerStatsPort.attackSpeed = playerStats.attackSpeed;
        playerStatsPort.criticalStrikeChance = playerStats.criticalStrikeChance;
        playerStatsPort.criticalDamageMultiplier = playerStats.criticalDamageMultiplier;
        playerStatsPort.bulletsPerSide = playerStats.bulletsPerSide;
        playerStatsPort.cannonballSpeed = playerStats.cannonballSpeed;

        playerStatsPort.HandCannonDamage = playerStats.HandCannonDamage;
        playerStatsPort.HandCannonAttackSpeed = playerStats.HandCannonAttackSpeed;
        playerStatsPort.HandCannonCannonballSpeed = playerStats.HandCannonCannonballSpeed;
        playerStatsPort.HandCannonCriticalStrikeChance = playerStats.HandCannonCriticalStrikeChance;
        playerStatsPort.HandCannonCriticalDamageMultiplier = playerStats.HandCannonCriticalDamageMultiplier;

        playerStatsPort.BarrelDamage = playerStats.BarrelDamage;
        playerStatsPort.BarrelExplosionRadius = playerStats.BarrelExplosionRadius;
        playerStatsPort.barrelCooldown = playerStats.barrelCooldown;

        playerStatsPort.launchSpeed = playerStats.launchSpeed;
        playerStatsPort.returnSpeed = playerStats.returnSpeed;
        playerStatsPort.maxDistance = playerStats.maxDistance;
    }
}