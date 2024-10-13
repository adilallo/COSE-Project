using UnityEngine;
using System;
using System.Collections.Generic;
using COSE.Hypothesis;

namespace COSE.Diagram
{
    public class DiagramManager : MonoBehaviour
    {
        public static DiagramManager Instance { get; private set; }

        [SerializeField] private List<DiagramElement> diagramElements = new List<DiagramElement>();

        public event Action<DiagramElement> OnDiagramElementClicked;

        public void DiagramElementClicked(DiagramElement element)
        {
            OnDiagramElementClicked?.Invoke(element);
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DeactivateAllElements();
        }

        private void OnEnable()
        {
           // HypothesisInteraction.OnLastLayerFinished += ActivateDiagramElements;
        }

        private void OnDisable()
        {
           // HypothesisInteraction.OnLastLayerFinished -= ActivateDiagramElements;
        }

        public void RegisterElement(DiagramElement element)
        {
            if (!diagramElements.Contains(element))
            {
                diagramElements.Add(element);
                element.gameObject.SetActive(false);
            }
        }

        public void UnregisterElement(DiagramElement element)
        {
            if (diagramElements.Contains(element))
            {
                diagramElements.Remove(element);
            }
        }

        private void ActivateDiagramElements(bool isLastLayerFinished)
        {
            if (isLastLayerFinished)
            {
                foreach (var element in diagramElements)
                {
                    element.gameObject.SetActive(true);
                }
            }
        }

        private void DeactivateAllElements()
        {
            foreach (var element in diagramElements)
            {
                element.gameObject.SetActive(false);
            }
        }
    }
}
