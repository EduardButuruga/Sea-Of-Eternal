using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RewardSelection : MonoBehaviour
{
    public GameObject rewardPanel;
    public Button[] rewardButtons;
    public Text[] rewardDescriptions;
    public Text[] names;
    public PlayerXp playerXP;
    public CardDatabase cardDatabase; // Referință către baza de date a cardurilor
    public PlayerStats playerStats;

    private void Start()
    {
        rewardPanel.SetActive(false);
    }

    public void ShowRewardSelection()
    {
        Debug.Log(playerStats);
        rewardPanel.SetActive(true);
        Time.timeScale = 0f;

        List<Card> randomCards = cardDatabase.GetRandomCards(rewardButtons.Length);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            rewardDescriptions[i].text = randomCards[i].description;
            rewardButtons[i].image.sprite = randomCards[i].icon;
            names[i].text = randomCards[i].name;

            int index = i; // Necesită pentru a evita problemele de referință în lambda
            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => SelectReward(randomCards[index]));
        }
    }

    public void SelectReward(Card selectedCard)
    {
        selectedCard.ApplyEffect(playerXP);
        rewardPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
