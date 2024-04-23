using UnityEngine;
using COSE.Diagram;
using COSE.Hypothesis;

namespace COSE.Text
{
    public class IngeTextInteraction : TextInteraction
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            HypothesisLayerInteraction.OnLayerMoved += ActivateText;
            HypothesisLayerInteraction.OnTextLayerClicked += ActivateText;
            DiagramManager.Instance.OnDiagramElementClicked += HandleDiagramElementClicked;
            HypothesisInteraction.OnCouplingActivated += ActivateCouplingText;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            HypothesisLayerInteraction.OnLayerMoved -= ActivateText;
            HypothesisLayerInteraction.OnTextLayerClicked -= ActivateText;
            DiagramManager.Instance.OnDiagramElementClicked -= HandleDiagramElementClicked;
            HypothesisInteraction.OnCouplingActivated -= ActivateCouplingText;
        }

        public void ActivateCouplingText(string couplingType)
        {
            this._text.text = couplingType;
        }

        private void HandleDiagramElementClicked(DiagramElement element)
        {
            Debug.Log($"Diagram element clicked: {element.name}");
            DeactivateAllTexts();
        }
    }
}
