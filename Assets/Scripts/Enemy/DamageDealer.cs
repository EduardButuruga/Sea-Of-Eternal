using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage = 1; // Cantitatea de damage dată jucătorului

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name); // Log când se detectează coliziunea

        if (collision.CompareTag("Player")) // Verifică dacă obiectul colizionat este jucătorul
        {
            Debug.Log("Collision with Player detected."); // Log când se colizionează cu jucătorul

            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Debug.Log("PlayerHealth component found on Player."); // Log când componenta PlayerHealth este găsită

                playerHealth.TakeDamage(damage, transform.position);
            }
            else
            {
                Debug.LogWarning("PlayerHealth component not found on Player."); // Log de avertizare dacă componenta PlayerHealth nu este găsită
            }
        }
        else
        {
            Debug.Log("Collision with non-Player object."); // Log când coliziunea nu este cu jucătorul
        }
    }
}