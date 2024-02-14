using UnityEngine;

namespace COSE.Diagram
{
    public class DiagramElement : MonoBehaviour
    {
        public GameObject associatedSystem;
        private Outline outlineScript;
        private bool isSystemActive = false; // Track the state of the associated system

        private void Start()
        {
            // Optionally, ensure the associated system is initially deactivated
            if (associatedSystem)
            {
                associatedSystem.SetActive(false);
            }

            // Attempt to get the Outline component on this GameObject
            outlineScript = GetComponent<Outline>();
            if (outlineScript)
            {
                outlineScript.enabled = false; // Initially disable the outline
            }

            DiagramManager.Instance?.RegisterElement(this);
        }

        private void OnDestroy()
        {
            DiagramManager.Instance?.UnregisterElement(this);
        }

        private void OnMouseDown()
        {
            // Toggle the state of the associated system and outline
            isSystemActive = !isSystemActive;
            if (associatedSystem)
            {
                associatedSystem.SetActive(isSystemActive);
            }
            if (outlineScript)
            {
                outlineScript.enabled = isSystemActive; // Toggle the outline in the same way
            }

            // Notify the DiagramManager that this element was clicked
            DiagramManager.Instance?.DiagramElementClicked(this);
        }
    }
}
