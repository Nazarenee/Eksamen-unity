using DefaultNamespace;
using UnityEngine;

public class DamageBow : MonoBehaviour
{
    [SerializeField] float Damage = 10f;
    public float BulletRange = 50f;
    [SerializeField] Camera playerCamera;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    public void Shoot()
    {
        if (playerCamera == null)
        {
            Debug.LogError("playerCamera is not assigned!");
            return;
        }

        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, BulletRange))
        {
            Vector3 targetPosition = hit.point;

            var arrow = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            Vector3 direction = (targetPosition - bulletSpawnPoint.position).normalized;

            arrow.transform.rotation = Quaternion.LookRotation(direction);

            arrow.transform.rotation = Quaternion.Euler(90, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            
            if (rb != null)
            {
                rb.linearVelocity = direction * bulletSpeed;
            }
            else
            {
                Debug.LogError("Arrow prefab does not have a Rigidbody component!");
            }

            if (hit.collider.gameObject.TryGetComponent(out Entity enemy))
            {
                enemy.TakeDamage(Damage, hit.point);
            }
        }
        else
        {
            Debug.Log("Ray did not hit anything");
        }
    }
}
