using UnityEngine;

namespace COSE.Interactions
{
    public class Sphere1Trigger : MonoBehaviour
    {
        public SphereInteraction sphereInteraction; // Reference to the main script

        void OnTriggerEnter(Collider other)
        {
            // Check if the player has collided with Sphere 1
            if (other.CompareTag("Player"))
            {
                sphereInteraction.OnSphere1Triggered();
            }
        }
    }
}
