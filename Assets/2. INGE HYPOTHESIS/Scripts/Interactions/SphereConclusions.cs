using UnityEngine;
using System.Collections.Generic;

namespace COSE.Interactions
{
    public class SphereConclusions : MonoBehaviour
    {
        [SerializeField] private List<GameObject> conclusionSpheres; // List of spheres to activate
        [SerializeField] private HypothesisInteraction hypothesisInteraction; // Reference to the HypothesisInteraction script
        [SerializeField] private TextInteraction textInteraction;

        public void HandleConclusionSphereTriggered(int index)
        {
                textInteraction.ActivateConclusionText(index);
        }

        // Method to call when the last layer finishes moving
        public void OnLastLayerFinished()
        {
            // Activate the first sphere in the list
            if (conclusionSpheres.Count > 0 && conclusionSpheres[0] != null)
            {
                conclusionSpheres[1].SetActive(true);
            }
        }

        void Update()
        {
            // Check if the last layer has finished its coroutine
            if (hypothesisInteraction != null && hypothesisInteraction.IsLastLayerFinished)
            {
                OnLastLayerFinished();
            }
        }
    }
}
