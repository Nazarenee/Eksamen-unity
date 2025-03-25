using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // If you want to update a UI element

public class HunterHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public int maxHealth = 10;
    public int currentHealth;

    [Header("UI References")] // Optional: for health display
    public Text healthText; // Optional UI Text component

    void Start()
    {
        // Initialize health to max at the start
        currentHealth = maxHealth;
        UpdateHealthDisplay();
    }

    public void TakeDamage(int damageAmount)
    {
        // Reduce health
        currentHealth -= damageAmount;

        // Update health display
        UpdateHealthDisplay();

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthDisplay()
    {
        // Optional: Update UI if you have a health text/UI
        if (healthText != null)
        {
            healthText.text = "Health: " + currentHealth + " / " + maxHealth;
        }

        // Optional: You might want to add a debug log
        Debug.Log("Current Health: " + currentHealth);
    }

    void Die()
    {
        
        Cursor.lockState = CursorLockMode.None;
    
        Cursor.visible = true;
        SceneManager.LoadScene("GameOver");
    }

    // Optional: Method to heal
    public void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        UpdateHealthDisplay();
    }
}