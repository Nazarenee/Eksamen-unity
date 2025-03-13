using UnityEngine;

namespace DefaultNamespace
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] public float maxHealth;
        public float currentHealth;

        void Start()
        {
            currentHealth = maxHealth; // Enemy starts with full health
        }

        public void TakeDamage(float damage, Vector3 hitPosition)
        {
            Debug.Log("Damage taken: " + damage + " | Before damage: " + currentHealth);
            currentHealth -= damage;
            Debug.Log("After damage: " + currentHealth);

            if (currentHealth <= 0)
            {
                Debug.Log("Calling Die() for: " + gameObject.name); 
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Enemy died!");
            Destroy(gameObject); 
        }
    }
}