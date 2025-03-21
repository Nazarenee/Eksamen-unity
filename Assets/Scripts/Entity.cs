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
        [SerializeField] private Vector3 healthBarOffset = new Vector3(0, 1.5f, 0); 

        void Start()
        {
            currentHealth = maxHealth; 
            CreateHealthBar();
        }

        void CreateHealthBar()
        {
            healthBarInstance = Instantiate(healthBarPrefab, transform);
            healthSlider = healthBarInstance.GetComponent<Slider>();

            if (healthSlider == null)
            {
                Debug.LogError("Health bar prefab must have a Slider component!");
                return;
            }

            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;

            healthSlider.value = currentHealth;

            healthSlider.wholeNumbers = false;

            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.green;
            }

            healthBarInstance.transform.localPosition = healthBarOffset;
        }



        void Update()
        {
            if (healthBarInstance != null)
            {
                healthBarInstance.transform.position = transform.position + healthBarOffset;

                healthBarInstance.transform.LookAt(Camera.main.transform);
                healthBarInstance.transform.Rotate(0, 180, 0);

                healthSlider.value = currentHealth;

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

            if (healthBarInstance != null)
            {
                Destroy(healthBarInstance);
            }

            Destroy(gameObject);
        }
    }
}
