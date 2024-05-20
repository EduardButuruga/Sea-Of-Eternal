using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage; // Cantitatea de damage dată jucătorului
    public PlayerHealth playerHealth; // Referință la componenta PlayerHealth a jucătorului
    public WaveManager waveManager;

    private void Start()
    {
        waveManager = GameObject.FindWithTag("WaveManager").GetComponent<WaveManager>();
        damage = damage * waveManager.currentWave;
        // Dacă referința nu este setată în Inspector, încearcă să o găsești automat
        if (playerHealth == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealth = player.GetComponent<PlayerHealth>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // Verifică dacă obiectul colizionat este jucătorul
        {
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage, transform.position);
            }
        }
    }
}