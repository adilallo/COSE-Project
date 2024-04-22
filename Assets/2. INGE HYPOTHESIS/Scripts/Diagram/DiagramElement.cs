using UnityEngine;

namespace COSE.Diagram
{
    public class DiagramElement : MonoBehaviour
    {
        public GameObject associatedSystem;
        private Outline outlineScript;
        private bool isSystemActive = false;

        private void Start()
        {
            if (associatedSystem)
            {
                associatedSystem.SetActive(false);
            }

            outlineScript = GetComponent<Outline>();
            if (outlineScript)
            {
                outlineScript.enabled = false;
            }

            DiagramManager.Instance?.RegisterElement(this);
        }

        private void OnDestroy()
        {
            DiagramManager.Instance?.UnregisterElement(this);
        }

        private void OnMouseDown()
        {
            isSystemActive = !isSystemActive;
            if (associatedSystem)
            {
                associatedSystem.SetActive(isSystemActive);
            }
            if (outlineScript)
            {
                outlineScript.enabled = isSystemActive;
            }

            DiagramManager.Instance?.DiagramElementClicked(this);
        }
    }
}
