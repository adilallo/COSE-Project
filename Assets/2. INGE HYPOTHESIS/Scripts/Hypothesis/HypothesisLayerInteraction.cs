using System;
using UnityEngine;

[System.Serializable]
public class HypothesisLayerInteraction: MonoBehaviour
{
    public string textKey;
    public int layerIndex;
    [HideInInspector] public Vector3 initialLocalPosition;
    [HideInInspector] public Quaternion initialLocalRotation;

    public static event Action<string> OnLayerMoved;
    public static event Action<string> OnLayerClicked;

    public void Initialize()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    private void OnMouseDown()
    {
        NotifyLayerClicked();
    }

    public static void NotifyLayerMoved(string textKey)
    {
        OnLayerMoved?.Invoke(textKey);
    }

    public void NotifyLayerClicked()
    {
        OnLayerClicked?.Invoke(textKey);
    }
}