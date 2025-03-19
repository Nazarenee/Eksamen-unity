using UnityEngine;
using UnityEngine.AI;

public class EnemyBossScript : MonoBehaviour
{
    public float lookRadius = 10f; // Detection range
    public float attackRadius = 2f; // Attack range
    public float attackCooldown = 2f; // Time between attacks

    private Transform target;
    private NavMeshAgent agent;
    private float nextAttackTime;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Hunter")?.transform; // Finds the player (Hunter)
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position); // Follow player
            
            if (distance <= attackRadius && Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + attackCooldown; // Set next attack time
            }
        }
    }

    void Attack()
    {
        Debug.Log("Boss Attacks the Hunter!");
        // TODO: Add attack animation, damage player, etc.
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius); // Detection range

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius); // Attack range
    }
}
