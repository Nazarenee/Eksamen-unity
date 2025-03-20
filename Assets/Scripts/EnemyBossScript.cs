using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class EnemyBossScript : MonoBehaviour
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
            // Check if world space canvas exists, create if not
            Canvas worldCanvas;
            GameObject canvasObj = GameObject.Find("WorldSpaceCanvas");

            if (canvasObj == null)
            {
                canvasObj = new GameObject("WorldSpaceCanvas");
                worldCanvas = canvasObj.AddComponent<Canvas>();
                worldCanvas.renderMode = RenderMode.WorldSpace;
                canvasObj.AddComponent<CanvasScaler>();
                canvasObj.AddComponent<GraphicRaycaster>();
            }
            else
            {
                worldCanvas = canvasObj.GetComponent<Canvas>();
            }

            // Instantiate health bar
            healthBarInstance = Instantiate(healthBarPrefab, worldCanvas.transform);
            healthSlider = healthBarInstance.GetComponent<Slider>();

            if (healthSlider == null)
            {
                Debug.LogError("Health bar prefab must have a Slider component!");
                return;
            }

            // Set slider values
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHealth;
            healthSlider.value = maxHealth; // Force to max value

            // Force UI refresh
            healthSlider.wholeNumbers = false;  // Ensure smooth scaling
            healthSlider.value = maxHealth;     // Set again to update UI

            // Get fill image
            Image fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = Color.green;
            }
        }

        
        void Update()
        {
            if (healthBarInstance != null)
            {
                // Position health bar above enemy
                healthBarInstance.transform.position = transform.position + healthBarOffset;
                
                // Make health bar face the camera
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
                Debug.Log("Calling EnemyBossDie() for: " + gameObject.name);
                EnemyBossDie();
            }
        }
        
        void EnemyBossDie()
        {
            Debug.Log("Boss died!");
            
            // Destroy health bar when enemy dies
            if (healthBarInstance != null)
            {
                Destroy(healthBarInstance);
            }
            
            Destroy(gameObject);
        }
    }
}
