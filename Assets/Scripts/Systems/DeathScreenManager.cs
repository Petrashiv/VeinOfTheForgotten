using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{
    public GameObject deathScreenCanvas;
    public GameObject MainCanvas;
    public TextMeshProUGUI coinsText;

    public void ShowDeathScreen(int coinsCollected)
    {
        MainCanvas.SetActive(false);
        deathScreenCanvas.SetActive(true);
        Time.timeScale = 0f; // замораживаем игру
        coinsText.text = $"Вы собрали: {coinsCollected} монет";
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
