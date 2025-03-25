using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public float maxHealth;
        public float currentHealth;

        // * Health Bar variables
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
            // * Instantiating the healthBarPrefab
            healthBarInstance = Instantiate(healthBarPrefab, transform);
            
            //* Getting the Slider component from the Slider prefab
            healthSlider = healthBarInstance.GetComponent<Slider>();

            if (healthSlider == null)
            {
                return;
            }

            //* Setting slider values 
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;

            healthSlider.value = currentHealth;

            healthSlider.wholeNumbers = false;

            //* Health bar color to green
            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.green;
            }

            //* Position the health bar above the entity
            healthBarInstance.transform.localPosition = healthBarOffset;
        }



        void Update()
        {
            if (healthBarInstance != null)
            {
                //* Heaælthbar follolws the entity
                healthBarInstance.transform.position = transform.position + healthBarOffset;

                //* Healthbar always faces the camera
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
            currentHealth -= damage;

            if (healthSlider != null)
            {
                healthSlider.value = currentHealth;
            }
            else
            {
            }

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        

        void Die()
        {

            if (healthBarInstance != null)
            {
                Destroy(healthBarInstance);
            }

            Destroy(gameObject);
        }
    }
}
