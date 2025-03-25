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
            //* Invoke the event
            OnBowShoot?.Invoke();
            currentCooldown = FireCooldown;
        }

        // * Decreases cooldown
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
        }
    }
}