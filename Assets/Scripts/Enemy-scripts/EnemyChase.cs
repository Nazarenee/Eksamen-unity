using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    
    public float speedThreshold = 0.5f;

    public float damageInterval = 1f; 
    private float lastDamageTime;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;


    //* Blend tree paramenter
    private const string SPEED_PARAMETER = "Speed";

    void Start()
    {
        // * Getting the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
        
        // * Getting the Animator component 
        animator = GetComponentInChildren<Animator>();

       
        if (agent == null)
        {
            enabled = false;
            return;
        }

        if (animator == null)
        {
            enabled = false;
            return;
        }

        player = GameObject.FindGameObjectWithTag("Hunter").transform;
    }

    void Update()
    {
        if (player == null) return;

        // Setting the Enemies Agents to the players position. So it always follows the player 
        agent.SetDestination(player.position);

        // Set animation speed based on agent velocity
        float animationSpeed = agent.velocity.magnitude > speedThreshold ? 1f : 0.01f;
        animator.SetFloat(SPEED_PARAMETER, animationSpeed);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hunter"))
        {
            // * Time function, checks if enough time has pased to reactivate damage
            if (Time.time - lastDamageTime >= damageInterval)
            {
                
                HunterHealth hunterHealth = collision.gameObject.GetComponent<HunterHealth>();
                
                if (hunterHealth != null)
                {
                    hunterHealth.TakeDamage(20);
                    lastDamageTime = Time.time;
                }
            }
        }
    }
}