using UnityEngine;

public class EnemyAttackAnimation : MonoBehaviour
{
    public float attackRange = 5.1f;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hunter")?.transform;
        animator = GetComponent<Animator>(); // Get Animator attached to the enemy
    }

    void Update()
    {
        CheckPlayerDistance();
    }

    void CheckPlayerDistance()
    {
        if (player == null || animator == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Set the "IsAttacking" parameter in the Animator
        animator.SetBool("IsAttacking", distance <= attackRange);
    }
}