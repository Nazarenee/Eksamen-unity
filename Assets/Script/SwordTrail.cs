using UnityEngine;

public class SwordTrail : MonoBehaviour
{
    public TrailRenderer trail;
    

    void Start()
    {
        trail.enabled = false; // Start disabled
    }

    public void StartTrail()
    {
        if (trail != null)
        {
            trail.enabled = true;
            Debug.Log("Trail Started!");
        }
    }

    public void StopTrail()
    {
        if (trail != null)
        {
            trail.enabled = false;
            Debug.Log("Trail Stopped!");
        }
    }
}