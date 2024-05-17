using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float luck = 0f;
    public float armor = 0f;
    public float lifeRegen = 0f;
    public float xpMultiplier = 1.0f;

    public PlayerStats()
    {
        luck = 0f;
        armor = 0f;
        lifeRegen = 0f;
        xpMultiplier = 1.0f;
    }
    public void IncreaseLuck(float amount)
    {
        luck += amount;
    }

    public void IncreaseArmor(float amount)
    {
        armor += amount;
    }

    public void IncreaseLifeRegen(float amount)
    {
        lifeRegen += amount;
    }

    public void IncreaseXpMultiplier(float amount)
    {
        xpMultiplier += amount;
    }

   
}
