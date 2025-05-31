using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;
using UnityEngine.UI;


public class PreGameManager : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public Image blinkingImage;
    public float blinkSpeed = 2f;
    private Coroutine fadeCoroutine;
    [Header("Player Stats Display")]
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI meleeText;
    public TextMeshProUGUI rangedText;
    public TextMeshProUGUI staminaText;
    private void Start()
    {
        UpdatePlayerStatsDisplay();
        FadeOut();
    }
    
    private void Update()
    {
        if (canvasGroup.alpha > 0 && blinkingImage != null)
        {
            float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;
            Color color = blinkingImage.color;
            color.a = alpha;
            blinkingImage.color = color;
        }
    }

    public void FadeOut()
    {
        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeTo(0f, disableRaycastAfter: true));
    }

    private IEnumerator FadeTo(float targetAlpha, bool disableRaycastAfter = false)
    {
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = targetAlpha;

        if (disableRaycastAfter)
            canvasGroup.blocksRaycasts = false;
    }
    void UpdatePlayerStatsDisplay()
    {
        if (PlayerStats.Instance != null)
        {
            healthText.text = PlayerStats.Instance.GetHealth().ToString();
            meleeText.text = PlayerStats.Instance.GetMeleeDamage().ToString();
            rangedText.text = PlayerStats.Instance.GetRangedDamage().ToString();
            staminaText.text = PlayerStats.Instance.GetStamina().ToString();
        }
    }
    public void OnMenu()
    {
        
        LoadingScreenManager.Instance.LoadSceneWithLoading("MainMenu");
        
    }
    public void Upgrades()
    {

        LoadingScreenManager.Instance.LoadSceneWithLoading("UpgradeMenu");

    }
    public void Retry()
    {
        Time.timeScale = 1f;
        LoadingScreenManager.Instance.LoadSceneWithLoading("SampleScene");
        
    }
}
