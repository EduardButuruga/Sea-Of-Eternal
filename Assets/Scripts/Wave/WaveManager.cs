using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveConfig
    {
        public List<GameObject> enemyPrefabs; // List of enemy types for this wave
        public int enemiesPerWave = 120; // Number of enemies for this wave
        public float timeBetweenEnemies = 0.1f; // Time interval between enemy spawns
    }

    public List<WaveConfig> waves; // List of wave configurations
    public WaveTextController waveTextController;
    public float timeBetweenWaves = 5f;
    public float spawnDistance = 20f;
    private int currentWave = 0;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < waves.Count)
        {
            ShowWaveText();
            WaveConfig waveConfig = waves[currentWave];

            for (int i = 0; i < waveConfig.enemiesPerWave; i++)
            {
                SpawnEnemy(waveConfig.enemyPrefabs);
                yield return new WaitForSeconds(waveConfig.timeBetweenEnemies); // Interval between enemies
            }

            yield return new WaitForSeconds(timeBetweenWaves); // Interval between waves
            currentWave++;
        }
    }

    private void SpawnEnemy(List<GameObject> enemyPrefabs)
    {
        // Calculate a random position off-screen
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instantiate a random enemy from the list
        GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector2 spawnPosition;

        switch (Random.Range(0, 4))
        {
            case 0: // Left
                spawnPosition = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 1: // Right
                spawnPosition = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 2: // Up
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 3: // Down
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - spawnDistance);
                break;
            default:
                spawnPosition = Vector2.zero;
                break;
        }

        return spawnPosition;
    }

    public void ShowWaveText()
    {
        waveTextController.DisplayWaveText(currentWave + 1);
        Debug.Log("Next wave: " + (currentWave + 1));
    }
}
