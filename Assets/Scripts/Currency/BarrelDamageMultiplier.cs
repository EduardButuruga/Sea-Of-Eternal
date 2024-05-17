using UnityEngine;
using UnityEngine.UI;

public class UpgradeBarrackManager : MonoBehaviour
{
    public ExplosiveBarrel explosiveBarrel; // Referința la scriptul ExplosiveBarrel
    public CannonController cannonController; // Referința la scriptul CannonController
    public float damageIncreaseAmount = 10f; // Cantitatea cu care crește damage-ul
    public Button increaseDamageButton; // Butonul pentru creșterea damage-ului

    void Start()
    {
        // Asigură-te că butonul este setat corect
        if (increaseDamageButton != null)
        {
            increaseDamageButton.onClick.AddListener(IncreaseDamage);
        }
    }

    public void IncreaseDamage()
    {
        
    }
}