using System.Collections;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawnConfig
    {
        public GameObject enemyPrefab; // Prefab pentru inamicul de generat
        public int enemyCount; // Numărul de inamici de generat
    }

    public EnemySpawnConfig[] enemySpawnConfigs; // Configurațiile pentru fiecare tip de inamic
    public Transform[] spawnPoints; // Punctele din care pot fi generați inamicii
    public float spawnInterval = 5f; // Intervalul de timp între verificări și generări
    private bool canSpawn = true;

    void Start()
    {
        StartCoroutine(CheckAndSpawnEnemies());
    }

    private IEnumerator CheckAndSpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (canSpawn && !AreEnemiesAlive())
            {
                Debug.Log("No enemies alive, spawning new enemies.");
                SpawnEnemies();
            }
            else
            {
                Debug.Log("Enemies still alive, waiting...");
            }
        }
    }

    private bool AreEnemiesAlive()
    {
        // Verifică dacă mai sunt inamici de tipurile specificate pe hartă
        foreach (var config in enemySpawnConfigs)
        {
            if (FindObjectsOfType(config.enemyPrefab.GetComponent<Enemy>().GetType()).Length > 0)
            {
                Debug.Log("Enemies found: " + config.enemyPrefab.name);
                return true;
            }
        }
        return false;
    }

    private void SpawnEnemies()
    {
        foreach (var config in enemySpawnConfigs)
        {
            for (int i = 0; i < config.enemyCount; i++)
            {
                // Alege aleatoriu un punct de spawn
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(config.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                Debug.Log("Spawned " + config.enemyPrefab.name + " at " + spawnPoint.position);
            }
        }
    }
}