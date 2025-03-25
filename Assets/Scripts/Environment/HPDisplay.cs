using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPDisplay : MonoBehaviour
{
    public TextMeshProUGUI  hpText; // Reference to the UI Text component

    // Reference to the HunterHealth script to get current HP
    private HunterHealth hunterHealth;

    void Start()
    {
        // Ensure the Canvas doesn't get destroyed when loading new rooms
        DontDestroyOnLoad(this.gameObject);

        // Find the HunterHealth component on the player
        hunterHealth = FindObjectOfType<HunterHealth>();

        // Ensure we have a Text component
        if (hpText == null)
        {
            Debug.LogError("HP Text is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Refind HunterHealth if it's null (useful if player is in a new room)
        if (hunterHealth == null)
        {
            hunterHealth = FindObjectOfType<HunterHealth>();
        }

        // Update the text if we have a HunterHealth and Text component
        if (hunterHealth != null && hpText != null)
        {
            hpText.text = $"HP: {hunterHealth.currentHealth}";
        }
    }
}