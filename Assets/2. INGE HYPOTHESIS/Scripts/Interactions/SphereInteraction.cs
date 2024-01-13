using UnityEngine;

namespace COSE.Interactions
{
    public class SphereInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] inactiveSpheres;
        [SerializeField] private HypothesisInteraction hypothesisMovement;

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

            // Activate corresponding Hypothesis movement state
            hypothesisMovement.ActivateState(sphereIndex);
        }
    }
}
