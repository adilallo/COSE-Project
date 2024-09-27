using UnityEngine;
using TMPro;  // Don't forget to include the TMPro namespace!

public class TextMeshColorChanger : MonoBehaviour
{
    // Define the colors to loop through
    public Color[] colors = new Color[4];

    // Speed of the color transition (4 seconds between each color change)
    public float duration = 4f;

    private TextMeshPro textMeshPro;
    private float timer;
    private int currentColorIndex = 0;
    private int nextColorIndex = 1;

    void Start()
    {
        // Get the TextMeshPro component of the object
        textMeshPro = GetComponent<TextMeshPro>();

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

        // Initialize the TextMeshPro's color to the first color
        textMeshPro.color = colors[currentColorIndex];
    }

    void Update()
    {
        // Increment the timer based on time
        timer += Time.deltaTime;

        // Determine how far we are into the current transition (0 to 1)
        float t = timer / duration;

        // Lerp between the current color and the next color
        textMeshPro.color = Color.Lerp(colors[currentColorIndex], colors[nextColorIndex], t);

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
