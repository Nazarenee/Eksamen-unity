using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePill : MonoBehaviour
{
    private UpgradeData upgradeData;
    private bool isLookingAtUpgrade = false;
    private Camera playerCamera;
    private GameObject hunter;
    private Canvas tooltipCanvas;
    [SerializeField] private TextMeshProUGUI tooltipText;
    [SerializeField] private Image borderImage;
    [SerializeField] private float interactionDistance = 5f;

    private void Start()
    {
        hunter = GameObject.FindGameObjectWithTag("Hunter");
        tooltipCanvas = GetComponentInChildren<Canvas>(true);
        playerCamera = hunter.GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            Debug.LogWarning("Camera not found on Hunter. Please ensure the camera is a child of the Hunter.");
        }
    }

    public void Initialize(UpgradeData data, Material rarityMaterial)
    {
        upgradeData = data;
        GetComponent<Renderer>().material = rarityMaterial;
        borderImage.color = rarityMaterial.color;
        
        tooltipText.text = $"{data.upgradeType}\n+{data.percentageIncrease*100}%";
        if (data.upgradeType == UpgradeType.DrawTime)
        {
            tooltipText.text = $"{data.rarity}\n{data.upgradeType}\n-{data.percentageIncrease/4*100}%";
        }
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(hunter.transform.position, transform.position);
        if (distanceToPlayer <= interactionDistance)
        {
            isLookingAtUpgrade = true;
            tooltipText.gameObject.SetActive(true);
            tooltipCanvas.gameObject.SetActive(true);
        }
        else
        {
            isLookingAtUpgrade = false;
            tooltipText.gameObject.SetActive(false);
            tooltipCanvas.gameObject.SetActive(false);
        }

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
        DespawnUpgrades();
        
    }

    public void DespawnUpgrades()
    {
        //Find and destory all UpgradePill
        GameObject[] upgrades = GameObject.FindGameObjectsWithTag("Upgrade");
        foreach (var upgrade in upgrades)
        {
            Destroy(upgrade);
        }
    }


}