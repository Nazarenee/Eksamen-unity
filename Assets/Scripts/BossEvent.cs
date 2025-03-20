using UnityEngine;

public class BossEvent : MonoBehaviour
{
    public EnemyBossScript boss; // Assign the boss in the Inspector
    private bool isDestroyed = false;

    public void TakeDamage(float damage)
    {
        if (isDestroyed) return; // If already destroyed, ignore

        Debug.Log("Weak Spot Destroyed!");
        isDestroyed = true;
        boss.MakeVulnerable(); // Tell the boss it can take damage now
        Destroy(gameObject); // Destroy the weak spot
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet")) // Make sure the bullet has the correct tag
        {
            TakeDamage(10f); // Example: Weak spot takes 10 damage per shot
            Destroy(other.gameObject); // Destroy the bullet
        }
    }
}