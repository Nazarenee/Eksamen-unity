using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Animator playerAnim;
    public GameObject fireEffectPrefab; 
    public Camera playerCamera; 

    [SerializeField] private float sensitivity = 5.0f; // Controls camera movement speed
    private float verticalRotation = 0f;  

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        RotateCamera();
        HandleMagicAttack();
    }

    void RotateCamera()
    {
        if (playerCamera == null) return;

        // * Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // * Rotate player horizontally (left/right)
        transform.Rotate(Vector3.up * mouseX);

        // * Rotate camera vertically (up/down)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -80f, 80f); 
        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
    }

    void HandleMagicAttack()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.CompareTag("Mage"))
        {
            playerAnim.SetTrigger("magic");

            Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)); 
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {

                //* Spawn fireball infront of player
                GameObject fireball = Instantiate(fireEffectPrefab, transform.position + transform.forward * 1.5f, Quaternion.identity);

                Vector3 direction = (hit.point - fireball.transform.position).normalized;
                // * Rotate to face direction
                fireball.transform.rotation = Quaternion.LookRotation(direction);

                Rigidbody rb = fireball.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = fireball.AddComponent<Rigidbody>();
                }
                rb.useGravity = false;
                rb.linearVelocity = direction * 10f; 

                Destroy(fireball, 2f);
            }
            else
            {
                Debug.Log("Ray did not hit anything");
            }
        }
    }
}
