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
    public static event Action<string> OnTextLayerClicked;
    public static event Action<int> OnLayerClickedByIndex;
    private static HypothesisLayerInteraction currentOutlined;

    private Outline outlineScript;

    private void Start()
    {
        outlineScript = GetComponent<Outline>();
        if (outlineScript)
        {
            outlineScript.enabled = false;
        }
    }

    public void Initialize()
    {
        initialLocalPosition = transform.localPosition;
        initialLocalRotation = transform.localRotation;
    }

    private void OnMouseDown()
    {
        NotifyLayerClicked();
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

    public static void NotifyLayerMoved(string textKey)
    {
        OnLayerMoved?.Invoke(textKey);
    }

    public static void NotifyTextLayer(string textKey)
    {
        OnTextLayerClicked?.Invoke(textKey);
    }

    public void NotifyLayerClicked()
    {
        OnLayerClicked?.Invoke(textKey);
    }
}