using COSE.Hypothesis;
using COSE.Text;
using UnityEngine;

namespace COSE.Sphere
{
    public class SphereInteraction : MonoBehaviour
    {
        public delegate void SphereTriggeredHandler(int sphereIndex);
        public static event SphereTriggeredHandler SphereTriggered;

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
                hypothesisMovement.isSphereOneTriggered = true;
            }

            if (sphereIndex == 2)
            {
                hypothesisMovement.isSphereTwoTriggered = true;
            }
            if (sphereIndex == 3)
            {
                hypothesisMovement.isSphereThreeTriggered = true;
            }
            if (sphereIndex == 4)
            {
                hypothesisMovement.isSphereFourTriggered = true;
            }
            if (sphereIndex == 5)
            {
                hypothesisMovement.isSphereFiveTriggered = true;
            }
            if (sphereIndex == 6)
            {
                hypothesisMovement.isSphereSixTriggered = true;
            }

            SphereTriggered?.Invoke(sphereIndex);

            // Activate corresponding Hypothesis movement state
            hypothesisMovement.ActivateState(sphereIndex);
            textInteraction.ActivateText(sphereIndex);
        }
    }
}
