using UnityEngine;

namespace COSE.Hypothesis
{
    public class LayerClickEvent : MonoBehaviour
    {
        public delegate void LayerClickedHandler(string layerName);
        public static event LayerClickedHandler OnLayerClicked;

        public delegate void LayerClickedByIndexHandler(int layerIndex);
        public static event LayerClickedByIndexHandler OnLayerClickedByIndex;

        [SerializeField] private int layerIndex;

        void OnMouseDown()
        {
            // Invoke the event, sending this layer's name when the mouse clicks this object
            Debug.Log($"Clicked on {gameObject.name}");
            OnLayerClicked?.Invoke(gameObject.name);

            Debug.Log($"Clicked on {gameObject.name} with index {layerIndex}");
            OnLayerClickedByIndex?.Invoke(layerIndex);
        }
    }
}
