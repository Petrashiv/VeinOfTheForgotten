using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class UpgradeContainer : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI costText;
    public Button buyButton;

    private UpgradeData upgradeData;

    public void SetUpgrade(UpgradeData data)
    {
        upgradeData = data;
        titleText.text = data.upgradeName;
        descriptionText.text = data.description;
        costText.text = "Cost: " + data.cost;

        buyButton.onClick.AddListener(TryBuyUpgrade);
    }

    void TryBuyUpgrade()
    {
        // Покупка и разблокировка
    }
}
