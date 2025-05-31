using UnityEngine;
using System.Collections.Generic;
using System.Linq; // 🔧 Добавлено для Select()

public class UpgradeProgress : MonoBehaviour
{
    public static UpgradeProgress Instance { get; private set; }

    private HashSet<string> purchasedUpgradeNames = new HashSet<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadProgress(); // Загрузка прогресса при старте
    }

    public void SaveProgress()
    {
        var upgradeNames = purchasedUpgradeNames.ToList(); // ✅ просто список названий
        string json = JsonUtility.ToJson(new SaveData { upgrades = upgradeNames });
        PlayerPrefs.SetString("UpgradeProgress", json);
        PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey("UpgradeProgress"))
        {
            string json = PlayerPrefs.GetString("UpgradeProgress");
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            purchasedUpgradeNames = new HashSet<string>(data.upgrades);
        }
    }

    [System.Serializable]
    private class SaveData
    {
        public List<string> upgrades;
    }

    public void MarkUpgradeAsPurchased(UpgradeData upgrade)
    {
        if (!purchasedUpgradeNames.Contains(upgrade.upgradeName))
        {
            purchasedUpgradeNames.Add(upgrade.upgradeName);
        }
    }

    public bool IsUpgradePurchased(UpgradeData upgrade)
    {
        return purchasedUpgradeNames.Contains(upgrade.upgradeName);
    }

    public void ClearProgress()
    {
        purchasedUpgradeNames.Clear();
        PlayerPrefs.DeleteKey("UpgradeProgress"); // 💾 очищаем сохранение
    }

    public HashSet<string> GetPurchasedUpgrades()
    {
        return purchasedUpgradeNames;
    }
}
