using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace COSE.Messages
{
    public class MessageButton : MonoBehaviour
    {
        public GameObject inboxPanel;
        public GameObject messagePrefab;
        public Transform messageContainer;

        // Method called when the button is clicked
        public void OnButtonClicked()
        {
            inboxPanel.SetActive(true); // Show the inbox panel
        }

        // Method to add a message to the inbox
        public void AddMessage(string messageText, int index)
        {
            // Instantiate the prefab
            GameObject messageInstance = Instantiate(messagePrefab, messageContainer);
            messageInstance.SetActive(true); // Ensure the instance is active

            // Set the message text
            TextMeshProUGUI textComponent = messageInstance.GetComponentInChildren<TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = messageText;
            }

            // Get the Button component and add a listener for the click event
            Button messageButton = messageInstance.GetComponent<Button>();
            if (messageButton != null)
            {
                // Remove all existing listeners to avoid stacking them
                messageButton.onClick.RemoveAllListeners();

                // Add a new listener to toggle the message
                // Pass 'inboxPanel' and 'index' to the listener
                messageButton.onClick.AddListener(() => ToggleMessageOnPanel(index));
            }
        }

        // Method to toggle the message visibility on the inbox panel
        private void ToggleMessageOnPanel(int index)
        {
            MessageItem messageItem = inboxPanel.GetComponent<MessageItem>();
            if (messageItem != null)
            {
                messageItem.ToggleMessage(index);
            }
            else
            {
                Debug.LogError("MessageItem component not found on inboxPanel");
            }
        }
    }
}
