using COSE.Sphere;
using UnityEngine;

public class DanielaInteractionManager : MonoBehaviour
{
    [SerializeField] private DanielaTextInteraction textInteraction;
    [SerializeField] private GameObject[] portalJiawen;
    [SerializeField] private GameObject[] portalInge;
    [SerializeField] private GameObject bookModelForCitation;
    [SerializeField] private GameObject citationDaniela;
    [SerializeField] private GameObject coloredPuzzleBig;
    [SerializeField] private GameObject coloredPuzzleSmall;
    [SerializeField] private GameObject greenScissors;
    [SerializeField] private GameObject warningBlock;
    [SerializeField] private GameObject textBoards;

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
            if (portalJiawen.Length > 0 && portalJiawen[0] != null)
                portalJiawen[0].SetActive(true);

            if (bookModelForCitation != null)
                bookModelForCitation.SetActive(true);
        }
        if (sphereId == "DANIELA_SPHERE_INFO_5_LOC_ID")
        {
            textBoards.SetActive(true);
            textInteraction.DeactivateAllTexts();
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
                case "DANIELA_LAYER_LAPTOP_RIGHT":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_LAPTOP_LEFT":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_1_IMAGE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalJiawen[1]?.SetActive(true);
                    portalInge[0]?.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_2_MIDDLE_BAR_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalInge[1]?.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_2_WRONG_INTERFACE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_2_IMAGE_1_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_DEAD_END_2_IMAGE_2_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalInge[2]?.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_3_HAND_PUZZLE_SELECTION_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalJiawen[2]?.SetActive(true);
                    coloredPuzzleBig.SetActive(true);
                    greenScissors.SetActive(true);
                    break;
                case "DANIELA_LAYER_DEAD_END_3_IMAGE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_3_GREEN_SCISSORS_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    portalJiawen[3]?.SetActive(true);
                    coloredPuzzleSmall.SetActive(true);
                    break;
                case "DANIELA_LAYER_STAGE_4_FUNNEL_MOTAVI_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_4_FUNNEL_GROUP_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_5_BALANCE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_5_MATH_PUZZLE_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_5_ARROW_DIAGRAM_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    break;
                case "DANIELA_LAYER_STAGE_6_WARNING_SIGN_LOC_ID":
                    textInteraction.ActivateLayerText(textKey);
                    warningBlock.SetActive(false);
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
