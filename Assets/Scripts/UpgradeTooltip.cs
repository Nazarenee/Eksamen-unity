using UnityEngine;
using TMPro;

public class UpgradeTooltip : MonoBehaviour
{
    public static UpgradeTooltip Instance; 

    public TextMeshProUGUI tooltipText;  // Assign in Inspector
    public Transform tooltipTransform;   // Assign in Inspector (UI Panel)
    private Transform targetUpgrade;     // The upgrade pill we're tracking
    

    private void Update()
    {
        if (targetUpgrade != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(targetUpgrade.position + Vector3.up * 1.5f);
            tooltipTransform.position = screenPos;
        }
    }

    public void ShowTooltip(string text, Transform upgradeTransform)
    {
        if (tooltipText == null)
        {
            Debug.LogError("Tooltip Text is NOT assigned!");
            return;
        }

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