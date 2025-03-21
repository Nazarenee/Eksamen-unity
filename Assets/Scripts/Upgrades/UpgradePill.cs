using System;
using UnityEngine;

public class UpgradePill : MonoBehaviour
{
    private UpgradeData upgradeData;
    private Transform player;
    public float tooltipRange = 3f;
    private bool isLookingAtUpgrade = false;
    private UpgradeTooltip tooltipInstance;
    public GameObject tooltipPrefab;

    public void Initialize(UpgradeData data, Material rarityMaterial)
    {
        upgradeData = data;
        GetComponent<Renderer>().material = rarityMaterial;
        player = GameObject.FindGameObjectWithTag("Hunter").transform;
        
        GameObject tooltipObject = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
        tooltipInstance = tooltipObject.GetComponent<UpgradeTooltip>();
        tooltipInstance.gameObject.SetActive(false);
    }

    private void Update()
    {
        {
            float distance = Vector3.Distance(player.position, transform.position);
            
            if (distance <= tooltipRange)
            {
                string tooltipMessage = $"{upgradeData.upgradeType}\nRarity: {upgradeData.rarity}\nIncrease: {upgradeData.percentageIncrease * 100}%";
                tooltipInstance.ShowTooltip(tooltipMessage, transform);
            }
            else if (tooltipInstance.gameObject.activeSelf) 
            {
                tooltipInstance.HideTooltip();
            }
            if(isLookingAtUpgrade && Input.GetKeyDown(KeyCode.E))
            {
                PickUpUpgrade();
            }
        }
    }

    private void OnMouseOver()
    {
        isLookingAtUpgrade = true;
    }
    
    private void OnMouseExit()
    {
        isLookingAtUpgrade = false;
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
        Destroy(tooltipInstance.gameObject);
        Destroy(gameObject);
    }

    
}