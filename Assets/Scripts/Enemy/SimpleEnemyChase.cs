using UnityEngine;

public class SimpleEnemyChase : MonoBehaviour
{
    public float speed = 5f;  // Speed of the enemy
    public GameObject player;  // Reference to the player

    private Rigidbody2D rb;  // Rigidbody2D component of the enemy
    private Vector2 movement;  // Movement direction
    private SpriteRenderer spriteRenderer; // SpriteRenderer component of the enemy

    void Start()
    {
        // Find the player object with tag "Player"
        player = GameObject.FindWithTag("Player");

        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Get the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player object is tagged 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector2 direction = (player.transform.position - transform.position).normalized;

            // Update the movement direction
            movement = direction;

            // Calculate the angle between the enemy's current direction and the player's direction
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the enemy to face the player
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Adjust angle based on your sprite's initial orientation

            // Flip the sprite on Y axis if needed
            if (Mathf.Abs(angle) > 90)
            {
                spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.flipY = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Move the enemy towards the player
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
    }
}