using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardRarity { Common, Rare, Epic, Legendary }

[CreateAssetMenu(fileName = "NewCard", menuName = "Card")]
public class Card : ScriptableObject
{
    public string cardName;
    public string description;
    public CardRarity rarity;
    public Sprite icon;

    public Card(string name, string desc, CardRarity rar, Sprite icn)
    {
        cardName = name;
        description = desc;
        rarity = rar;
        icon = icn;
    }
    public void ApplyEffect(PlayerXp player)
    {
        // Implementați logica specifică a efectului cardului aici
        switch (cardName)
        {
            case "Increase Health":
                player.IncreaseHealth();
                break;
            case "Increase Speed":
                player.IncreaseSpeed();
                break;
            case "Increase Damage":
                player.IncreaseDamage();
                break;
                // Adăugați mai multe cazuri pentru alte efecte de card
        }
    }
}
