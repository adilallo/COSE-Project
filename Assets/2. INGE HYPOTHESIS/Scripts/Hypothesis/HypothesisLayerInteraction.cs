using System;
using UnityEngine;

[System.Serializable]
public class HypothesisLayerInteraction: LayerInteraction
{
    public int layerIndex;
    [HideInInspector] public Vector3 initialLocalPosition;
    [HideInInspector] public Quaternion initialLocalRotation;

    public static event Action<string> OnLayerMoved;
    //public static event Action<string> OnTextLayerClicked;
    //public static event Action<int> OnLayerClickedByIndex;

    public void Initialize()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

   /* protected override void OnMouseDown()
    {
        base.OnMouseDown();;
        OnLayerClickedByIndex?.Invoke(layerIndex);
    }*/

    public static void NotifyLayerMoved(string textKey)
    {
        OnLayerMoved?.Invoke(textKey);
    }

   /* public static void NotifyTextLayer(string textKey)
    {
        OnTextLayerClicked?.Invoke(textKey);
    }*/
}