using UnityEngine;
using UnityEngine.Events;

public class Bow : MonoBehaviour
{
    public UnityEvent OnBowShoot;
    public float FireCooldown;
    public bool Automatic;

    private float CurrentCooldown;

    void Start()
    {
        CurrentCooldown = FireCooldown;
    }

    void Update()
    {
        if (Automatic)
        {
            if (Input.GetMouseButton(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnBowShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (CurrentCooldown <= 0f)
                {
                    OnBowShoot?.Invoke();
                    CurrentCooldown = FireCooldown;
                }
            }
        }

        CurrentCooldown -= Time.deltaTime;
    }
}