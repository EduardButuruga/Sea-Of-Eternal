using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyConfig
    {
        public GameObject enemyPrefab; // Prefab pentru acest tip de inamic
        public int count; // Numărul acestui tip de inamic
    }

    [System.Serializable]
    public class WaveConfig
    {
        public List<EnemyConfig> enemyConfigs; // Lista tipurilor de inamici pentru acest wave
        public GameObject bossPrefab; // Prefab pentru boss
        public float timeBetweenEnemies = 0.1f; // Intervalul de timp între apariția inamicilor
    }

    public static WaveManager Instance { get; private set; }

    public List<WaveConfig> waves; // Lista configurărilor pentru wave-uri
    public Text waveText; // Referință la componenta Text din UI pentru numărul wave-ului
    public Text waveAnnouncementText; // Referință la componenta Text pentru anunțul wave-ului
    public Text warningText; // Referință la componenta Text pentru avertismentul boss-ului
    public float spawnDistance = 20f;
    public float fadeDuration = 1f; // Durata pentru fade in și fade out
    private int currentWave = 0;
    private Camera mainCamera;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalEnemiesInWave;
    private bool bossSpawned = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        mainCamera = Camera.main;
        UpdateWaveText(); // Afișează wave-ul inițial
    }

    public void StartWaves()
    {
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (currentWave < waves.Count)
        {
            ShowWaveAnnouncement();
            yield return new WaitForSeconds(3f); // Timp pentru a afișa anunțul wave-ului

            HideWaveAnnouncement();

            WaveConfig waveConfig = waves[currentWave];

            totalEnemiesInWave = 0;
            foreach (var enemyConfig in waveConfig.enemyConfigs)
            {
                totalEnemiesInWave += enemyConfig.count;
            }

            foreach (var enemyConfig in waveConfig.enemyConfigs)
            {
                for (int i = 0; i < enemyConfig.count; i++)
                {
                    SpawnEnemy(enemyConfig.enemyPrefab);
                    yield return new WaitForSeconds(waveConfig.timeBetweenEnemies); // Interval între inamici
                }
            }

            // Așteaptă până când toți inamicii sunt eliminați
            while (activeEnemies.Count > 0)
            {
                activeEnemies.RemoveAll(enemy => enemy == null); // Elimină inamicii distruși din listă

                // Verifică dacă trebuie să genereze boss-ul
                if (!bossSpawned && activeEnemies.Count <= 3 && waveConfig.bossPrefab != null)
                {
                    SpawnBoss(waveConfig.bossPrefab);
                    bossSpawned = true;
                }

                yield return null;
            }

            bossSpawned = false;
            currentWave++;
            UpdateWaveText(); // Actualizează textul cu numărul wave-ului curent
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Calculează o poziție random în afara ecranului
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instanțiază inamicul și îl adaugă în lista de inamici activi
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    private void SpawnBoss(GameObject bossPrefab)
    {
        // Calculează o poziție random în afara ecranului
        Vector2 spawnPosition = GetRandomSpawnPosition();

        // Instanțiază boss-ul și îl adaugă în lista de inamici activi
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(boss);
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

    private void UpdateWaveText()
    {
        waveText.text = "Wave: " + (currentWave + 1);
    }

    public void ShowWaveAnnouncement()
    {
        waveAnnouncementText.text = "Wave " + (currentWave + 1) + " incoming...";
        StartCoroutine(FadeTextInAndOut(waveAnnouncementText));

        // Afișează textul de avertisment dacă există un boss în acest wave
        if (waves[currentWave].bossPrefab != null)
        {
            warningText.text = "BEWARE!!!";
            warningText.color = Color.red;
            StartCoroutine(FadeTextInAndOut(warningText));
        }
        else
        {
            warningText.gameObject.SetActive(false);
        }

        Debug.Log("Next wave: " + (currentWave + 1));
    }

    public void HideWaveAnnouncement()
    {
        waveAnnouncementText.gameObject.SetActive(false);
        warningText.gameObject.SetActive(false);
    }

    private IEnumerator FadeTextInAndOut(Text text)
    {
        // Fade in
        text.gameObject.SetActive(true);
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t / fadeDuration));
            yield return null;
        }

        // Display for a while
        yield return new WaitForSeconds(1f);

        // Fade out
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, t / fadeDuration));
            yield return null;
        }

        text.gameObject.SetActive(false);
    }

    public void ResetWaves()
    {
        StopAllCoroutines(); // Oprește toate corutinele active
        currentWave = 0;
        activeEnemies.ForEach(enemy => Destroy(enemy)); // Distruge toți inamicii activi
        activeEnemies.Clear();
        UpdateWaveText(); // Actualizează textul pentru wave
    }
}