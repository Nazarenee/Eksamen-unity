using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public float maxHealth;
        public float currentHealth;

        // Health Bar variables
        [SerializeField] private GameObject healthBarPrefab;
        private GameObject healthBarInstance;
        private Slider healthSlider;
        [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0); // Adjust based on your character height

        void Start()
        {
            currentHealth = maxHealth; // Enemy starts with full health
            CreateHealthBar();
        }

        void CreateHealthBar()
        {
            // Instantiate the health bar and assign it to the correct parent
            healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthSlider = healthBarInstance.GetComponent<Slider>();

            if (healthSlider == null)
            {
                Debug.LogError("Health bar prefab must have a Slider component!");
                return;
            }

            // Set the min and max values correctly (Ensure maxHealth is a valid number)
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;

            // Set the initial health value (shouldn't be 0, unless the enemy starts dead)
            healthSlider.value = currentHealth;

            // Ensure slider is not in whole number mode (for smooth transitions)
            healthSlider.wholeNumbers = false;

            // Debug log to check values
            // Set fill image color (green initially)
            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.green;
            }

            // Ensure the health bar is positioned above the enemy
            healthBarInstance.transform.localPosition = healthBarOffset;
        }



        void Update()
        {
            if (healthBarInstance != null)
            {
                // Position health bar above enemy
                healthBarInstance.transform.position = transform.position + healthBarOffset;

                // Make health bar face the camera (to always be visible from the player's perspective)
                healthBarInstance.transform.LookAt(Camera.main.transform);
                healthBarInstance.transform.Rotate(0, 180, 0);

                // Update health bar value (for smooth transitions)
                healthSlider.value = currentHealth;

                // Update color based on health percentage
                UpdateHealthBarColor();
            }
        }

        private void UpdateHealthBarColor()
        {
            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                float healthPercentage = currentHealth / maxHealth;

                if (healthPercentage > 0.6f)
                {
                    fillImage.color = Color.green;
                }
                else if (healthPercentage > 0.3f)
                {
                    fillImage.color = Color.yellow;
                }
                else
                {
                    fillImage.color = Color.red;
                }
            }
        }

        public void TakeDamage(float damage, Vector3 hitPosition)
        {
            Debug.Log("Damage taken: " + damage + " | Before damage: " + currentHealth);
            currentHealth -= damage;
            Debug.Log("After damage: " + currentHealth);

            // Update health bar value
            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
                Debug.Log("Updating health slider to: " + currentHealth);
            }
            else
            {
                Debug.LogError("healthSlider is null in TakeDamage!");
            }

            if (currentHealth <= 0)
            {
                Debug.Log("Calling Die() for: " + gameObject.name);
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Enemy died!");

            // Destroy health bar when enemy dies
            if (healthBarInstance != null)
            {
                Destroy(healthBarInstance);
            }

            Destroy(gameObject);
        }
    }
}
