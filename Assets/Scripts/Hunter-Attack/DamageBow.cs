using DefaultNamespace;
using UnityEngine;

public class DamageBow : MonoBehaviour
{
    [SerializeField] public float Damage = 10f;
    public float BulletRange = 50f;
    [SerializeField] private Camera playerCamera;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    

    [SerializeField] private float sensitivity = 5.0f; 
    private float verticalRotation = 0f;
    
    
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
    }
    
    void RotateCamera()
    {
        if (playerCamera == null) return;

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity; 
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Rotate the player
        transform.Rotate(Vector3.up * mouseX);

        // * Rotate the camera
        verticalRotation -= mouseY;
        //* Clamp the vertical rotation
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f); 
        //* Apply the rotation
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    public void Shoot()
    {
        if (playerCamera == null)
        {
            Debug.LogError("playerCamera is not assigned!");
            return;
        }

        // *  Ray from screen center
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); 
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, BulletRange))
        {
            // * Hit point
            Vector3 targetPosition = hit.point;
            
            // * Spawn the arrow
            var arrow = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            
            Vector3 direction = (targetPosition - bulletSpawnPoint.position).normalized;
            
            // * Arrow rotation ( When counting for mouse position != camera position)
            arrow.transform.rotation = Quaternion.LookRotation(direction);
            arrow.transform.rotation = Quaternion.Euler(90, arrow.transform.rotation.eulerAngles.y, arrow.transform.rotation.eulerAngles.z);

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            Debug.Log("Raycast hit: " + hit.collider.name);

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
            else  if (hit.collider.gameObject.TryGetComponent(out EnemyBossScript enemy1))
            {
                enemy1.TakeDamage(Damage, hit.point);
            }
        }
        else
        {
            Debug.Log("Ray did not hit anything");
        }
    }
    
 
}
