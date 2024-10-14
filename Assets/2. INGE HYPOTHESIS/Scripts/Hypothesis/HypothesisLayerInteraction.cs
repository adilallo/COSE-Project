using System;
using UnityEngine;

[System.Serializable]
public class HypothesisLayerInteraction: MonoBehaviour
{
    public string textKey;
    [HideInInspector] public Vector3 initialLocalPosition;
    [HideInInspector] public Quaternion initialLocalRotation;

    private Outline layerOutlineScript;

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
}