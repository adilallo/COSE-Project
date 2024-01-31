using UnityEngine;

namespace COSE.Diagram
{
    public class DiagramElement : MonoBehaviour
    {
        public GameObject associatedSystem;

        private void Start()
        {
            DiagramManager.Instance?.RegisterElement(this);
        }

        private void OnDestroy()
        {
            DiagramManager.Instance?.UnregisterElement(this);
        }

        private void OnMouseEnter()
        {
            associatedSystem.SetActive(true);
        }

        private void OnMouseExit()
        {
            associatedSystem.SetActive(false);
        }
    }
}
