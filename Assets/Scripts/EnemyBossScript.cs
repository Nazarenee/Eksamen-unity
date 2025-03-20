using UnityEngine;
using UnityEngine.UI;
using TMPro;  // ✅ Make sure to use TextMeshPro
using System.Collections;

public class EnemyBossScript : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthFill;
    [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 2f, 0);

    [SerializeField] private TextMeshProUGUI warningText; // ✅ Using TMP

    private bool isVulnerable = false;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthSlider == null)
        {
            Debug.LogError("HealthSlider is not assigned!");
            return;
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        UpdateHealthBarColor();

        // ✅ Make sure TMP text is active but invisible
        if (warningText != null)
        {
            warningText.gameObject.SetActive(true);
            warningText.text = ""; // Hide text
        }
        else
        {
            Debug.LogError("WarningText (TMP) is not assigned!");
        }
    }

    void Update()
    {
        if (healthSlider != null)
        {
            healthSlider.transform.position = transform.position + healthBarOffset;
        }
    }

    public void MakeVulnerable()
    {
        isVulnerable = true;
        Debug.Log("Boss is now vulnerable!");
    }

    public void TakeDamage(float damage, Vector3 hitPosition)
    {
        if (!isVulnerable)
        {
            Debug.Log("Boss is invulnerable! Destroy the weak spot first.");
            ShowWarning(); // ✅ Show text when hit while invulnerable
            return;
        }

        Debug.Log("Damage taken: " + damage + " | Before damage: " + currentHealth);
        currentHealth -= damage;
        Debug.Log("After damage: " + currentHealth);

        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
            UpdateHealthBarColor();
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

        if (healthPercent > 0.5f)      
            healthFill.color = Color.green;
        else if (healthPercent > 0.25f) 
            healthFill.color = Color.yellow;
        else                           
            healthFill.color = Color.red;
    }

    void Die()
    {
        Debug.Log("Boss died!");
        if (healthSlider != null)
        {
            Destroy(healthSlider.gameObject);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) 
        {
            TakeDamage(10f, other.transform.position);
            Destroy(other.gameObject);
        }
    }

    // ✅ Show warning text for 2 seconds
    private void ShowWarning()
    {
        if (warningText != null)
        {
            StopCoroutine(HideWarning()); // Reset timer if hit again
            warningText.text = "Destroy the weakspot first.";
            StartCoroutine(HideWarning());
        }
    }

    private IEnumerator HideWarning()
    {
        yield return new WaitForSeconds(2f);
        if (warningText != null)
        {
            warningText.text = ""; // Hide text after delay
        }
    }
}
