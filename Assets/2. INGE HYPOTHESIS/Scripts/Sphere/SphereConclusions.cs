using UnityEngine;
using System.Collections.Generic;
using COSE.Hypothesis;
using UnityEngine.UI;
using COSE.Messages;

namespace COSE.Sphere
{
    public class SphereConclusions : MonoBehaviour
    {
        [SerializeField] private List<GameObject> conclusionSpheres; // List of spheres to activate
        [SerializeField] private HypothesisInteraction hypothesisInteraction; // Reference to the HypothesisInteraction script
        [SerializeField] private Button conclusionButton;
        [SerializeField] private MessageButton messageButtonScript;

        void OnEnable()
        {
            HypothesisInteraction.OnLastLayerFinished += HandleLastLayerFinished;
        }

        void OnDisable()
        {
            HypothesisInteraction.OnLastLayerFinished -= HandleLastLayerFinished;
        }

        public void HandleConclusionSphereTriggered(int index)
        {
            Debug.Log($"HandleSphereTriggered called with index: {index}");
            // Instantiate the message item
            messageButtonScript.AddMessage("First Conclusion", index);

            // Optionally activate the conclusion button
            ActivateConclusionButton();
        }

        private void ActivateConclusionButton()
        {
            if (conclusionButton != null)
            {
                conclusionButton.gameObject.SetActive(true);
            }
        }

        // Method to call when the last layer finishes moving
        private void HandleLastLayerFinished()
        {
            // Activate the first sphere in the list
            if (conclusionSpheres.Count > 0 && conclusionSpheres[0] != null)
            {
                conclusionSpheres[1].SetActive(true);
            }
        }
    }
}
