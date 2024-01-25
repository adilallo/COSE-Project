using COSE.Hypothesis;
using COSE.Text;
using UnityEngine;

namespace COSE.Sphere
{
    public class SphereInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] inactiveSpheres;
        [SerializeField] private HypothesisInteraction hypothesisMovement;
        [SerializeField] private TextInteraction textInteraction;

        public void HandleSphereTriggered(int index)
        {
                OnSphereTriggered(index);
        }

        public void OnSphereTriggered(int sphereIndex)
        {
            if (sphereIndex == 1)
            {
                foreach (GameObject sphere in inactiveSpheres)
                {
                    if (sphere != null)
                    {
                        sphere.SetActive(true);
                    }
                }
            }

            if (sphereIndex == 1)
            {
                hypothesisMovement.isSphereOneTriggered = true; // Set the flag when Sphere 1 is triggered
            }

            if (sphereIndex == 2)
            {
                hypothesisMovement.isSphereTwoTriggered = true; // Set the flag when Sphere 1 is triggered
            }

            // Activate corresponding Hypothesis movement state
            hypothesisMovement.ActivateState(sphereIndex);
            textInteraction.ActivateText(sphereIndex);
        }
    }
}
