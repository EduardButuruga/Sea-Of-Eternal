using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    public List<Card> allCards = new List<Card>();
    public WeaponUnlocks weaponUnlocks;
    public checkboxCtrl checkboxCtrl;

    public List<Card> GetRandomCards(int count, float luck, WeaponUnlocks weaponUnlockss, checkboxCtrl checkboxCtrls)
    {
        weaponUnlocks = weaponUnlockss;
        checkboxCtrl = checkboxCtrls;
        List<Card> randomCards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            Card randomCard = GetRandomCardByLuck(luck);
            randomCards.Add(randomCard);
        }
        return randomCards;
    }

    private Card GetRandomCardByLuck(float luck)
    {
        if (luck > 1000)
            luck = 1000;
        // Probabilități de bază pentru fiecare raritate
        float baseCommonProbability = 0.7f;
        float baseRareProbability = 0.25f;
        float baseEpicProbability = 0.04f;
        float baseLegendaryProbability = 0.01f;

        // Ajustare în funcție de luck
        float adjustedCommonProbability = Mathf.Max(baseCommonProbability - luck * 0.00069f, 0.01f);
        float adjustedRareProbability = Mathf.Min(baseRareProbability - luck * 0.00021f, 1f);
        float adjustedEpicProbability = Mathf.Min(baseEpicProbability + luck * 0.00021f, 1f);
        float adjustedLegendaryProbability = Mathf.Min(baseLegendaryProbability + luck * 0.00069f, 1f);

        // Normalizare a probabilităților
        float totalProbability = adjustedCommonProbability + adjustedRareProbability + adjustedEpicProbability + adjustedLegendaryProbability;
        adjustedCommonProbability /= totalProbability;
        adjustedRareProbability /= totalProbability;
        adjustedEpicProbability /= totalProbability;
        adjustedLegendaryProbability /= totalProbability;

        // Generăm o valoare random și selectăm cardul bazat pe probabilități
        float randomValue = Random.value;
        if (randomValue < adjustedCommonProbability)
        {
            return GetRandomCardByRarity(CardRarity.Common);
        }
        else if (randomValue < adjustedCommonProbability + adjustedRareProbability)
        {
            return GetRandomCardByRarity(CardRarity.Rare);
        }
        else if (randomValue < adjustedCommonProbability + adjustedRareProbability + adjustedEpicProbability)
        {
            return GetRandomCardByRarity(CardRarity.Epic);
        }
        else
        {
            return GetRandomCardByRarity(CardRarity.Legendary);
        }
    }

    private Card GetRandomCardByRarity(CardRarity rarity)
    {
        List<Card> cardsOfRarity = allCards.FindAll(card => card.rarity == rarity);
        if (cardsOfRarity.Count > 0)
        {
            Debug.Log("CN e fals" + checkboxCtrl.isDoubleCannonEnabled);
            Card x;
            while(true)
            { x = cardsOfRarity[Random.Range(0, cardsOfRarity.Count)];
                if (x.category.ToString() == "OverallStats")
                {
                    return x;
                }
                if (x.category.ToString() == "Barrel" && weaponUnlocks.isBarrelUnlocked && checkboxCtrl.isBarrelEnabled)
                {
                    return x;

                }
                if (x.category.ToString() == "DoubleCannon" && weaponUnlocks.isDoubleCannonUnlocked && checkboxCtrl.isDoubleCannonEnabled)
                {
                    return x;

                }
                if (x.category.ToString() == "HandCannon" && weaponUnlocks.isHandCannonUnlocked && checkboxCtrl.isHandCannonEnabled)
                {
                    return x;
                }
            }
        }
        return null;
    }
}
