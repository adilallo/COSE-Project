using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace COSE.Hypothesis
{
    public class HoverManager : MonoBehaviour
    {
        [SerializeField] private HypothesisInteraction hypothesisInteraction;
        [SerializeField] private GameObject[] icons; // Array of icons to be activated

        private Dictionary<string, List<int>> layerToIconMap;
        private Dictionary<string, List<int>> nameToIndexMap;

        private Coroutine timeoutCoroutine;

        private void OnEnable()
        {
            LayerClickEvent.OnLayerClicked += HandleLayerClicked;
        }

        private void OnDisable()
        {
            LayerClickEvent.OnLayerClicked -= HandleLayerClicked;
        }

        void Start()
        {
            // Initialize the mapping between layer names and icon indices
            InitializeLayerToIconMap();
            InitializeLayerToTabFieldMap();

        }

        void Update()
        {
            CheckHoverInteraction();

            if (hypothesisInteraction.isSphereTwoTriggered && hypothesisInteraction.isMovementComplete)
            {
                ActivateAllIcons();
            }
        }

        private void InitializeLayerToIconMap()
        {
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

        private void InitializeLayerToTabFieldMap()
        {
            nameToIndexMap = new Dictionary<string, List<int>>()
            {
                {"tab field complex", new List<int>{1, 3, 5, 6, 7, 8, 9, 11, 14, 17}},
                {"similarity", new List<int>{7, 10}},
                {"identity", new List<int>{8, 3}},
                {"shrink", new List<int>{5, 9}}, // Assuming shrink maps to layers 8 => 5 and 12 => 9
                {"multiply", new List<int>{5, 9}},
                {"singularities", new List<int>{4, 18}},
                {"temporarily empty", new List<int>{7, 3}},
                {"search performing complex", new List<int>{3, 7, 8, 10}}
            };
        }

        private void CheckHoverInteraction()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            bool hoverDetected = false;

            foreach (var layerInteraction in hypothesisInteraction.firstHypothesisText)
            {
                Outline layerOutlineScript = layerInteraction.layerObject.GetComponent<Outline>(); // Assuming Outline is the script name
                if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject == layerInteraction.layerObject)
                {
                    // Hover detected
                    hoverDetected = true;

                    // Handle outline
                    if (layerOutlineScript != null)
                        layerOutlineScript.enabled = true;

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
                else if (layerOutlineScript != null)
                {
                    layerOutlineScript.enabled = false;
                }
            }

            // If no hover detected, reset outlines and potentially icons
            if (!hoverDetected)
            {
                ResetAllOutlines();
                if (hypothesisInteraction.isSphereTwoTriggered && hypothesisInteraction.isMovementComplete)
                {
                    ResetAllIcons();
                }
            }
        }

        private void ActivateAllIcons()
        {
            // Activate all icons
            foreach (GameObject icon in icons)
            {
                icon.SetActive(true);
            }
        }

        private void ActivateIconsForLayer(string layerName)
        {
            if (layerToIconMap.TryGetValue(layerName, out List<int> iconIndices))
            {
                foreach (int index in iconIndices)
                {
                    if (index >= 0 && index < icons.Length)
                    {
                        Outline iconOutline = icons[index].GetComponent<Outline>();
                        if (iconOutline != null)
                        {
                            iconOutline.enabled = true;
                            RestartTimeout();
                        }
                    }
                }
            }
        }

        private void RestartTimeout()
        {
            if (timeoutCoroutine != null)
            {
                StopCoroutine(timeoutCoroutine);
            }
            timeoutCoroutine = StartCoroutine(ResetAfterTimeout(20f));
        }

        private IEnumerator ResetAfterTimeout(float delay)
        {
            yield return new WaitForSeconds(delay);
            ResetAllOutlines();
            ResetAllIcons();
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
            foreach (GameObject icon in icons)
            {
                Outline iconOutline = icon.GetComponent<Outline>();
                if (iconOutline != null) iconOutline.enabled = false;
            }
        }

        private void HandleLayerClicked(string layerName)
        {
            // First, deactivate all outlines
            hypothesisInteraction.DeactivateAllOutlines();

            if (nameToIndexMap.TryGetValue(layerName, out List<int> indices))
            {
                foreach (int index in indices)
                {
                    Debug.Log($"Found indices for {layerName}: {string.Join(", ", indices)}");
                    if (index >= 0 && index < hypothesisInteraction.firstHypothesisText.Count)
                    {
                        var layer = hypothesisInteraction.firstHypothesisText[index];

                        hypothesisInteraction.ActivateLayerByIndex(index);

                        var outline = layer.layerObject.GetComponent<Outline>() ?? layer.layerObject.GetComponentInChildren<Outline>();
                        if (outline != null)
                        {
                            outline.enabled = true;
                        }
                    }
                }
            }
        }
    }
}
