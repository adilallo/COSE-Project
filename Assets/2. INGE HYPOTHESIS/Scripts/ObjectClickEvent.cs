using UnityEngine;

namespace COSE.Events
{
    public class ObjectClickEvent : MonoBehaviour
    {
        public string objectId; // ID to identify the object
        private static ObjectClickEvent currentOutlinedObject = null;
        private Outline outline; // Reference to the Outline component

        public delegate void ObjectClickedHandler(string objectId);
        public static event ObjectClickedHandler OnObjectClicked;

        void Start()
        {
            outline = GetComponent<Outline>();
            if (outline == null)
            {
                outline = gameObject.AddComponent<Outline>();
            }
            outline.enabled = false;
        }

        void OnMouseDown()
        {
            // Disable the outline of the previously outlined object, if any
            if (currentOutlinedObject != null && currentOutlinedObject != this)
            {
                currentOutlinedObject.outline.enabled = false;
            }

            // Toggle the outline on this object
            outline.enabled = !outline.enabled;

            // Update the current outlined object reference
            if (outline.enabled)
            {
                currentOutlinedObject = this;
            }
            else if (currentOutlinedObject == this)
            {
                currentOutlinedObject = null;
            }

            OnObjectClicked?.Invoke(objectId);
        }
    }
}
