using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;
    public int cost;
    public UpgradeType upgradeType;
    public int value; // Насколько увеличивается характеристика
    public UpgradeData[] unlocksNext; // Какие улучшения открываются после покупки
}

public enum UpgradeType
{
    Health,
    MeleeDamage,
    RangedDamage,
    Stamina,
    Ability // Способность, которую дает
}
