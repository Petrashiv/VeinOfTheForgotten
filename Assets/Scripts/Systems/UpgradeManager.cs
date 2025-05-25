using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public TextMeshProUGUI coinText;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public Image blinkingImage;
    public float blinkSpeed = 2f;
    private Coroutine fadeCoroutine;

    public Transform upgradeContainer;
    public GameObject upgradeButtonPrefab;
    public List<UpgradeData> availableUpgrades;
    private HashSet<UpgradeData> purchasedUpgrades = new HashSet<UpgradeData>();

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
    void Start()
    {
        FadeOut();
        coinText.text = "Монет: " + CoinManager.Instance.GetCoinCount();
        DisplayUpgrades();
    }
    void DisplayUpgrades()
    {
        foreach (Transform child in upgradeContainer)
            Destroy(child.gameObject);

        foreach (var upgrade in availableUpgrades)
        {
            if (purchasedUpgrades.Contains(upgrade)) continue;

            GameObject buttonObj = Instantiate(upgradeButtonPrefab, upgradeContainer);
            buttonObj.GetComponentInChildren<TextMeshProUGUI>().text = $"{upgrade.upgradeName} ({upgrade.cost} монет)";
            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() => TryPurchase(upgrade, buttonObj));
        }
    }

    void TryPurchase(UpgradeData upgrade, GameObject buttonObj)
    {
        if (CoinManager.Instance.GetCoinCount() >= upgrade.cost)
        {
            CoinManager.Instance.AddCoins(-upgrade.cost);
            ApplyUpgrade(upgrade);
            purchasedUpgrades.Add(upgrade);
            Destroy(buttonObj);

            // Добавляем открытые улучшения
            foreach (var next in upgrade.unlocksNext)
                if (!availableUpgrades.Contains(next)) availableUpgrades.Add(next);

            DisplayUpgrades(); // Перестроить UI
        }
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.Health:
                PlayerStats.Instance.IncreaseHealth(upgrade.value);
                break;
            case UpgradeType.MeleeDamage:
                PlayerStats.Instance.IncreaseMeleeDamage(upgrade.value);
                break;
            case UpgradeType.RangedDamage:
                PlayerStats.Instance.IncreaseRangedDamage(upgrade.value);
                break;
            case UpgradeType.Stamina:
                PlayerStats.Instance.IncreaseStamina(upgrade.value);
                break;
            /*case UpgradeType.Ability:
                PlayerAbilities.Instance.UnlockAbility(upgrade);
                break;*/
        }
    }


    public void OnMenuButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    

    public void BackToMainMenu()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("MainMenu");
    }
}
