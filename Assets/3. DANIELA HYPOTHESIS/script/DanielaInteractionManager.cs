using COSE.Sphere;
using UnityEngine;

public class DanielaInteractionManager : MonoBehaviour
{
    [SerializeField] private DanielaTextInteraction textInteraction;
    [SerializeField] private GameObject portalJiawen1;
    [SerializeField] private GameObject portalJiawen2;
    [SerializeField] private GameObject portalInge1;
    [SerializeField] private GameObject portalInge2;
    [SerializeField] private GameObject portalInge3;
    [SerializeField] private GameObject bookModelForCitation;
    [SerializeField] private GameObject citationDaniela;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked += ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered += ActivateSphereObjects;
    }

    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked -= ActivateLayerObjects;
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
                case "DANIELA_LAYER_DEAD_END_1_IMAGE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalJiawen2.SetActive(true);
                    portalInge1.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_2_MIDDLE_BAR_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalInge2.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_2_WRONG_INTERFACE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_2_IMAGE_1_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_2_IMAGE_2_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalInge3.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_3_HAND_PUZZLE_SELECTION_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_3_IMAGE_LOC_ID":
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
