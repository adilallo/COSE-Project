using UnityEngine;

public class color_change : MonoBehaviour
{
    public Color[] colors;
    private Material material;

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            // Create a unique material instance for this object
            material = renderer.material;

            if (colors.Length > 0)
            {
                // Set initial color
                material.SetColor("_BaseColor", colors[0]);
                material.SetFloat("_Alpha", 0.5f);
            }
        }
    }

    // Animation Event callback to change color
    public void OnColorChange(int colorIndex)
    {
        if (material != null && colorIndex >= 0 && colorIndex < colors.Length)
        {
            material.SetColor("_BaseColor", colors[colorIndex]);
        }
    }

    // Adjust the Alpha property for visibility
    public void SetVisibility(bool isVisible)
    {
        if (material != null)
        {
            float alphaValue = isVisible ? 0.5f : 0f;
            material.SetFloat("_Alpha", alphaValue);
        }
        else
        {
            Debug.LogWarning($"{gameObject.name}: Material not found!");
        }
    }
}
