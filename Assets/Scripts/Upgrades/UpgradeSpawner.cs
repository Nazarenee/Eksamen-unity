using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject upgradePrefab;  // The Upgrade Pill prefab
    public UpgradeData[] possibleUpgrades; // Array of all upgrade types (without rarity)
    public Material[] rarityMaterials; // Materials for each rarity

    private Vector3 upgradeSpawn1 = new Vector3(-5, 1.5f, -4);
    private Vector3 upgradeSpawn2 = new Vector3(-5, 1.5f, 4);

    private void Start()
    {
        SpawnUpgrade(upgradeSpawn1);
        SpawnUpgrade(upgradeSpawn2);
    }

    private void SpawnUpgrade(Vector3 position)
    {
        UpgradeData baseUpgrade = GetRandomUpgradeType();
        if (baseUpgrade == null) return;

        UpgradeRarity rolledRarity = RollRarity();
        float percentageIncrease = GetPercentageIncrease(rolledRarity);

        // Create a new instance of UpgradeData dynamically
        UpgradeData newUpgrade = ScriptableObject.CreateInstance<UpgradeData>();
        newUpgrade.upgradeType = baseUpgrade.upgradeType;
        newUpgrade.rarity = rolledRarity;
        newUpgrade.percentageIncrease = percentageIncrease;

        GameObject upgradeInstance = Instantiate(upgradePrefab, position, Quaternion.identity);
        UpgradePill upgradeScript = upgradeInstance.GetComponent<UpgradePill>();
        upgradeScript.Initialize(newUpgrade, GetMaterialForRarity(rolledRarity));
    }

    private UpgradeData GetRandomUpgradeType()
    {
        if (possibleUpgrades.Length == 0) return null;
        return possibleUpgrades[Random.Range(0, possibleUpgrades.Length)];
    }

    private UpgradeRarity RollRarity()
    {
        float roll = Random.value;
        float cumulative = 0f;

        (UpgradeRarity rarity, float chance)[] rarityChances =
        {
            (UpgradeRarity.Common, 0.50f),
            (UpgradeRarity.Rare, 0.30f),
            (UpgradeRarity.Epic, 0.15f),
            (UpgradeRarity.Heroic, 0.04f),
            (UpgradeRarity.Legendary, 0.01f)
        };

        foreach (var pair in rarityChances)
        {
            cumulative += pair.chance;
            if (roll <= cumulative)
                return pair.rarity;
        }

        return UpgradeRarity.Common;
    }

    private float GetPercentageIncrease(UpgradeRarity rarity)
    {
        switch (rarity)
        {
            case UpgradeRarity.Common: return 0.20f;
            case UpgradeRarity.Rare: return 0.40f;
            case UpgradeRarity.Epic: return 0.75f;
            case UpgradeRarity.Heroic: return 1.35f;
            case UpgradeRarity.Legendary: return 2.00f;
            default: return 0f;
        }
    }

    private Material GetMaterialForRarity(UpgradeRarity rarity)
    {
        return rarityMaterials[(int)rarity];
    }
}
