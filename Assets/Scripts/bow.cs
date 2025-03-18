using UnityEngine;
using UnityEngine.Events;

public class Bow : MonoBehaviour
{
    public UnityEvent OnBowShoot; 
    public float FireCooldown = 2f;  
    private float currentCooldown = 0f;  

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentCooldown <= 0f)
        {
            OnBowShoot?.Invoke();
            currentCooldown = FireCooldown;
        }

        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}