using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    // Define the colors to loop through
    public Color[] colors = new Color[4];
    
    // Speed of the color transition (4 seconds between each color change)
    public float duration = 4f;

    private Renderer objectRenderer;
    private float timer;
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;

    void Start()
    {
        // Get the Renderer component of the sphere
        objectRenderer = GetComponent<Renderer>();

        // Set up initial colors if none are assigned in the inspector
        if (colors.Length < 4)
        {
            colors = new Color[]
            {
                Color.red,    // First color
                Color.green,  // Second color
                Color.blue,   // Third color
                Color.yellow  // Fourth color
            };
        }

        // Initialize the sphere's color to the first color
        objectRenderer.material.color = colors[currentColorIndex];
    }

    void Update()
    {
        // Increment the timer based on time
        timer += Time.deltaTime;

        // Determine how far we are into the current transition (0 to 1)
        float t = timer / duration;

        // Lerp between the current color and the next color
        objectRenderer.material.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

        // When the transition is complete (t >= 1), reset the timer and update the color indices
        if (t >= 1f)
        {
            // Reset the timer
            timer = 0f;

            // Update the color indices to loop through the colors
            currentColorIndex = nextColorIndex;
            nextColorIndex = (nextColorIndex + 1) % colors.Length;  // Loop back to 0 after reaching the last color
        }
    }
}
