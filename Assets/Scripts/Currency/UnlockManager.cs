using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockManager : MonoBehaviour
{
    [System.Serializable]
    public class Weapon
    {
        public string name;
        public int price;
        public Image weaponImage;
        public Button buyButton;
        public Text priceText;
        public GameObject associatedBuilding;
        public GameObject weaponGameObject;
        public bool unlocksBarrels;
        public Button relatedUnlockButton;
        public MonoBehaviour scriptToEnable;
    }

    public Weapon[] weapons;
    public PlayerCtrl playerCtrl;

    void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.priceText.text = weapon.price.ToString();
            weapon.buyButton.onClick.AddListener(() => BuyWeapon(weapon));

            if (weapon.relatedUnlockButton != null)
            {
                weapon.relatedUnlockButton.gameObject.SetActive(false);
            }

            if (weapon.scriptToEnable != null)
            {
                weapon.scriptToEnable.enabled = false;
            }
        }
    }

    void BuyWeapon(Weapon weapon)
    {
        if (CoinManager.instance.GetCoins() >= weapon.price)
        {
            CoinManager.instance.AddCoin(-weapon.price);
            weapon.weaponImage.color = Color.white;
            weapon.buyButton.gameObject.SetActive(false);
            weapon.priceText.gameObject.SetActive(false);

            if (weapon.associatedBuilding != null)
            {
                weapon.associatedBuilding.SetActive(true);
            }
            if (weapon.weaponGameObject != null)
            {
                weapon.weaponGameObject.SetActive(true);
            }

            if (weapon.unlocksBarrels)
            {
                playerCtrl.areBarrelsUnlocked = true;
            }

            if (weapon.relatedUnlockButton != null)
            {
                weapon.relatedUnlockButton.gameObject.SetActive(true);
            }

            if (weapon.scriptToEnable != null)
            {
                weapon.scriptToEnable.enabled = true;
            }
        }
    }

    public void UpdateWeaponStates(List<string> unlockedWeapons)
    {
        foreach (var weapon in weapons)
        {
            if (unlockedWeapons.Contains(weapon.name))
            {
                weapon.weaponImage.color = Color.white;
                weapon.buyButton.gameObject.SetActive(false);
                weapon.priceText.gameObject.SetActive(false);

                if (weapon.associatedBuilding != null)
                {
                    weapon.associatedBuilding.SetActive(true);
                }
                if (weapon.weaponGameObject != null)
                {
                    weapon.weaponGameObject.SetActive(true);
                }

                if (weapon.unlocksBarrels)
                {
                    playerCtrl.areBarrelsUnlocked = true;
                }

                if (weapon.relatedUnlockButton != null)
                {
                    weapon.relatedUnlockButton.gameObject.SetActive(true);
                }

                if (weapon.scriptToEnable != null)
                {
                    weapon.scriptToEnable.enabled = true;
                }
            }
        }
    }
}