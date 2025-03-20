using UnityEngine;
using TMPro;

public class UpgradeTooltip : MonoBehaviour
{
    public static UpgradeTooltip Instance; 

    public TextMeshProUGUI tooltipText;  // Assign in Inspector
    public Transform tooltipTransform;   // Assign in Inspector (UI Panel)
    private Transform targetUpgrade;     // The upgrade pill we're tracking

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false); // Hide on start
    }

    private void Update()
    {
        if (targetUpgrade != null)
        {
            transform.position = targetUpgrade.position + Vector3.up * 1.5f; // Float above upgrade
        }
    }

    public void ShowTooltip(string text, Transform upgradeTransform)
    {
        tooltipText.text = text;
        targetUpgrade = upgradeTransform;
        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
        targetUpgrade = null;
    }
}