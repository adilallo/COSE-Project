using UnityEngine;

namespace COSE.Interactions
{
    public class SphereTrigger : MonoBehaviour
    {
        public SphereInteraction sphereInteraction; // Reference to the SphereInteraction script
        public int sphereIndex; // Index of the sphere

        void OnTriggerEnter(Collider other)
        {
            // Check if the player has collided with this sphere
            if (other.CompareTag("Player"))
            {
                sphereInteraction.OnSphereTriggered(sphereIndex);
            }
        }
    }
}
