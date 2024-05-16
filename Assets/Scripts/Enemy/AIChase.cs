using UnityEngine;

public class AIChase : MonoBehaviour
{
    public float speed;

    public GameObject player;
    private Animator animator;
    private Vector2 currentDirection;
    private maxHealth health;

    void Start()
    {
        player = GameObject.FindWithTag("player");
        health = GetComponent<maxHealth>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player object is tagged 'Player'.");
            return;
        }

        animator = GetComponent<Animator>();

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
        currentDirection = directionToPlayer;      
        transform.position = Vector2.MoveTowards(transform.position, (Vector2)transform.position + currentDirection, speed * Time.deltaTime);      
        UpdateAnimator(currentDirection);
    }

    void UpdateAnimator(Vector2 direction)
    {             
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.magnitude);
    }
}