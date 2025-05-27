using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas;
    public GameObject MainCanvas;
    public GameObject panel;
    public TextMeshProUGUI coinsText;
    
    
    public void ShowDeathScreen(int coinsCollected)
    {
    deathScreenCanvas.SetActive(true);
    //Time.timeScale = 0f; 
    coinsText.text = $"Вы собрали: {coinsCollected} монет";
    }

    public void OnUpgradeButton()
    {
        panel.SetActive(false);
        LoadingScreenManager.Instance.LoadSceneWithLoading("UpgradeMenu");

    }
    public void OnMenuButton()
    {
        panel.SetActive(false);
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }

    public void Retry()
    {
        Time.timeScale = 1f;
        LoadingScreenManager.Instance.LoadSceneWithLoading("SampleScene");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
