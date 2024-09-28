using UnityEngine;

public class ExitOnCollide : MonoBehaviour
{
    // Ensure the object this script is attached to has "Is Trigger" checked in the Collider component.

    // This method will be called when another collider enters the trigger zone.
    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that collided with the trigger is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Exit the application when the player enters the trigger
            Debug.Log("Player has triggered exit.");
            ExitGame();
        }
    }

    // Exit the game, works only in a built application (not in the editor)
    void ExitGame()
    {
        #if UNITY_EDITOR
            // If in the editor, stop playing the scene
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // In a built application, quit the game
            Application.Quit();
        #endif
    }
}
