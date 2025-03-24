using UnityEngine;

public class CharacterHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    private Renderer[] modelRenderers;
    private Material[][] originalMaterials;
    
    void Start()
    {
        // Get all renderers in the character and its children
        modelRenderers = GetComponentsInChildren<Renderer>();
        
        // Store original materials
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
            // Apply highlight material to all renderers
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
        // Restore original materials
        for (int i = 0; i < modelRenderers.Length; i++)
        {
            modelRenderers[i].materials = originalMaterials[i];
        }
    }
}