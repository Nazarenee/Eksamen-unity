using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeData : ScriptableObject
{
    public UpgradeType upgradeType;  // Which stat this affects
    public UpgradeRarity rarity;     // Rarity level
    public float percentageIncrease; // Percentage applied
}