using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;

    void Start()
    {
        coinText.text = "�����: " + CoinManager.Instance.GetCoinCount();
    }

    public void UpgradeHealth()
    {
        TrySpendCoins(10, "�������� ���������!");
    }

    public void UpgradeDamage()
    {
        TrySpendCoins(15, "���� ��������!");
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
            coinText.text = "�����: " + CoinManager.Instance.GetCoinCount();
        }
        else
        {
            Debug.Log("������������ �����!");
        }
    }

    public void BackToMainMenu()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("MainMenu");
    }
}
