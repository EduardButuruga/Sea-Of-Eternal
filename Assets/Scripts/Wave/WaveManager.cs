using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public WaveTextController waveTextController;
    public Button testButton;
    public int enemiesPerWave = 5; // Numărul de inamici per val
    public float timeBetweenWaves = 5f;
    public float spawnDistance = 20f;
    private int currentWave = 0;
    // Start is called before the first frame update
    private int testWaveNumber = 0;
    private Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;

        if (testButton != null)
        {
            testButton.onClick.AddListener(TestWaveText);
        }
    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            currentWave++;
            for (int i = 0; i < enemiesPerWave; i++)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(1f); // Interval între inamici
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
    // Update is called once per frame
    void Update()
    {
        
    }
    public void TestWaveText()
    {
        waveTextController.DisplayWaveText(testWaveNumber);
        Debug.Log("Next wave");
        testWaveNumber++;
    }

    //void StartNewWave()
    //{
    //    currentWave++;
    //    waveTextController.DisplayWaveText(currentWave);

    //    // Logica pentru a începe un nou val de inamici
    //}
}
