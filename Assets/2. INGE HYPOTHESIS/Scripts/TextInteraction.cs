using UnityEngine;
using TMPro;
using COSE.Diagram;
using System.Collections.Generic;
using COSE.Hypothesis;
using COSE.Sphere;
using UnityEngine.Localization.Settings;

namespace COSE.Text
{
    public class TextInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject[] conclusionTextObjects;
        [SerializeField] private GameObject couplingTextObject;
        [SerializeField] private GameObject[] fourthHypothesisTextObjects;

        [SerializeField] private GameObject _textUI;
        [SerializeField] private TextMeshProUGUI _text;

        private void OnEnable()
        {
            SphereTrigger.OnSphereTriggered += ActivateText;
            LayerInteraction.OnLayerMoved += ActivateText;
            LayerClickEvent.OnLayerClickedByIndex += ActivateHypothesisFourTextByIndex;

            if (DiagramManager.Instance != null)
            {
                DiagramManager.Instance.OnDiagramElementClicked += HandleDiagramElementClicked;
            }
        }

        private void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= ActivateText;
            LayerInteraction.OnLayerMoved -= ActivateText;
            LayerClickEvent.OnLayerClickedByIndex -= ActivateHypothesisFourTextByIndex;

            if (DiagramManager.Instance != null)
            {
                DiagramManager.Instance.OnDiagramElementClicked -= HandleDiagramElementClicked;
            }
        }

        public void Start()
        {
            DeactivateAllTexts();
        }

        private void ActivateText(string text)
        {
            if (_textUI.activeSelf == false)
            {
                _textUI.SetActive(true);
            }

            this._text.text = LocalizationSettings.StringDatabase.GetLocalizedString(text);
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
            _textUI.SetActive(false);
            _text.text = default;
           /* 

            foreach (GameObject textObj in conclusionTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }

            couplingTextObject.SetActive(false);

            foreach (GameObject textObj in fourthHypothesisTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }*/
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
