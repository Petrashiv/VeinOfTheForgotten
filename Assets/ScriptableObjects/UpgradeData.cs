using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrades/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    [TextArea] public string description;
    public int cost;

    public List<StatBonus> bonuses; // ������ �������������, ������� �������� �������

    public UpgradeData[] unlocksNext; // ���������, ������� ��������� ����� �������
}

[System.Serializable]
public class StatBonus
{
    public UpgradeType upgradeType;
    public int value;
}

public enum UpgradeType
{
    Health,
    MeleeDamage,
    RangedDamage,
    Stamina,
    Ability
}
