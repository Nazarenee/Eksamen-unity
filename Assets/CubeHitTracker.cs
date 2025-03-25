using UnityEngine;

public class CubeHitTracker : MonoBehaviour
{
    public Color boringColor = Color.gray; // Color after being hit
    private bool isHit = false;
    private MeshRenderer meshRenderer;

    private static int hitCount = 0; // Shared count for all cubes
    private static int totalCubes = 4; // Adjust if needed

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material.color = Color.red; // Start as red
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isHit && collision.gameObject.CompareTag("Arrow")) // Make sure only arrows count
        {
            isHit = true;
            meshRenderer.material.color = boringColor;
            hitCount++;

            Debug.Log($"Cube hit! {hitCount}/{totalCubes} cubes down.");

            if (hitCount >= totalCubes)
            {
                MakeBossVulnerable();
            }
        }
    }

    private void MakeBossVulnerable()
    {
        EnemyBossScript boss = FindObjectOfType<EnemyBossScript>();
        if (boss != null)
        {
            boss.MakeVulnerable();
        }
        else
        {
            Debug.LogError("Boss script not found in the scene!");
        }
    }
}