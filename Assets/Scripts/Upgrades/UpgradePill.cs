using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public UpgradeData upgradeData;
    private Renderer renderer;
    private bool isInFocus = false;

    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    public void Initialize(UpgradeData data, Material material)
    {
        upgradeData = data;
        renderer.material = material;
    }

    public void SetFocus(bool focus)
    {
        isInFocus = focus;
        // You can trigger a UI message here (e.g., "Press E to pick up")
    }

    private void Update()
    {
        if (isInFocus && Input.GetKeyDown(KeyCode.E))
        {
            //PickUpUpgrade();
        }
    }
    /*
    private void PickUpUpgrade()
    {
        HunterStats hunter = FindFirstObjectByType<HunterStats>(); // Assuming there's only one Hunter in the scene
        if (hunter != null)
        {
            hunter.ApplyUpgrade(upgradeData);
        }
        Destroy(gameObject);
    }
    */
}