using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Video;

namespace COSE.Hypothesis
{
    public class HypothesisClickManager : MonoBehaviour
    {
        [SerializeField] private HypothesisInteraction hypothesisInteraction;
        [SerializeField] private GameObject[] icons;
        [SerializeField] private VideoPlayer videoPlayer;

        private Dictionary<string, List<int>> layerToIconMap;
        private Dictionary<string, List<int>> nameToIndexMap;

        private Coroutine timeoutCoroutine;

        private void OnEnable()
        {
            HypothesisLayerInteraction.OnLayerClicked += HandleLayerClicked;
            AnimationClickEvent.OnAnimationClicked += PlayVideoClip;
        }

        private void OnDisable()
        {
            HypothesisLayerInteraction.OnLayerClicked -= HandleLayerClicked;
            AnimationClickEvent.OnAnimationClicked -= PlayVideoClip;
        }

        void Start()
        {
            InitializeLayerToIconMap();
            InitializeLayerToTabFieldMap();
            ResetAllIcons();
        }

        void Update()
        {
            if (hypothesisInteraction.currentStateIndex == 2 && hypothesisInteraction.isMovementComplete)
            {
                ActivateAllIcons();
            }
        }

        private void HandleLayerClicked(string textKey)
        {
            Debug.Log("Layer clicked: " + textKey);
            RestartTimeout();

            if (hypothesisInteraction.currentStateIndex >= 1 && hypothesisInteraction.isLastLayerFinished && hypothesisInteraction.currentStateIndex != 8)
            {
                HypothesisLayerInteraction.NotifyTextLayer(textKey);
            }

            if (hypothesisInteraction.currentStateIndex == 2 && hypothesisInteraction.isMovementComplete)
            {
                ResetAllIcons();
                ActivateIconsForLayer(textKey);             
            }

            if (hypothesisInteraction.currentStateIndex == 4)
            {
                ActivateLayersForKeywords(textKey);
            }

            if (hypothesisInteraction.currentStateIndex == 8)
            {
                ActivateHypothesis8Screenshots(textKey);
            }

            if (hypothesisInteraction.currentStateIndex == 9)
            {
                ActivateHypothesis9Animations(textKey);
            }
        }

        private void ResetAllOutlines()
        {
            foreach (var layerInteraction in hypothesisInteraction.mainHypothesisLayers)
            {
                var outline = layerInteraction.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
        }

        private void ActivateAllIcons()
        {
            foreach (GameObject icon in icons)
            {
                icon.SetActive(true);
            }
        }
        private void ActivateIconsForLayer(string textKey)
        {
            if (layerToIconMap.TryGetValue(textKey, out List<int> iconIndices))
            {
                foreach (int index in iconIndices)
                {
                    if (index >= 0 && index < icons.Length)
                    {
                        Outline iconOutline = icons[index].GetComponent<Outline>();
                        if (iconOutline != null)
                        {
                            iconOutline.enabled = true;
                        }
                    }
                }
            }
        }

        private void ActivateLayersForKeywords(string textKey)
        {
            if (nameToIndexMap.TryGetValue(textKey, out List<int> indices))
            {
                hypothesisInteraction.ResetAllLayers();

                foreach (int index in indices)
                {
                    if (index >= 0 && index < hypothesisInteraction.mainHypothesisLayers.Count)
                    {
                        hypothesisInteraction.mainHypothesisLayers[index].gameObject.SetActive(true);
                    }
                }
            }
        }
        
        private void ActivateHypothesis8Screenshots(string textKey)
        {
            if (textKey == "Hypothesis_8_Opaque")
            {
                hypothesisInteraction.hypothesis8Screenshot2.SetActive(false);
                hypothesisInteraction.hypothesis8Screenshot1.SetActive(true);
                Debug.Log("Clicked on Hypothesis_8_Opaque");
            }
            else if (textKey == "Hypothesis_8_Transparent")
            {
                hypothesisInteraction.hypothesis8Screenshot1.SetActive(false);
                hypothesisInteraction.hypothesis8Screenshot2.SetActive(true);
                Debug.Log("Clicked on Hypothesis_8_Transparent");
            }
        }

        private void ActivateHypothesis9Animations(string textKey)
        {
            if (textKey == "INGE_LAYER_HYPOTHESIS_9_L1_LOC_ID")
            {
                StartContinuousRotation(hypothesisInteraction.hypothesis9Scheme1, Vector3.up, 30);
                StartContinuousRotation(hypothesisInteraction.hypothesis9Scheme2, Vector3.up, 30);
                hypothesisInteraction.hypothesis9Scheme2.SetActive(false);
                StartCoroutine(ActivateAfterDelay(hypothesisInteraction.hypothesis9Scheme2, 15f));
            }
        }

        private void StartContinuousRotation(GameObject gameObjectToRotate, Vector3 rotationAxis, float speed)
        {
            StartCoroutine(ContinuousRotationCoroutine(gameObjectToRotate, rotationAxis, speed));
        }

        private IEnumerator ContinuousRotationCoroutine(GameObject gameObjectToRotate, Vector3 rotationAxis, float speed)
        {
            while (true)
            {
                gameObjectToRotate.transform.Rotate(rotationAxis * speed * Time.deltaTime, Space.World);
                yield return null;
            }
        }

        private IEnumerator ActivateAfterDelay(GameObject scheme, float delay)
        {
            yield return new WaitForSeconds(delay);
            scheme.SetActive(true);
        }

        private void PlayVideoClip(VideoClip clip)
        {
            if (videoPlayer != null)
            {
                Debug.Log("video clip triggered");
                videoPlayer.clip = clip;
                videoPlayer.Play();
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

        private void ResetAllIcons()
        {
            foreach (GameObject icon in icons)
            {
                Outline iconOutline = icon.GetComponent<Outline>();
                if (iconOutline != null) iconOutline.enabled = false;
            }
        }

        private void InitializeLayerToIconMap()
        {
            layerToIconMap = new Dictionary<string, List<int>>
            {
                { "INGE_LAYER_HYPOTHESIS_1_L17_LOC_ID", new List<int> { 9 } }, // Icon up/down arrow index 9
                { "INGE_LAYER_HYPOTHESIS_1_L13_LOC_ID", new List<int> { 3 } }, // Right/left arrow index 3
                { "INGE_LAYER_HYPOTHESIS_1_L15_LOC_ID", new List<int> { 3 } }, // Right/left arrow index 3
                { "INGE_LAYER_HYPOTHESIS_1_L3_LOC_ID", new List<int> { 3 } }, // Right/left arrow index 3
                { "INGE_LAYER_HYPOTHESIS_1_L1_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L8_LOC_ID", new List<int> { 0, 8 } }, // All directionís arrow icon index 0, Pencil with frame, spacebar, etc. index 8
                { "INGE_LAYER_HYPOTHESIS_1_L12_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L14_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L6_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L9_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L5_LOC_ID", new List<int> { 0 } }, // All directionís arrow icon index 0
                { "INGE_LAYER_HYPOTHESIS_1_L7_LOC_ID", new List<int> { 6, 7 } }, // Pencil with frame index 6, highlighter index 7, and Space Bar
                { "INGE_LAYER_HYPOTHESIS_1_L10_LOC_ID", new List<int> { 6, 7 } }, // Pencil with frame index 6, highlighter index 7, and Space Bar
            };
        }

        private void InitializeLayerToTabFieldMap()
        {
            nameToIndexMap = new Dictionary<string, List<int>>()
            {
                {"INGE_LAYER_HYPOTHESIS_4_L1_LOC_ID", new List<int>{1, 3, 5, 6, 7, 8, 9, 11, 14, 17}},
                {"INGE_LAYER_HYPOTHESIS_4_L2_LOC_ID", new List<int>{7, 10}},
                {"INGE_LAYER_HYPOTHESIS_4_L3_LOC_ID", new List<int>{8, 3}},
                {"INGE_LAYER_HYPOTHESIS_4_L4_LOC_ID", new List<int>{5, 9}}, // Assuming shrink maps to layers 8 => 5 and 12 => 9
                {"INGE_LAYER_HYPOTHESIS_4_L5_LOC_ID", new List<int>{5, 9}},
                {"INGE_LAYER_HYPOTHESIS_4_L6_LOC_ID", new List<int>{4, 18}},
                {"INGE_LAYER_HYPOTHESIS_4_L8_LOC_ID", new List<int>{3, 7, 8, 10}},
                {"INGE_LAYER_HYPOTHESIS_4_L7_LOC_ID", new List<int>{7, 3}},
                {"INGE_LAYER_HYPOTHESIS_4_L9_LOC_ID", new List<int>{3, 7, 8, 10}}
            };
        }        
    }
}
