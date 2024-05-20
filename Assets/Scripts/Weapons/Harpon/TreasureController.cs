using System.Collections.Generic;
using UnityEngine;

public class TreasureController : MonoBehaviour
{
    private Transform harpoon;
    public GameObject moneyPrefab; // Prefab pentru bani
    public int moneyAmount = 10; // Cantitatea de bani generată la explozie
    public float explosionRadius = 2f; // Raza exploziei
    public float minDistanceBetweenCoins = 0.5f; // Distanța minimă între monede

    public void AttachToHarpoon(Transform harpoonTransform)
    {
        Debug.Log("Treasure attached to harpoon!"); // Debug message

        harpoon = harpoonTransform;
        // Dezactivează fizica obiectului pentru a preveni interferențele
        GetComponent<Rigidbody2D>().isKinematic = true;
        // Atașează obiectul de harpon
        transform.parent = harpoon;
    }

    public void DetachFromHarpoon()
    {
        Debug.Log("Treasure detached from harpoon!"); // Debug message

        // Eliberează referința la harpon
        harpoon = null;
        // Reactivatează fizica obiectului
        GetComponent<Rigidbody2D>().isKinematic = false;
        // Dezparentă cufărul
        transform.parent = null;
    }

    void Update()
    {
        if (harpoon != null)
        {
            // Urmărește harponul
            transform.position = harpoon.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (harpoon != null && collision.CompareTag("Player"))
        {
            Debug.Log("Treasure collided with player!"); // Debug message

            // Cufărul explodează și generează bani
            Explode();
            // Distruge cufărul după explozie
            Destroy(gameObject);
        }
    }

    private void Explode()
    {
        Debug.Log("Treasure exploded!"); // Debug message

        List<Vector2> spawnPositions = new List<Vector2>();

        for (int i = 0; i < moneyAmount; i++)
        {
            Vector2 spawnPosition;
            int attempts = 0;
            do
            {
                spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * explosionRadius;
                attempts++;
                Debug.Log($"Attempt {attempts}: Trying position {spawnPosition}");
            } while (IsPositionTooClose(spawnPosition, spawnPositions) && attempts < 100);

            if (attempts < 100)
            {
                spawnPositions.Add(spawnPosition);
                Instantiate(moneyPrefab, spawnPosition, Quaternion.identity);
                Debug.Log($"Spawned money at position {spawnPosition}");
            }
            else
            {
                Debug.LogWarning("Failed to find a valid position for a coin after 100 attempts");
            }
        }
    }

    private bool IsPositionTooClose(Vector2 position, List<Vector2> positions)
    {
        foreach (Vector2 pos in positions)
        {
            if (Vector2.Distance(position, pos) < minDistanceBetweenCoins)
            {
                Debug.Log($"Position {position} is too close to {pos}");
                return true;
            }
        }
        return false;
    }
}