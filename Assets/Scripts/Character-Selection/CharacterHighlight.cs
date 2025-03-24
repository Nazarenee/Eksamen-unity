using UnityEngine;

public class CharacterHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    private Renderer[] modelRenderers;
    private Material[][] originalMaterials;
    
    void Start()
    {
        modelRenderers = GetComponentsInChildren<Renderer>();
        
        originalMaterials = new Material[modelRenderers.Length][];
        for (int i = 0; i < modelRenderers.Length; i++)
        {
            originalMaterials[i] = modelRenderers[i].materials;
        }
    }
    
    void OnMouseEnter()
    {
        if (highlightMaterial != null)
        {
            foreach (Renderer renderer in modelRenderers)
            {
                Material[] newMaterials = new Material[renderer.materials.Length];
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = highlightMaterial;
                }
                renderer.materials = newMaterials;
            }
        }
    }
    
    void OnMouseExit()
    {
        for (int i = 0; i < modelRenderers.Length; i++)
        {
            modelRenderers[i].materials = originalMaterials[i];
        }
    }
}