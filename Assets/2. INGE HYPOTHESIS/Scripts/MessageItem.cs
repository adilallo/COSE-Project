using UnityEngine;
using COSE.Text;

namespace COSE.Messages
{
    public class MessageItem : MonoBehaviour
    {
        // A list to hold references to the GameObjects containing the text
        [SerializeField] private TextInteraction textInteraction;

        // Method to toggle visibility based on messageIndex
        public void ToggleMessage(int index)
        {
            Debug.Log($"Toggling message with index: {index}");
            textInteraction.ActivateConclusionText(index);
        }
    }
}
