using System;
using UnityEngine;

namespace COSE.Hypothesis
{
    public class LayerClickEvent : MonoBehaviour
    {
        public static event Action<string> OnLayerClicked;
        public static event Action<int> OnLayerClickedByIndex;

        [SerializeField] private int layerIndex;
        [SerializeField] private string layerKey;
        private Outline outlineScript;

        private static LayerClickEvent currentOutlined;

        private void Start()
        {
            outlineScript = GetComponent<Outline>();
            if (outlineScript)
            {
                outlineScript.enabled = false;
            }
        }

        private void OnMouseDown()
        {
            Debug.Log($"Clicked on {layerKey}");
            OnLayerClicked?.Invoke(layerKey);
            OnLayerClickedByIndex?.Invoke(layerIndex);

            if (currentOutlined != this)
            {
                if (currentOutlined != null && currentOutlined.outlineScript != null)
                {
                    currentOutlined.outlineScript.enabled = false;
                }

                outlineScript.enabled = true;
                currentOutlined = this;
            }
        }
    }
}
