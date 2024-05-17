using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptableObjects/CardDatabase", order = 1)]
public class CardDatabase : ScriptableObject
{
    public List<Card> allCards = new List<Card>();

    public List<Card> GetRandomCards(int count)
    {
        List<Card> randomCards = new List<Card>();
        for (int i = 0; i < count; i++)
        {
            Card randomCard = allCards[Random.Range(0, allCards.Count)];
            randomCards.Add(randomCard);
        }
        return randomCards;
    }
}
