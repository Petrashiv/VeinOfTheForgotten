using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;
    
    public int coinCount = 0;
    public TextMeshProUGUI coinText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // сохраняем объект между сценами
            LoadCoins();
        }
        else
        {
            Destroy(gameObject);
        }
            
    }
    void Start()
    {
        UpdateUI();
    }
    public void AddCoins(int amount)
    {
        coinCount += amount;
        
        UpdateUI();
    }
    
    public int GetCoinCount()
    {
        return coinCount;
    }
    void UpdateUI()
    {
        if (coinText != null)
            coinText.text = "Coins: " + coinCount;
    }
    private void SaveCoins()
    {
        PlayerPrefs.SetInt("TotalCoins", coinCount);
    }

    private void LoadCoins()
    {
        coinCount = PlayerPrefs.GetInt("TotalCoins", 0);
    }

    public void ResetCoins()
    {
        coinCount = 0;
        SaveCoins();
        UpdateUI();
    }
}
