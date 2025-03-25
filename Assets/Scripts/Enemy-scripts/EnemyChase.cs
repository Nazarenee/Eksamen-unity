using UnityEngine;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour
{
    [Header("Movement Settings")]
    public float detectionRadius = 10f;
    public float stopDistance = 1.5f;

    [Header("Animation Settings")]
    public float speedThreshold = 0.5f;

    [Header("Damage Settings")]
    public float damageInterval = 1f; // Time between damage ticks
    private float lastDamageTime;

    private NavMeshAgent agent;
    private Animator animator;
    private Transform player;

    // Animation parameter name (match exactly with your Animator)
    private const string SPEED_PARAMETER = "Speed";

    void Start()
    {
        // Get NavMesh Agent component
        agent = GetComponent<NavMeshAgent>();
        
        // Get Animator component
        animator = GetComponentInChildren<Animator>();

        // Null checks
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on " + gameObject.name);
            enabled = false;
            return;
        }

        if (animator == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
            enabled = false;
            return;
        }

        // Find player
        player = GameObject.FindGameObjectWithTag("Hunter").transform;
    }

    void Update()
    {
        // Null checks
        if (player == null) return;

        // Always chase player
        agent.SetDestination(player.position);

        // Set animation speed based on agent velocity
        float animationSpeed = agent.velocity.magnitude > speedThreshold ? 1f : 0.01f;
        animator.SetFloat(SPEED_PARAMETER, animationSpeed);
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if the collided object is the hunter
        if (collision.gameObject.CompareTag("Hunter"))
        {
            // Check if enough time has passed since last damage
            if (Time.time - lastDamageTime >= damageInterval)
            {
                // Try to get the HunterHealth component
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