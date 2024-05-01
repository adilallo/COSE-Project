using UnityEngine;

public class GroupLayerInteraction : MonoBehaviour
{
    [SerializeField] private GameObject[] layers;
    [SerializeField] private string textKeys;
    private static GroupLayerInteraction currentOutlined;
    private Outline outlines;
    private Collider groupCollider;

    public static event System.Action<string> OnLayerGroupClicked;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += LayerClicked;
    }
    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= LayerClicked;
    }

    private void Start()
    {
        groupCollider = GetComponent<Collider>();

        foreach (GameObject layer in layers)
        {
            outlines = layer.GetComponent<Outline>();
            if (outlines)
            {
                outlines.enabled = false;
            }
        }
    }

    private void OnMouseDown()
    {
        NotifyLayerGroupClicked();
        if (currentOutlined != this)
        {
            if (currentOutlined != null && currentOutlined.outlines != null)
            {
                foreach (GameObject layer in currentOutlined.layers)
                {
                    outlines = layer.GetComponent<Outline>();
                    if (outlines)
                    {
                        outlines.enabled = false;
                    }
                }
            }

            foreach (GameObject layer in layers)
            {
                outlines = layer.GetComponent<Outline>();
                if (outlines)
                {
                    outlines.enabled = true;
                }
            }

            currentOutlined = this;
        }
    }

    private void NotifyLayerGroupClicked()
    {
        OnLayerGroupClicked?.Invoke(textKeys);
    }

    private void LayerClicked(string textKey)
    {
        currentOutlined = null;

        foreach (GameObject layer in layers)
        {
            outlines = layer.GetComponent<Outline>();
            if (outlines)
            {
                outlines.enabled = false;
            }
        }
    }
}