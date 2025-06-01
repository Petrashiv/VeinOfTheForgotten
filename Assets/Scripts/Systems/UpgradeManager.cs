using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class UpgradeButtonPair
{
    public UpgradeData upgrade;
    public Button button;
}

public class UpgradeManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI coinText;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;
    public Image blinkingImage;
    public float blinkSpeed = 2f;

    [Header("Upgrades")]
    public List<UpgradeButtonPair> upgradeButtons;
    private HashSet<UpgradeData> purchasedUpgrades = new HashSet<UpgradeData>();

    [Header("Upgrade Info Panel")]
    public GameObject upgradeInfoPanel;
    public TextMeshProUGUI infoTitle;
    public TextMeshProUGUI infoDescription;
    public TextMeshProUGUI infoCost;
    public TextMeshProUGUI healthBonusText;
    public TextMeshProUGUI meleeBonusText;
    public TextMeshProUGUI rangedBonusText;
    public TextMeshProUGUI staminaBonusText;
    public Button purchaseButton;
    public Button closeButton;

    private Coroutine fadeCoroutine;
    private UpgradeData currentSelectedUpgrade;

    private void Start()
    {
        FadeOut();
        UpdateCoinText();

        if (upgradeInfoPanel != null)
            upgradeInfoPanel.SetActive(false);

        if (closeButton != null)
            closeButton.onClick.AddListener(HideUpgradeInfo);

        foreach (var pair in upgradeButtons)
        {
            var upgrade = pair.upgrade;

            // Проверяем, куплен ли уже апгрейд — отключаем кнопку
            if (UpgradeProgress.Instance.IsUpgradePurchased(upgrade))
            {
                pair.button.interactable = false;
                continue;
            }

            pair.button.onClick.AddListener(() => ShowUpgradeInfo(upgrade));
        }
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

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + CoinManager.Instance.GetCoinCount();
    }

    void ShowUpgradeInfo(UpgradeData upgrade)
    {
        currentSelectedUpgrade = upgrade;

        infoTitle.text = upgrade.upgradeName;
        infoDescription.text = upgrade.description;
        infoCost.text = "Стоимость: " + upgrade.cost + " монет";

        healthBonusText.text = "";
        meleeBonusText.text = "";
        rangedBonusText.text = "";
        staminaBonusText.text = "";

        foreach (var bonus in upgrade.bonuses)
        {
            switch (bonus.upgradeType)
            {
                case UpgradeType.Health:
                    healthBonusText.text = $"+{bonus.value}";
                    break;
                case UpgradeType.MeleeDamage:
                    meleeBonusText.text = $"+{bonus.value}";
                    break;
                case UpgradeType.RangedDamage:
                    rangedBonusText.text = $"+{bonus.value}";
                    break;
                case UpgradeType.Stamina:
                    staminaBonusText.text = $"+{bonus.value}";
                    break;
            }
        }

        purchaseButton.onClick.RemoveAllListeners();
        purchaseButton.onClick.AddListener(() => TryPurchase(upgrade));

        upgradeInfoPanel.SetActive(true);
    }

    void HideUpgradeInfo()
    {
        upgradeInfoPanel.SetActive(false);
    }

    void TryPurchase(UpgradeData upgrade)
    {
        if (CoinManager.Instance.GetCoinCount() >= upgrade.cost)
        {
            CoinManager.Instance.AddCoins(-upgrade.cost);
            UpdateCoinText();

            ApplyUpgrade(upgrade);
            purchasedUpgrades.Add(upgrade);
            UpgradeProgress.Instance.MarkUpgradeAsPurchased(upgrade);
            UpgradeProgress.Instance.SaveProgress();
            foreach (var next in upgrade.unlocksNext)
            {
                foreach (var pair in upgradeButtons)
                {
                    if (pair.upgrade == next)
                        pair.button.gameObject.SetActive(true);
                }
            }

            HideUpgradeInfo();

            foreach (var pair in upgradeButtons)
            {
                if (pair.upgrade == upgrade)
                    pair.button.interactable = false;
            }
        }
    }

    void ApplyUpgrade(UpgradeData upgrade)
    {
        foreach (var bonus in upgrade.bonuses)
        {
            switch (bonus.upgradeType)
            {
                case UpgradeType.Health:
                    PlayerStats.Instance.IncreaseHealth(bonus.value);
                    break;
                case UpgradeType.MeleeDamage:
                    PlayerStats.Instance.IncreaseMeleeDamage(bonus.value);
                    break;
                case UpgradeType.RangedDamage:
                    PlayerStats.Instance.IncreaseRangedDamage(bonus.value);
                    break;
                case UpgradeType.Stamina:
                    PlayerStats.Instance.IncreaseStamina(bonus.value);
                    break;
            }
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
