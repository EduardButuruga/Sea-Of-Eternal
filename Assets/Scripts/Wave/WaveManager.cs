using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public WaveTextController waveTextController;
    public int enemiesPerWave = 120; // Numărul de inamici per val
    public float timeBetweenWaves = 5f;
    public float spawnDistance = 20f;
    private int currentWave = 0;
    // Start is called before the first frame update
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
        while (true)
        {
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.1f); // Interval între inamici
            }
            yield return new WaitForSeconds(timeBetweenWaves); // Interval între valuri
        }
    }

    private void SpawnEnemy()
    {
        // Calculează o poziție random în afara ecranului
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instanțiază inamicul
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector2 spawnPosition;

        switch (Random.Range(0, 4))
        {
            case 0: // Stânga
                spawnPosition = new Vector2(-screenBounds.x - spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 1: // Dreapta
                spawnPosition = new Vector2(screenBounds.x + spawnDistance, Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 2: // Sus
                spawnPosition = new Vector2(Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 3: // Jos
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
        currentWave++;
        waveTextController.DisplayWaveText(currentWave);
        Debug.Log("Next wave");
    }
}
