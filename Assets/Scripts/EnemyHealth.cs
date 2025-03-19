using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject bloodEffectPrefab; // Assign a blood particle system prefab in the Inspector

    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword")) // Assuming your sword has the "Sword" tag
        {
            Debug.Log("HIT ENEMY WITH SWORD!");
            TakeDamage(20, other.transform.position); // Damage from the sword
        }
    }
    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Debug.Log("enemy took damage: " + damage);
        health -= damage;

        // Spawn blood effect at hit position
        if (bloodEffectPrefab != null)
        {
            Instantiate(bloodEffectPrefab, hitPosition, Quaternion.identity);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject); // Handle enemy death
    }
}
