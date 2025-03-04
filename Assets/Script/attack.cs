using UnityEngine;

public class PlayerMagic : MonoBehaviour
{
    public Animator playerAnim;
    public GameObject fireEffectPrefab; 
    public Camera playerCamera; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && gameObject.CompareTag("Mage"))
        {
            playerAnim.SetTrigger("magic");

            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Ray hit: " + hit.point);

                GameObject fireball = Instantiate(fireEffectPrefab, hit.point, Quaternion.identity);

                Vector3 direction = hit.point - fireball.transform.position;
                fireball.transform.rotation = Quaternion.LookRotation(direction);

                Rigidbody rb = fireball.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = fireball.AddComponent<Rigidbody>();
                }
                rb.useGravity = false; 
                rb.linearVelocity = direction.normalized * 10f; 

                Destroy(fireball, 2f);
            }
            else
            {
                Debug.Log("Ray did not hit anything");
            }
        }
    }
}