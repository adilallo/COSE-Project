using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public Renderer targetRenderer;

    void Start()
    {
        // Disable the targetRenderer at start
        targetRenderer.enabled = false;
    }

    public void ReactivateObject()
    {
        targetRenderer.enabled = true;
    }

    public void DeactivateObject()
    {
        targetRenderer.enabled = false;
    }
}
