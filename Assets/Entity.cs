using UnityEngine;

namespace DefaultNamespace
{
    public class Entity : MonoBehaviour
    {
        [SerializeField] private float StartingHealth;
        private float health;
        

        public float Health
        {
            get { return health; }
            set
            {
                health = value;
                Debug.Log(health);
                if (health <= 0f)
                {
                    Destroy(gameObject);
                }
            }
        }

        void Start()
        {
            Health = StartingHealth;
        }
        
    }
} 