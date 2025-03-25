using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPDisplay : MonoBehaviour
{
    public TextMeshProUGUI  hpText; 

   
    private HunterHealth hunterHealth;

    void Start()
    {
        // * Canvas doesnt get destroyed when changing scenes
        DontDestroyOnLoad(this.gameObject);

        // * Finding the HunterHealth script
        hunterHealth = FindObjectOfType<HunterHealth>();

        // Ensure we have a Text component
        if (hpText == null)
        {
            Debug.LogError("HP Text is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // * Refinding the HunterHealth script (if it somehow messes up betweens rooms)
        if (hunterHealth == null)
        {
            hunterHealth = FindObjectOfType<HunterHealth>();
        }

        // * Updating the HP text
        if (hunterHealth != null && hpText != null)
        {
            hpText.text = $"HP: {hunterHealth.currentHealth}";
        }
    }
}