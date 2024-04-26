using System;
using UnityEngine;

[System.Serializable]
public class LayerInteraction: MonoBehaviour
{
    public string textKey;

    public static event Action<string> OnLayerClicked;
    private static LayerInteraction currentOutlined;
    private Outline outlineScript;

    private void OnEnable()
    {
        GroupLayerInteraction.OnLayerGroupClicked += GroupClicked;
    }

    private void OnDisable()
    {
        GroupLayerInteraction.OnLayerGroupClicked -= GroupClicked;
    }

    private void Start()
    {
        outlineScript = GetComponent<Outline>();
        if (outlineScript)
        {
            outlineScript.enabled = false;
        }
    }

    protected virtual void OnMouseDown()
    {
        NotifyLayerClicked();

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

    protected virtual void NotifyLayerClicked()
    {
        OnLayerClicked?.Invoke(textKey);
    }

    private void GroupClicked(string textKey)
    {
        if (currentOutlined != null && currentOutlined.outlineScript != null)
        {
            currentOutlined.outlineScript.enabled = false;
        }
    }
}