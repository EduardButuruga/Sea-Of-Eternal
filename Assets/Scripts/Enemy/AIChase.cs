using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;
    public GameObject player;
    public Animator animator;
    private Vector2 currentDirection;
    private Vector2 smoothedDirection;
    private maxHealth health;

    public EnemyColliderManager colliderManager; // Referință la DirectionalColliderManager
    public SpriteRenderer spriteRenderer; // Referință la SpriteRenderer
    public float optimalDistance = 5f; // Distanța optimă față de jucător
    public float directionChangeDelay = 0.5f; // Întârziere între schimbările de direcție
    public float smoothingSpeed = 0.1f; // Viteza de interpolare pentru direcție

    private float lastDirectionChangeTime;

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

        smoothedDirection = Vector2.zero;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);

        // Calculate the perpendicular direction for the lateral
        Vector2 lateralDirection = Vector2.Perpendicular(directionToPlayer).normalized;

        // Choose the lateral direction that is closer to the current facing direction
        if (Vector2.Dot(transform.right, lateralDirection) < 0)
        {
            lateralDirection = -lateralDirection;
        }

        // Change direction only if the time elapsed since the last change is greater than the delay
        if (Time.time - lastDirectionChangeTime > directionChangeDelay)
        {
            if (distanceToPlayer < optimalDistance)
            {
                currentDirection = -directionToPlayer + lateralDirection;
            }
            else if (distanceToPlayer > optimalDistance)
            {
                currentDirection = directionToPlayer + lateralDirection;
            }
            else
            {
                currentDirection = lateralDirection;
            }
            lastDirectionChangeTime = Time.time;
        }

        // Smoothly interpolate the direction
        smoothedDirection = Vector2.Lerp(smoothedDirection, currentDirection, smoothingSpeed);

        // Move the boat towards the new direction
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + smoothedDirection, speed * Time.deltaTime);

        UpdateAnimator(smoothedDirection);
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