using UnityEngine;

namespace COSE.Interactions
{
    public class SphereInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] inactiveSpheres;
        [SerializeField] private HypothesisInteraction hypothesisMovement;

        public void OnSphere1Triggered()
        {
            foreach (GameObject sphere in inactiveSpheres)
            {
                if (sphere != null)
                {
                    sphere.SetActive(true);
                }
            }

            // Activate Hypothesis 1 movement
            hypothesisMovement.ActivateMovement1();
        }
    }
}
