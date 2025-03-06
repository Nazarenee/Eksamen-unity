using DefaultNamespace;
using UnityEngine;

public class DamageBow : MonoBehaviour
{
    public float Damage;
    public float BulletRange;
    private Camera playerCamera;
    

    void Start()
    {
        playerCamera = Camera.main;
    }

    public void Shoot()
    {
        // Get the mouse position and create a ray
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast and check if it hits something within the BulletRange
        if (Physics.Raycast(ray, out hit, BulletRange))
        {
            // Log hit info for debugging
            Debug.Log("Ray hit: " + hit.collider.gameObject.name);

            // If it hits an entity with the "Entity" component, apply damage
            if (hit.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.Health -= Damage;
                Debug.Log("Damaged enemy: " + enemy.name);

            }
        }
        else
        {
            Debug.Log("Ray did not hit anything");
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("HIT ENEMY!");
            EnemyHealth enemy = other.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                enemy.TakeDamage(20, other.transform.position);
            }
        }
    }
}