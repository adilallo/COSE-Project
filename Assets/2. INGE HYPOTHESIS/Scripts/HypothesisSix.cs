using UnityEngine;
using COSE.Sphere;
using System.Collections;

namespace COSE.Hypothesis
{
    public class HypothesisSix : MonoBehaviour
    {
        [SerializeField] private GameObject heroModel1;
        [SerializeField] private GameObject heroModel2;
        [SerializeField] private GameObject heroModel3;
        [SerializeField] private GameObject heroModel4;

        private void OnEnable()
        {
            SphereInteraction.SphereTriggered += OnSphereTriggered;
        }

        private void OnDisable()
        {
            SphereInteraction.SphereTriggered -= OnSphereTriggered;
        }

        private void OnSphereTriggered(int sphereIndex)
        {
            if (sphereIndex == 6)
            {
                StartCoroutine(ActivateHeroCoroutine(30f));
            }
        }

        private IEnumerator ActivateHeroCoroutine(float delay)
        {
            yield return new WaitForSeconds(delay);

            heroModel1.SetActive(true);
            heroModel2.SetActive(true);
            heroModel3.SetActive(true);
            heroModel4.SetActive(true);
        }
    }
}
