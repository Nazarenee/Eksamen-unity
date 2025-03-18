using UnityEngine;

public class UpgradeSpawner : MonoBehaviour
{
    public GameObject upgradePrefab;  // The Upgrade Pill prefab
    public Transform spawnPoint;      // Location where it spawns
    public UpgradeData[] possibleUpgrades; // Array of all possible upgrades
    public Material[] rarityMaterials; // Materials for each rarity

    private void Start()
    {
        SpawnUpgrade();
    }

    private void SpawnUpgrade()
    {
        UpgradeData chosenUpgrade = RollUpgrade();
        if (chosenUpgrade == null) return;

        // Spawn the Upgrade Pill
        GameObject upgradeInstance = Instantiate(upgradePrefab, spawnPoint.position, Quaternion.identity);
        Upgrade upgradeScript = upgradeInstance.GetComponent<Upgrade>();

        // Initialize the upgrade
        upgradeScript.Initialize(chosenUpgrade, GetMaterialForRarity(chosenUpgrade.rarity));
    }

    private UpgradeData RollUpgrade()
    {
        float roll = Random.value;
        float cumulative = 0f;

        foreach (UpgradeData upgrade in possibleUpgrades)
        {
            float rarityChance = GetRarityChance(upgrade.rarity);
            cumulative += rarityChance;

            if (roll <= cumulative)
                return upgrade;
        }

        return null; // Fallback case
    }

    private float GetRarityChance(UpgradeRarity rarity)
    {
        switch (rarity)
        {
            case UpgradeRarity.Common: return 0.50f;
            case UpgradeRarity.Rare: return 0.30f;
            case UpgradeRarity.Epic: return 0.15f;
            case UpgradeRarity.Heroic: return 0.04f;
            case UpgradeRarity.Legendary: return 0.01f;
            default: return 0f;
        }
    }

    private Material GetMaterialForRarity(UpgradeRarity rarity)
    {
        return rarityMaterials[(int)rarity];
    }
}