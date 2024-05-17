using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardRarity { Common, Rare, Epic, Legendary }

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public float effect;
    public CardRarity rarity;
    public Sprite icon;

    public Card(string name, string desc, float eff, CardRarity rar, Sprite icn)
    {
        cardName = name;
        description = desc;
        effect = eff;
        rarity = rar;
        icon = icn;
    }
    public void ApplyEffect(PlayerStats playerStats)
    {
        Debug.Log(effect);
        Debug.Log(playerStats);
        switch (cardName)
        {
            case "Increase Health":
                playerStats.IncreaseMaxHealth((int)effect);
                break;
            case "Increase MoveSpeed":
                playerStats.IncreaseMoveSpeed(effect);
                break;
            case "Demonic MoveSpeed":
                playerStats.IncreaseMoveSpeed(effect);
                playerStats.IncreaseMaxHealth(-20);
                break;
            case "Increase Luck":
                playerStats.IncreaseLuck(effect);
                break;
        }
    }
}
