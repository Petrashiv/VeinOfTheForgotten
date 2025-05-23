using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        coinText.text = "Монет: " + CoinManager.Instance.GetCoinCount();
    }

    public void UpgradeHealth()
    {
        TrySpendCoins(10, "Здоровье увеличено!");
    }

    public void UpgradeDamage()
    {
        TrySpendCoins(15, "Урон увеличен!");
    }
    public void OnMenuButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    private void TrySpendCoins(int cost, string message)
    {
        if (CoinManager.Instance.GetCoinCount() >= cost)
        {
            CoinManager.Instance.AddCoins(-cost);
            Debug.Log(message);
            coinText.text = "Монет: " + CoinManager.Instance.GetCoinCount();
        }
        else
        {
            Debug.Log("Недостаточно монет!");
        }
    }

    public void BackToMainMenu()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("MainMenu");
    }
}
