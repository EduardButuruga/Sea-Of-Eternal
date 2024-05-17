using UnityEngine;

public class Coin : MonoBehaviour
{
    private Transform playerTransform;
    public float magnetSpeed = 5f; // Viteza cu care moneda se apropie de jucător
    private bool isBeingAttracted = false; // Dacă moneda este atrasă către jucător

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isBeingAttracted && playerTransform != null)
        {
            MoveTowardsPlayer();
        }
    }

    public void StartAttraction(Transform player)
    {
        playerTransform = player;
        isBeingAttracted = true;
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = direction * magnetSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinManager.instance.AddCoin(1); // Adaugă o monedă la CoinManager
            Destroy(gameObject); // Distruge moneda
        }
    }
}