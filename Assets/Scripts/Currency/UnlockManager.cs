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
        public bool unlocksBarrels; // Flag pentru deblocarea butoaielor
        public Button relatedUnlockButton; // Butonul care trebuie activat după achiziționare
        public MonoBehaviour scriptToEnable; // Referință la scriptul care trebuie activat după achiziționare
    }

    public Weapon[] weapons;
    public PlayerCtrl playerCtrl; // Referință către scriptul PlayerCtrl

    void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.priceText.text = weapon.price.ToString();
            weapon.buyButton.onClick.AddListener(() => BuyWeapon(weapon));

            // Dezactivează butonul de deblocare inițial
            if (weapon.relatedUnlockButton != null)
            {
                weapon.relatedUnlockButton.gameObject.SetActive(false);
            }

            // Dezactivează scriptul inițial
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

            // Activează butonul de deblocare asociat
            if (weapon.relatedUnlockButton != null)
            {
                weapon.relatedUnlockButton.gameObject.SetActive(true);
            }

            // Activează scriptul asociat
            if (weapon.scriptToEnable != null)
            {
                weapon.scriptToEnable.enabled = true;
            }
        }
    }
}