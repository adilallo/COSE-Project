using System;
using UnityEngine;

[System.Serializable]
public class HypothesisLayerInteraction: MonoBehaviour
{
    public string textKey;
    [HideInInspector] public Vector3 initialLocalPosition;
    [HideInInspector] public Quaternion initialLocalRotation;

    private Outline layerOutlineScript;

    //public static event Action<string> OnLayerMoved;
    //public static event Action<string> OnTextLayerClicked;
    //public static event Action<int> OnLayerClickedByIndex;

    public void Initialize()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;

        layerOutlineScript = GetComponent<Outline>();
        if (layerOutlineScript)
        {
            layerOutlineScript.enabled = false;
        }
    }

    public void OutlineLayer(bool enable)
    {
        if (layerOutlineScript)
        {
            layerOutlineScript.enabled = enable;
        }
    }

   /* protected override void OnMouseDown()
    {
        base.OnMouseDown();;
        OnLayerClickedByIndex?.Invoke(layerIndex);
    }*/

   /* public static void NotifyLayerMoved(string textKey)
    {
        OnLayerMoved?.Invoke(textKey);
    }
 public static void NotifyTextLayer(string textKey)
    {
        OnTextLayerClicked?.Invoke(textKey);
    }*/
}