using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vector2 = System.Numerics.Vector2;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;
    public GameObject bloodEffectPrefab; 

    
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword")) 
        {
            Debug.Log("HIT ENEMY WITH SWORD!");
            TakeDamage(20, other.transform.position); 
        }
    }
    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        Debug.Log("enemy took damage: " + damage);
        health -= damage;

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
        Destroy(gameObject); 
    }
}
