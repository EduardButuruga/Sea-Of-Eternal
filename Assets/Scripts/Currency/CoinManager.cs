using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public Text coinText;
    private int coins;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        coins = 0;
        UpdateCoinText();
    }

    public void AddCoin(int amount)
    {
        coins += amount;
        UpdateCoinText();
    }

    public int GetCoins()
    {
        return coins;
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = "x" + coins;
        }
    }
}