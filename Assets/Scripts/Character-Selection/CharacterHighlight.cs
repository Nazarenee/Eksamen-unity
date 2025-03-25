using UnityEngine;

public class CharacterHighlight : MonoBehaviour
{
    public Material highlightMaterial;
    private Renderer[] modelRenderers;
    private Material[][] originalMaterials;
    
    void Start()
    {
        // * GetComponentsInChildren<Renderer>() returns all Renderers in the GameObject and its children
        modelRenderers = GetComponentsInChildren<Renderer>();
        
        // * Initializa array to the same length as modelRenderers
        originalMaterials = new Material[modelRenderers.Length][];
        
        // * Stores original materials for each Renderer

        for (int i = 0; i < modelRenderers.Length; i++)
        {
            originalMaterials[i] = modelRenderers[i].materials;
        }
    }
    
    void OnMouseEnter()
    {
        if (highlightMaterial != null)
        {
            // * all renders iterate
            foreach (Renderer renderer in modelRenderers)
            {
                // * create a new array of materials with the same length as the original materials
                Material[] newMaterials = new Material[renderer.materials.Length];
                
                 // * set all materials to the highlight material
                for (int i = 0; i < newMaterials.Length; i++)
                {
                    newMaterials[i] = highlightMaterial;
                }
                // * set the materials of the renderer to the new materials
                renderer.materials = newMaterials;
            }
        }
    }
    
    void OnMouseExit()
    {
        // * Resets the materials of the renderers to the original materials
        for (int i = 0; i < modelRenderers.Length; i++)
        {
            modelRenderers[i].materials = originalMaterials[i];
        }
    }
}