using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class EnemyBossScript : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Slider healthSlider; // Assign in Inspector
    [SerializeField] private Image healthFill;   // Assign the Fill Image of the Slider in Inspector
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 5f, 0); // Adjust Y for height above the boss

    private bool isVulnerable = false; // Boss starts as invulnerable

    [SerializeField] private TMP_Text warningText; // Assign in Inspector

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider is not assigned! Drag your Slider into the Inspector.");
            return;
        }

        if (warningText == null)
        {
            Debug.LogError("WarningText (TMP) is not assigned! Drag your TextMeshPro object into the Inspector.");
            return;
        }

        // Hide warning text at the start
        warningText.gameObject.SetActive(false);

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthBarColor();
    }

    void Update()
    {
        if (healthSlider != null)
        {
            // Ensure the health bar follows the boss and stays above it
            healthSlider.transform.position = transform.position + healthBarOffset;
        }
    }

    public void MakeVulnerable()
    {
        isVulnerable = true; // Now the boss can take damage
        Debug.Log("Boss is now vulnerable!");
    }

    public void TakeDamage(float damage, Vector3 hitPosition)
    {
        if (!isVulnerable)
        {
            Debug.Log("Boss is invulnerable! Destroy the weak spot first.");
            ShowWarning(); // Show the warning text
            return;
        }

        Debug.Log("Damage taken: " + damage + " | Before damage: " + currentHealth);
        currentHealth -= damage;
        Debug.Log("After damage: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            UpdateHealthBarColor(); // ✅ Change color based on health
        }

        if (currentHealth <= 0)
        {
            Debug.Log("Boss defeated!");
            Die();
        }
    }

    void UpdateHealthBarColor()
    {
        if (healthFill == null)
        {
            Debug.LogError("HealthFill Image is not assigned!");
            return;
        }

        float healthPercent = currentHealth / maxHealth;

        if (healthPercent > 0.5f)      // Green (Above 50%)
            healthFill.color = Color.green;
        else if (healthPercent > 0.25f) // Yellow (Between 25% and 50%)
            healthFill.color = Color.yellow;
        else                           // Red (Below 25%)
            healthFill.color = Color.red;
    }

    void Die()
    {
        Debug.Log("Boss died!");

        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject); // Destroy health bar if boss dies
        }

        Destroy(gameObject); // Destroy the boss object
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // Ensure bullets hit the boss
        {
            TakeDamage(10f, other.transform.position);
            Destroy(other.gameObject); // Destroy the bullet
        }
    }

    private void ShowWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(true); // Show the warning
            CancelInvoke(nameof(HideWarning)); // Cancel any previous hide call
            Invoke(nameof(HideWarning), 2f); // Hide after 2 seconds
        }
    }

    private void HideWarning()
    {
        if (warningText != null)
        {
            warningText.gameObject.SetActive(false); // Hide the warning
        }
    }
}
