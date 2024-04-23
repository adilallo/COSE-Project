using COSE.Sphere;
using COSE.Text;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    [SerializeField] private DanielaTextInteraction textInteraction;
    [SerializeField] private GameObject portalJiawen1;
    [SerializeField] private GameObject bookModelForCitation;
    [SerializeField] private GameObject citationDaniela;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered += ActivateSphereObjects;
    }

    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered -= ActivateSphereObjects;
    }

    private void ActivateSphereObjects(string sphereId)
    {
        if (sphereId == "DANIELA_SPHERE_READ_ME_LOC_ID")
        {
            if (portalJiawen1 != null)
                portalJiawen1.SetActive(true);

            if (bookModelForCitation != null)
                bookModelForCitation.SetActive(true);
        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        if (textKey != null)
        {
            Debug.Log("Received text key: " + textKey);

            switch (textKey)
            {
                case "CITATION_DANIELA":
                    citationDaniela.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_1_CLOUD_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                default:
                    Debug.Log("No specific action for this key: " + textKey);
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Received null text key.");
        }
    }
}
