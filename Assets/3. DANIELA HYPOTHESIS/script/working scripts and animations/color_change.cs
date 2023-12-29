using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class color_change : MonoBehaviour
{
    public Color[] colors; // Array of colors to be applied
    public Renderer renderer; // Reference to the Renderer component of the object

    void Start()
    {
        renderer = GetComponent<Renderer>();
        // Set initial color
        if (colors.Length > 0)
        {
            renderer.material.color = colors[0];
        }
    }

    // Animation Event callback
    void OnColorChange(int colorIndex)
    {
        if (colorIndex >= 0 && colorIndex < colors.Length)
        {
            renderer.material.color = colors[colorIndex];
        }
    }
}
