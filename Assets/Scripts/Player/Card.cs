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
        {//TO DO: De luat fiecare fisier la rand si scris codul pentru fiecare in parte
            case "Stone Skin":
                playerStats.IncreaseArmor(effect);
                break;
            case "Painful Tastes":
                playerStats.IncreaseArmor(effect);
                playerStats.IncreaseMaxHealth(-20);
                break;
            case "Barrel Mania":
                playerStats.IncreaseBarrelCooldown(-effect);
                break;
            case "Barrel DMG":
                playerStats.IncreaseBarrelDamage(effect);
                break;
            case "Bomba Range":
                playerStats.IncreaseBarrelExplosionRadius(effect);
                break;
            case "Huge Cannons":
                playerStats.IncreaseDmgMultiplier(effect);
                break;
            case "Hell Cannons":
                playerStats.IncreaseDmgMultiplier(effect);
                playerStats.IncreaseMaxHealth(-20);
                playerStats.IncreaseMoveSpeed(-40);
                break;
            case "Polished pipe":
                playerStats.IncreaseDoubleCannonAttackSpeed(effect);
                break;
            case "Ball Speed":
                playerStats.IncreaseDoubleCannonCannonballSpeed(effect);
                break;
            case "Multi Cannon":
                playerStats.IncreaseDoubleCannonBulletsPerSide((int)effect);
                break;
            case "Fine pipe":
                playerStats.IncreaseDoubleCannonCriticalStrikeChance(effect);
                break;
            case "Ship Destroyer":
                playerStats.IncreaseDoubleCannonCriticalDamageMultiplier(effect);
                break;
            case "Sharp Eyes":
                playerStats.IncreaseDoubleCannonDamage(effect);
                break;
            case "FASTERRR":
                playerStats.IncreaseHandCannonAttackSpeed(effect);
                break;
            case "XLR8":
                playerStats.IncreaseHandCannonCannonballSpeed(effect);
                break;
            case "Fire balls":
                playerStats.IncreaseHandCannonCriticalStrikeChance(effect);
                break;
            case "Heavy Balls":
                playerStats.IncreaseHandCannonCriticalDamageMultiplier(effect);
                break;
            case "OneManArmy":
                playerStats.IncreaseHandCannonDamage(effect);
                break;
            case "Scorbut Imunity":
                playerStats.IncreaseMaxHealth((int)effect);
                break;
            case "Hell Gambler":
                playerStats.IncreaseMaxHealth((int)effect);
                playerStats.IncreaseLuck(-100);
                break;
            case "Quicksilver":
                playerStats.IncreaseLifeRegen((int)effect);
                break;
            case "Blood Bag":
                playerStats.IncreaseLifeRegen((int)effect);
                playerStats.IncreaseMoveSpeed(-40);
                break;
            case "Rabbit's Paw":
                playerStats.IncreaseLuck(effect);
                break;
            case "Lucky Pact":
                playerStats.IncreaseLuck(effect);
                break;
            case "HyperEngine":
                playerStats.IncreaseMoveSpeed(effect);
                break;
            case "IShowSpeed":
                playerStats.IncreaseMoveSpeed(effect);
                playerStats.IncreaseMaxHealth(-20);
                break;
            case "Fast Learner":
                playerStats.IncreaseXpMultiplier(effect);
                break;
            case "Black Magic":
                playerStats.IncreaseXpMultiplier(effect);
                playerStats.IncreaseMoveSpeed(-40);
                playerStats.IncreaseMoveSpeed(-20);
                break;
        }
    }
}