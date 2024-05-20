using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class EnemyConfig
    {
        public GameObject enemyPrefab;
        public int count;
    }

    [System.Serializable]
    public class WaveConfig
    {
        public List<EnemyConfig> enemyConfigs;
        public GameObject bossPrefab;
        public float timeBetweenEnemies = 0.1f;
    }

    public static WaveManager Instance { get; private set; }

    public List<WaveConfig> waves;
    public Text waveText;
    public Text waveAnnouncementText;
    public Text warningText;
    public float spawnDistance = 20f;
    public float fadeDuration = 1f;

    public int currentWave = 0;
    private Camera mainCamera;
    private List<GameObject> activeEnemies = new List<GameObject>();
    private int totalEnemiesInWave;
    private bool bossSpawned = false;
    public bool isBossWave = false; // Boolean pentru a verifica dacă este o rundă de boss

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
        UpdateWaveText();
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
            yield return new WaitForSeconds(3f);

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
                    yield return new WaitForSeconds(waveConfig.timeBetweenEnemies);
                }
            }

            while (activeEnemies.Count > 0)
            {
                activeEnemies.RemoveAll(enemy => enemy == null);

                if (!bossSpawned && activeEnemies.Count <= 3 && waveConfig.bossPrefab != null)
                {
                    SpawnBoss(waveConfig.bossPrefab);
                    bossSpawned = true;
                    isBossWave = true;
                }

                yield return null;
            }

            bossSpawned = false;
            currentWave++;
            isBossWave = false;
            UpdateWaveText();
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(enemy);
    }

    private void SpawnBoss(GameObject bossPrefab)
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject boss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        activeEnemies.Add(boss);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        Vector2 spawnPosition;

        switch (UnityEngine.Random.Range(0, 4))
        {
            case 0:
                spawnPosition = new Vector2(-screenBounds.x - spawnDistance, UnityEngine.Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 1:
                spawnPosition = new Vector2(screenBounds.x + spawnDistance, UnityEngine.Random.Range(-screenBounds.y, screenBounds.y));
                break;
            case 2:
                spawnPosition = new Vector2(UnityEngine.Random.Range(-screenBounds.x, screenBounds.x), screenBounds.y + spawnDistance);
                break;
            case 3:
                spawnPosition = new Vector2(UnityEngine.Random.Range(-screenBounds.x, screenBounds.x), -screenBounds.y - spawnDistance);
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
        text.gameObject.SetActive(true);
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, t / fadeDuration));
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, t / fadeDuration));
            yield return null;
        }

        text.gameObject.SetActive(false);
    }

    public void ResetWaves()
    {
        StopAllCoroutines();
        currentWave = 0;
        activeEnemies.ForEach(enemy => Destroy(enemy));
        activeEnemies.Clear();
        UpdateWaveText();
    }
}