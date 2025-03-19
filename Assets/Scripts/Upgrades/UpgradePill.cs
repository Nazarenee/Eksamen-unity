using System;
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
        
        GameObject hunter = GameObject.FindGameObjectWithTag("Hunter");
        GameObject warrior = GameObject.FindGameObjectWithTag("Warrior");
        GameObject mage = GameObject.FindGameObjectWithTag("Mage");
        
        GameObject player = hunter;
        
        DamageBow bow = player.GetComponentInChildren<DamageBow>();
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        Bow bowSpeed = player.GetComponent<Bow>();

        if (bow != null)
        {
            switch (upgradeData.upgradeType)
            {
                case UpgradeType.Damage:
                    bow.Damage *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.DrawTime:
                    bowSpeed.FireCooldown *= (1 - (upgradeData.percentageIncrease/4)); 
                    break;
                case UpgradeType.ArrowSpeed:
                    bow.bulletSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
                case UpgradeType.MoveSpeed:
                    movement.walkSpeed *= (1 + upgradeData.percentageIncrease);
                    movement.rotationSpeed *= (1 + upgradeData.percentageIncrease);
                    break;
            }
        }

        Destroy(gameObject);
    }
    
}