using UnityEngine;
using System.Collections;
using TMPro;
using COSE.Diagram;
using System.Collections.Generic;
using System.Linq;
using COSE.Hypothesis;
using System;

namespace COSE.Text
{
    public class TextInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] textObjects;
        [SerializeField] private GameObject[] firstHypothesisTextObjects;
        [SerializeField] private GameObject[] conclusionTextObjects;
        [SerializeField] private GameObject couplingTextObject;
        [SerializeField] private GameObject[] fourthHypothesisTextObjects;

        private void OnEnable()
        {
            LayerClickEvent.OnLayerClickedByIndex += ActivateHypothesisFourTextByIndex;

            if (DiagramManager.Instance != null)
            {
                DiagramManager.Instance.OnDiagramElementClicked += HandleDiagramElementClicked;
            }
        }

        private void OnDisable()
        {
            LayerClickEvent.OnLayerClickedByIndex -= ActivateHypothesisFourTextByIndex;

            if (DiagramManager.Instance != null)
            {
                DiagramManager.Instance.OnDiagramElementClicked -= HandleDiagramElementClicked;
            }
        }

        public void ActivateText(int sphereIndex)
        {
            if (sphereIndex >= 0 && sphereIndex < textObjects.Length)
            {
                DeactivateAllTexts();

                // Activate the specific text object related to the sphereIndex
                if (textObjects[sphereIndex] != null)
                {
                    textObjects[sphereIndex].SetActive(true);

                    // If the sphere index is 2, change the text after a delay.
                    if (sphereIndex == 2)
                    {
                        StartCoroutine(ChangeTextAfterDelay(textObjects[sphereIndex],
                            "But some elements have bonds, they act as a cluster. By interacting with them, we can test the linkages and affordances of the elements.",
                            20f));
                    }

                    if (sphereIndex == 6)
                    {
                        StartCoroutine(ChangeTextAfterDelay(textObjects[sphereIndex],
                            "In the .com browser, one website is depicted within a tab field complex. As the tab field complex is a mix of buttons, texts and colour fields, its configuration has much depth. This stretched-out three-dimensionality is an important factor as soon as further tab field complexes enter the scene.",
                            10f));
                    }
                }
            }
        }

        public void ActivateHypothesisText(int firstHypothesisIndex)
        {
            if (firstHypothesisIndex >= 0 && firstHypothesisIndex < firstHypothesisTextObjects.Length)
            {
                DeactivateAllTexts();

                // Activate the specific text object related to the sphereIndex
                if (firstHypothesisTextObjects[firstHypothesisIndex] != null)
                {
                    firstHypothesisTextObjects[firstHypothesisIndex].SetActive(true);
                }
            }
        }

        public void ActivateConclusionText(int conclusionIndex)
        {
            // Deactivate all text objects in all lists
            DeactivateAllTexts();

            // Activate the specific text object related to the conclusion index
            if (conclusionTextObjects[conclusionIndex] != null)
            {
                conclusionTextObjects[conclusionIndex].SetActive(true);
            }
        }

        public void ActivateCouplingText(List<int> coupling, bool isTightlyCoupled)
        {
            DeactivateAllTexts();

            // Assume there's a designated text object for coupling information
                couplingTextObject.SetActive(true);
                TMP_Text textMeshPro = couplingTextObject.GetComponentInChildren<TMP_Text>();
                if (textMeshPro != null)
                {
                    string couplingType = isTightlyCoupled ? "Tightly Coupled: " : "Loosely Coupled: ";
                    string couplingIds = string.Join(", ", coupling);
                    textMeshPro.text = $"{couplingType}{couplingIds}";
                }
                else
                {
                    Debug.LogWarning("TMP_Text component not found on the coupling text object!");
                }
        }

        public void DeactivateAllTexts()
        {
            foreach (GameObject textObj in textObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }

            foreach (GameObject textObj in firstHypothesisTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }

            foreach (GameObject textObj in conclusionTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }

            couplingTextObject.SetActive(false);

            foreach (GameObject textObj in fourthHypothesisTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }
        }

        private IEnumerator ChangeTextAfterDelay(GameObject textObject, string newText, float delay)
        {
            yield return new WaitForSeconds(delay);

            // Change the text of the TextMeshPro component
            TMP_Text textMeshPro = textObject.GetComponentInChildren<TMP_Text>(); // Using GetComponentInChildren in case the TMP component is not directly on the parent object
            if (textMeshPro != null)
            {
                textMeshPro.text = newText;
            }
            else
            {
                Debug.LogWarning("TMP_Text component not found on the text object!");
            }
        }

        private void HandleDiagramElementClicked(DiagramElement element)
        {
            Debug.Log($"Diagram element clicked: {element.name}");
            DeactivateAllTexts();
        }

        private void ActivateHypothesisFourTextByIndex(int index)
        {
            if (index >= 0 && index < fourthHypothesisTextObjects.Length)
            {
                DeactivateAllTexts();
                fourthHypothesisTextObjects[index].SetActive(true);
            }
        }
    }
}
