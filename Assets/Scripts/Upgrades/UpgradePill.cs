using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    private Renderer renderer;
    private bool isInFocus = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Initialize(UpgradeData data, Material material)
    {
        upgradeData = data;
        renderer.material = material;
    }

    public void SetFocus(bool focus)
    {
        isInFocus = focus;
        // You can trigger a UI message here (e.g., "Press E to pick up")
    }

    private void Update()
    {
        if (isInFocus && Input.GetKeyDown(KeyCode.E))
        {
            PickUpUpgrade();
        }
    }
    
    private void PickUpUpgrade()
    {
        player_movement movement = FindFirstObjectOfType<player_movement>();
        DamageBow bow = FindObjectOfType<DamageBow>();

        if (movement != null && bow != null)
        {
            switch (upgradeData.upgradeType)
            {
                case UpgradeType.Damage:
                    bow.damage *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.Health:
                    bow.health *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.HealthRegen:
                    bow.healthRegen *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.DrawTime:
                    bow.drawTime *= (1 - upgradeData.percentageIncrease); // Reduce draw time
                    break;
                case UpgradeType.Knockback:
                    bow.knockback *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.ArrowSpeed:
                    bow.arrowSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.MoveSpeed:
                    movement.moveSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
            }
        }

        Destroy(gameObject);
    }
    
}