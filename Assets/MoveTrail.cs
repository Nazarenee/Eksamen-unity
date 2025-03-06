using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    [SerializeField] private Transform sword;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = sword.position;
        transform.rotation = sword.rotation;
    }
}
