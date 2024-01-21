using UnityEngine;

namespace COSE.Interactions
{
    public class SphereTrigger : MonoBehaviour
    {
        public SphereInteraction sphereInteraction; // Reference to the SphereInteraction script
        public int sphereIndex; // Index of the sphere

        private Collider sphereCollider;

        void Start()
        {
            // Get the collider component attached to this sphere
            sphereCollider = GetComponent<Collider>();
        }

        void OnTriggerEnter(Collider other)
        {
            // Check if the player has collided with this sphere
            if (other.CompareTag("Player"))
            {
                sphereInteraction.OnSphereTriggered(sphereIndex);

                // Disable the collider to prevent further triggers
                if (sphereCollider != null)
                {
                    sphereCollider.enabled = false;
                }
            }
        }
    }
}
