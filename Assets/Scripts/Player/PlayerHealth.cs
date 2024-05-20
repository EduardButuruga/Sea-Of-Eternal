using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    private Animator animator;
    public PlayerStats playerStats;
    public DamageFlicker damageFlicker;
    public GameObject respawnUI;
    public PlayerXp playerXp;

    public static PlayerHealth Instance { get; private set; }
    private bool isDead;
    private bool isInvulnerable;
    public float invulnerabilityDuration = 0.10f; // Durata invulnerabilității în secunde
    public Rigidbody2D rb;
    public EnemyColliderManager colliderManager; // Referință la DirectionalColliderManager
    public SpriteRenderer spriteRenderer;
    public StartWaves startWaves; // Referință la scriptul StartWaves
    public GameObject port;
    public CannonController cannonController;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(10000, Vector3.zero);
        }
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Opțional: păstrează jucătorul între scene
        }
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        playerStats.currentHealth = playerStats.maxHealth;
        healthBar.SetMaxHealth(playerStats.maxHealth);
        StartCoroutine(HealthRegenCoroutine());
    }

    public void TakeDamage(int damage, Vector3 enemypos)
    {
        if (isInvulnerable || isDead)
            return;

        int trueDamage = damage - (int)playerStats.armor;
        if (trueDamage < 0)
        {
            trueDamage = 1;
        }
        Debug.Log(trueDamage);
        playerStats.currentHealth -= trueDamage;
        healthBar.SetHealth(playerStats.currentHealth);
        
        StartCoroutine(InvulnerabilityCoroutine());

        if (playerStats.currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("Player is dead!");
            Die();
        }
        else
        {
            if (damageFlicker != null)
            {
                damageFlicker.StartFlicker();
            }
        }
    }

    private void Die()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.Euler(0, 0, 0);
        cannonController.OnDeath();
        Time.timeScale = 0f;
        WaveManager.Instance.ResetWaves();       
        // Afișează UI de respawn
        if (respawnUI != null)
        {
            respawnUI.SetActive(true);
            // Afișează numărul de inamici omorâți
            Text enemiesKilledText = respawnUI.transform.Find("EnemiesKilledText").GetComponent<Text>();
            enemiesKilledText.text = "Enemies killed: " + EnemyManager.Instance.GetEnemiesKilledCount();

            Button respawnButton = respawnUI.transform.Find("RespawnButton").GetComponent<Button>();
            respawnButton.onClick.AddListener(() => Respawn());
        }     
    }

    public void Respawn()
    {
        playerStats.CopyStatsFromPermanent();
        playerXp.ResetLevel();
        Time.timeScale = 1f;      
        // Reinițializează sănătatea jucătorului
        playerStats.currentHealth = playerStats.maxHealth;
        healthBar.SetHealth(playerStats.currentHealth);

        // Reinițializează poziția jucătorului în port
        transform.position = new Vector3(-88f, -24.56f, 0f);

        // Reactivează flag-ul isInPort
        PlayerCtrl playerCtrl = GetComponent<PlayerCtrl>();
        if (playerCtrl != null)
        {
            playerCtrl.isInPort = true;
        }

        // Resetează flag-ul de moarte
        isDead = false;

        // Ascunde UI de respawn
        if (respawnUI != null)
        {
            respawnUI.SetActive(false);
        }

        if (startWaves != null)
        {
            startWaves.ResetActiv();
        }
        // Reîncepe regenerarea sănătății
        StartCoroutine(HealthRegenCoroutine());
        port.SetActive(true); 
        cannonController.ResetCannon();      
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private IEnumerator HealthRegenCoroutine()
    {
        while (true)
        {
            if (!isDead && playerStats.currentHealth < playerStats.maxHealth)
            {
                playerStats.currentHealth += Mathf.FloorToInt(playerStats.lifeRegen);
                playerStats.currentHealth = Mathf.Clamp(playerStats.currentHealth, 0, playerStats.maxHealth);
                healthBar.SetHealth(playerStats.currentHealth);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}