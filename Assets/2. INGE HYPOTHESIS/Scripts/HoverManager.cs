using UnityEngine;
using System.Collections.Generic;

namespace COSE.Hypothesis
{
    public class HoverManager : MonoBehaviour
    {
        [SerializeField] private HypothesisInteraction hypothesisInteraction;
        [SerializeField] private GameObject[] icons; // Array of icons to be activated

        private Dictionary<string, List<int>> layerToIconMap;

        void Start()
        {
            // Initialize the mapping between layer names and icon indices
            layerToIconMap = new Dictionary<string, List<int>>
            {
                { "Layer_17_SCROLL_BAR", new List<int> { 9 } }, // Icon up/down arrow index 9
                { "Layer_13_middle_bar_URL_ball", new List<int> { 3 } }, // Right/left arrow index 3
                { "Layer_15_middle_bar_formatted_text_ball", new List<int> { 3 } }, // Right/left arrow index 3
                { "Layer_3_FORMATED_TEXT", new List<int> { 3 } }, // Right/left arrow index 3
                { "Layer_1_HTML_COLOR_FIELD", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_8_URL_text_field", new List<int> { 0, 8 } }, // All direction’s arrow icon index 0, Pencil with frame, spacebar, etc. index 8
                { "Layer_12_URL_SQUARE", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_14_PENTAGON_BALL", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_6_PENTAGON_LINE", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_9_HISTORY_SQUARE", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_5_HISTORY_URL", new List<int> { 0 } }, // All direction’s arrow icon index 0
                { "Layer_7_HTML_TEXT_FIELD", new List<int> { 6, 7 } }, // Pencil with frame index 6, highlighter index 7, and Space Bar
                { "Layer_10_MIDDLE_BAR_URL_TEXT", new List<int> { 6, 7 } }, // Pencil with frame index 6, highlighter index 7, and Space Bar
            };
        }

        void Update()
        {
                CheckHoverInteraction();
        }

        private void CheckHoverInteraction()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hoverDetected = false;

            foreach (var layerInteraction in hypothesisInteraction.firstHypothesisText)
            {
                Outline outlineScript = layerInteraction.layerObject.GetComponent<Outline>(); // Assuming Outline is the script name
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == layerInteraction.layerObject)
                {
                    // Hover detected
                    hoverDetected = true;

                    // Handle outline
                    if (outlineScript != null)
                        outlineScript.enabled = true;

                    // If Sphere Two is active, handle icons
                    if (hypothesisInteraction.isSphereTwoTriggered && hypothesisInteraction.isMovementComplete)
                    {
                        ActivateIconsForLayer(layerInteraction.layerObject.name);
                    }

                    // If Sphere One is active, animations are done, and sphere 2 isn't triggered, handle text
                    if (hypothesisInteraction.isSphereOneTriggered && hypothesisInteraction.IsLastLayerFinished && !hypothesisInteraction.isSphereTwoTriggered)
                    {
                        hypothesisInteraction.NotifyTextInteraction(layerInteraction.textIndex);
                    }

                    break;
                }
                else if (outlineScript != null)
                {
                    outlineScript.enabled = false;
                }
            }

            // If no hover detected, reset outlines and potentially icons
            if (!hoverDetected)
            {
                ResetAllOutlines();
                if (hypothesisInteraction.isSphereTwoTriggered)
                {
                    ResetAllIcons();
                }
            }
        }

        private void ActivateIconsForLayer(string layerName)
        {
            // Activate corresponding icons
            if (layerToIconMap.TryGetValue(layerName, out List<int> iconIndices))
            {
                foreach (int index in iconIndices)
                {
                    if (index >= 0 && index < icons.Length)
                        icons[index].SetActive(true);
                }
            }
        }

        private void ResetAllOutlines()
        {
            foreach (var layerInteraction in hypothesisInteraction.firstHypothesisText)
            {
                Outline outlineScript = layerInteraction.layerObject.GetComponent<Outline>();
                if (outlineScript != null) outlineScript.enabled = false;
            }
        }

        private void ResetAllIcons()
        {
            foreach (var icon in icons)
            {
                icon.SetActive(false);
            }
        }
    }
}
