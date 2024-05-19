using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance { get; private set; }
    private int enemiesKilledCount;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Opțional: păstrează obiectul între scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetEnemiesKilledCount()
    {
        return enemiesKilledCount;
    }

    public void IncrementEnemiesKilledCount()
    {
        enemiesKilledCount++;
    }

    public void ResetEnemiesKilledCount()
    {
        enemiesKilledCount = 0;
    }
}