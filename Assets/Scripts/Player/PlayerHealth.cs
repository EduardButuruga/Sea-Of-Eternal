using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthBar;
    private Animator animator;
    public PlayerStats playerStats;
    public DamageFlicker damageFlicker;

    public static PlayerHealth Instance { get; private set; }
    private bool isDead;
    private bool isInvulnerable;
    public float invulnerabilityDuration = 0.10f; // Durata invulnerabilității în secunde
    public Rigidbody2D rb;
    public EnemyColliderManager colliderManager; // Referință la DirectionalColliderManager
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); // Optional: Keep the player across scenes
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

        var trueDamage = damage - playerStats.armor;
        playerStats.currentHealth -= damage;
        healthBar.SetHealth(playerStats.currentHealth);
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }

        StartCoroutine(InvulnerabilityCoroutine());

        if (playerStats.currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Debug.Log("Player is dead!");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Decomentează pentru a reîncărca scena
        }
        else
        {
            if (damageFlicker != null)
            {
                damageFlicker.StartFlicker();
            }
        }
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