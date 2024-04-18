using UnityEngine;
using TMPro;
using COSE.Diagram;
using System.Collections.Generic;
using System;
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
            HypothesisLayerInteraction.OnLayerMoved += ActivateText;
            DiagramManager.Instance.OnDiagramElementClicked += HandleDiagramElementClicked;
            HypothesisInteraction.OnCouplingActivated += ActivateCouplingText;
            LayerClickEvent.OnLayerClicked += ActivateText;
        }

        private void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= ActivateText;
            HypothesisLayerInteraction.OnLayerMoved -= ActivateText;
            DiagramManager.Instance.OnDiagramElementClicked -= HandleDiagramElementClicked;
            HypothesisInteraction.OnCouplingActivated -= ActivateCouplingText;
            LayerClickEvent.OnLayerClicked -= ActivateText;
        }

        void Start()
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
            DeactivateAllTexts();

            // Activate the specific text object related to the conclusion index
            if (conclusionTextObjects[conclusionIndex] != null)
            {
                conclusionTextObjects[conclusionIndex].SetActive(true);
            }
        }

        public void ActivateCouplingText(string couplingType)
        {
            this._text.text = couplingType;
        }

        public void DeactivateAllTexts()
        {
            _textUI.SetActive(false);
            _text.text = default;
           /* 

            foreach (GameObject textObj in conclusionTextObjects)
            {
                if (textObj != null) textObj.SetActive(false);
            }*/
        }

        private void HandleDiagramElementClicked(DiagramElement element)
        {
            Debug.Log($"Diagram element clicked: {element.name}");
            DeactivateAllTexts();
        }
    }
}
