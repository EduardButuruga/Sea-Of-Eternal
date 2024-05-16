using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10;
    public int health;

    public HealthBar healthBar;
    private Animator animator;

    public static PlayerHealth Instance { get; private set; }
    private bool isDead;
    public Rigidbody2D rb;

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
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    public void TakeDamage(int damage, Vector3 enemypos)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (animator != null)
        {
            animator.SetTrigger("hurt");
        }

        if (health <= 0 && !isDead)
        {
            isDead = true;
            // Poți adăuga logică pentru când jucătorul moare, de exemplu reîncărcarea scenei
            Debug.Log("Player is dead!");
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Decomentariu pentru a reîncărca scena
        }
    }
}