using UnityEngine;
using System.Collections;
using TMPro;

namespace COSE.Text
{
    public class TextInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] textObjects;
        [SerializeField] private GameObject[] firstHypothesisTextObjects;
        [SerializeField] private GameObject[] conclusionTextObjects;

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

        private void DeactivateAllTexts()
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
    }
}
