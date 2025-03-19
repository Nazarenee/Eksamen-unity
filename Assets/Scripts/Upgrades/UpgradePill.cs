using System;
using UnityEngine;

public class UpgradePill : MonoBehaviour
{
    private UpgradeData upgradeData;
    private bool isLookingAtUpgrade = false;

    public void Initialize(UpgradeData data, Material rarityMaterial)
    {
        upgradeData = data;
        GetComponent<Renderer>().material = rarityMaterial;
    }

    private void Update()
    {
        if (isLookingAtUpgrade && Input.GetKeyDown(KeyCode.E))
        {
            PickUpUpgrade();
        }
    }

    private void PickUpUpgrade()
    {
        GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
        PlayerMovement movement = hunter.GetComponent<PlayerMovement>();
        DamageBow bow = hunter.GetComponentInChildren<DamageBow>();
        Bow bowSpeed = hunter.GetComponentInChildren<Bow>();

        if (movement != null && bow != null)
        {
            switch (upgradeData.upgradeType)
            {
                case UpgradeType.Damage:
                    bow.Damage *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.DrawTime:
                    bowSpeed.FireCooldown *= (1 - upgradeData.percentageIncrease/4);
                    break;
                case UpgradeType.ArrowSpeed:
                    bow.bulletSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.MoveSpeed:
                    movement.walkSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
            }
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hunter"))
        {
            isLookingAtUpgrade = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hunter"))
        {
            isLookingAtUpgrade = false;
        }
    }
}