using UnityEngine;

namespace COSE.Sphere
{
    public class SphereInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] inactiveSpheres;

        private void OnEnable()
        {
            SphereTrigger.OnSphereTriggered += ActivateSphere;
        }

        private void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= ActivateSphere;
        }

        public void ActivateSphere(string text)
        {
            if (text == "INGE_SPHERE_HYPOTHESIS_1_LOC_ID")
            {
                foreach (GameObject sphere in inactiveSpheres)
                {
                    if (sphere != null)
                    {
                        sphere.SetActive(true);
                    }
                }
            }
           
        }
    }
}
