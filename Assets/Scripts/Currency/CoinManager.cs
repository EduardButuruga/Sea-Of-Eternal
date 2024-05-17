using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public static CoinManager instance;
    public Text coinText;
    public Text coinText2;
    public int coins;

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
    void Update()
    {
        UpdateCoinText();
    }
    void Start()
    {     
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
            coinText2.text = "x" + coins;
            coinText.text = "x" + coins;
        }
    }
}