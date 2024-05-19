using UnityEngine;

public class BossChase : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 0.5f; // Viteza de rotație mai mică pentru o barcă uriașă
    public GameObject player;
    public Animator animator;
    private Vector2 currentDirection;
    private maxHealth health;

    public EnemyColliderManager colliderManager; // Referință la DirectionalColliderManager
    public SpriteRenderer spriteRenderer; // Referință la SpriteRenderer

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = GetComponent<maxHealth>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player object is tagged 'Player'.");
            return;
        }

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.freezeRotation = true;
        }

        health.IncreaseHealth(1);
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        currentDirection = Vector2.Lerp(currentDirection, directionToPlayer, rotationSpeed * Time.deltaTime);
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + currentDirection, speed * Time.deltaTime);
        UpdateAnimator(currentDirection);
        UpdateColliderDirection();
    }

    void UpdateAnimator(Vector2 direction)
    {
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.magnitude);
    }

    void UpdateColliderDirection()
    {
        string spriteName = spriteRenderer.sprite.name;
        colliderManager.SetDirection(spriteName);
    }
}