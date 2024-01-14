using UnityEngine;

public class TextInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] textObjects;

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
}
