using UnityEngine;

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
                // Deactivate all text objects
                foreach (GameObject textObj in textObjects)
                {
                    if (textObj != null)
                    {
                        textObj.SetActive(false);
                    }
                }

                // Activate the specific text object related to the sphereIndex
                if (textObjects[sphereIndex] != null)
                {
                    textObjects[sphereIndex].SetActive(true);
                }
            }
        }

        public void ActivateHypothesisText(int firstHypothesisIndex)
        {
            if (firstHypothesisIndex >= 0 && firstHypothesisIndex < firstHypothesisTextObjects.Length)
            {
                // Deactivate all text objects
                foreach (GameObject textObj in textObjects)
                {
                    if (textObj != null)
                    {
                        textObj.SetActive(false);
                    }
                }

                foreach (GameObject textObj in firstHypothesisTextObjects)
                {
                    if (textObj != null)
                    {
                        textObj.SetActive(false);
                    }
                }

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
    }
}
