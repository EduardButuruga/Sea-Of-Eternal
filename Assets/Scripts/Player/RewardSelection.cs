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
    public Button confirmButton;
    public Text[] rewardDescriptions;
    public Text[] names;
    public Text[] rarity;
    public PlayerXp playerXP;
    public CardDatabase cardDatabase; // Referință către baza de date a cardurilor
    public PlayerStats playerStats;
    public WeaponUnlocks weaponUnlocks;
    public checkboxCtrl checkboxCtrl;
    public StateOfUI stateOfUI;
    private Card? currentChosenCard;
    public bool cardChosen = false;

    private void Start()
    {
        if(playerStats == null)
        {
            playerStats = FindObjectOfType<PlayerStats>();
        }
        rewardPanel.SetActive(false);
    }

    public void ShowRewardSelection()
    {
        rewardPanel.SetActive(true);
        cardChosen = false;
        stateOfUI.SetLvlInterfaceTrue();
        Time.timeScale = 0f;

        List<Card> randomCards = cardDatabase.GetRandomCards(rewardButtons.Length, playerStats.luck, weaponUnlocks, checkboxCtrl);

        for (int i = 0; i < rewardButtons.Length; i++)
        {
            rewardDescriptions[i].text = randomCards[i].description;
            rewardButtons[i].image.sprite = randomCards[i].icon;
            names[i].text = randomCards[i].cardName;
            if (randomCards[i].name == "demonic")
                rarity[i].text = "Demonic";
            else
                rarity[i].text = randomCards[i].rarity.ToString();
            
            int index = i;
            rewardButtons[i].onClick.RemoveAllListeners();
            rewardButtons[i].onClick.AddListener(() => SelectReward(randomCards[index]));
        }
    }

    public void SelectReward(Card selectedCard)
    {
        currentChosenCard = selectedCard;
        cardChosen = true;
    }

    public void Confirm()
    {
        if(cardChosen)
        {
            currentChosenCard.ApplyEffect(playerStats);
            stateOfUI.SetGameTrue();
            rewardPanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Reroll()
    {
        if(playerStats.rerolls > 0)
        {
            playerStats.rerolls--;
            ShowRewardSelection();
        }
    }
}
