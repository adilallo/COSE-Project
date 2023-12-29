using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalObjectObjectActivator : MonoBehaviour
{
    public Renderer targetRenderer;

    public void ReactivateObject()
    {
        targetRenderer.enabled = true;
    }

    public void DeactivateObject()
    {
        targetRenderer.enabled = false;
    }
}
