using COSE.Coin;
using COSE.Hypothesis;
using COSE.Sphere;
using COSE.Text;
using UnityEngine;

public class IngeInteractionManager : MonoBehaviour
{
    [SerializeField] private IngeTextInteraction textInteraction;
    [SerializeField] private HypothesisInteraction hypothesisInteraction;
    [SerializeField] private GameObject[] bookModelCitation;
    [SerializeField] private GameObject[] ingameTxt;
    [SerializeField] private GameObject[] citations;
    [SerializeField] private GameObject[] coins;
    [SerializeField] private GameObject icons;
    [SerializeField] private GameObject[] animations;
    [SerializeField] private GameObject hypothesis1Model;
    [SerializeField] private GameObject hypothesis2Model;
    [SerializeField] private GameObject hypothesis3Model;
    [SerializeField] private GameObject hypothesis3Couplings;
    [SerializeField] private GameObject[] couplingAnimations;
    [SerializeField] private GameObject[] couplingNumbers;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked += ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered += ActivateSphereObjects;
        CoinTrigger.OnCoinTriggered += ActivateCoinObjects;
    }

    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked -= ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered -= ActivateSphereObjects;
        CoinTrigger.OnCoinTriggered -= ActivateCoinObjects;
    }

    private void ActivateSphereObjects(string sphereId)
    {
        switch(sphereId)
        {
            case "INGE_SPHERE_HYPOTHESIS_1_LOC_ID":
                hypothesisInteraction.CurrentStateIndex = 1;
                break;
            case "INGE_SPHERE_HYPOTHESIS_2_LOC_ID":
                hypothesisInteraction.CurrentStateIndex = 2;
                icons.SetActive(true);
                break;
            case "INGE_SPHERE_HYPOTHESIS_3_LOC_ID":
                hypothesis1Model.SetActive(false);
                hypothesis2Model.SetActive(false);
                hypothesis3Model.SetActive(true);
                hypothesis3Couplings.SetActive(true);  
                break;
        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        switch(textKey)
        {
            case "INGE_LAYER_ENTRY_IMAGE_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_ENTRY_IMAGE_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_ENTRY_IMAGE_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_CITATION_0":
                citations[0].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCREENSHOT_RIGHT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bookModelCitation[0].SetActive(true);
                foreach (GameObject ingameTxt in ingameTxt)
                {
                    ingameTxt.SetActive(false);
                }
                ingameTxt[0].SetActive(true);
                ingameTxt[5].SetActive(true);
                break;
            case "INGE_LAYER_CITATION_1":
                citations[1].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCREENSHOT_MIDDLE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bookModelCitation[1].SetActive(true);
                foreach (GameObject ingameTxt in ingameTxt)
                {
                    ingameTxt.SetActive(false);
                }
                ingameTxt[0].SetActive(true);
                ingameTxt[1].SetActive(true);
                ingameTxt[3].SetActive(true);
                break;
            case "INGE_LAYER_CITATION_2":
                citations[2].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCREENSHOT_LEFT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bookModelCitation[2].SetActive(true);
                foreach (GameObject ingameTxt in ingameTxt)
                {
                    ingameTxt.SetActive(false);
                }
                ingameTxt[1].SetActive(true);
                ingameTxt[2].SetActive(true);
                ingameTxt[4].SetActive(true);
                ingameTxt[6].SetActive(true);
                break;
            case "INGE_LAYER_CITATION_3":
                citations[3].SetActive(true);
                coins[0].SetActive(true);
                break;
            case "INGE_LAYER_ALL_DIRECTIONS_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_ALL_DIRECTIONS_ARROW_LOC_ID");
                break;
            case "INGE_LAYER_ALL_ARROW_IN_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_ALL_ARROW_IN_LOC_ID");
                break;
            case "INGE_LAYER_LATERAL_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_LATERAL_ARROW_LOC_ID");
                break;
            case "INGE_LAYER_MAGNIFYING_GLASS_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_MAGNIFYING_GLASS_LOC_ID");
                break;
            case "INGE_LAYER_MARKER_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_MARKER_LOC_ID");
                break;
            case "INGE_LAYER_MEGAPHONE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_MEGAPHONE_LOC_ID");
                break;
            case "INGE_LAYER_PENCIL_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_PENCIL_LOC_ID");
                break;
            case "INGE_LAYER_UP_DOWN_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_UP_DOWN_ARROW_LOC_ID");
                break;
            case "INGE_LAYER_DOUBLE_CLICK_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_DOUBLE_CLICK_ARROW_LOC_ID");
                break;
            case "INGE_LAYER_RESIZE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis2("INGE_LAYER_RESIZE_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_1_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_2_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_3_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_4_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_3_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_5_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_5_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_6_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_6_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_7_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_7_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_8_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis3("INGE_LAYER_COUPLING_8_LOC_ID");
                break;
            case "INGE_LAYER_COUPLING_9_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject couplingNumber in couplingNumbers)
                {
                    couplingNumber.SetActive(true);
                }
                break;
        }
    }

    private void ActivateCoinObjects(string coinId)
    {
        switch(coinId)
        {
            case "INGE_COIN_02_LOC_ID":
                coins[1].SetActive(true);
                break;
        }
    }

    private void Hypothesis2(string layerText)
    {
        foreach (GameObject animation in animations)
        {
            animation.SetActive(false);
        }
        switch (layerText)
        {
            case "INGE_LAYER_ALL_DIRECTIONS_ARROW_LOC_ID":
                animations[0].SetActive(true);
                break;
            case "INGE_LAYER_ALL_ARROW_IN_LOC_ID":
                animations[1].SetActive(true);
                break;
            case "INGE_LAYER_LATERAL_ARROW_LOC_ID":
                animations[2].SetActive(true);
                break;
            case "INGE_LAYER_MAGNIFYING_GLASS_LOC_ID":
                animations[3].SetActive(true);
                break;
            case "INGE_LAYER_MARKER_LOC_ID":
                animations[4].SetActive(true);
                break;
            case "INGE_LAYER_MEGAPHONE_LOC_ID":
                animations[5].SetActive(true);
                break;
            case "INGE_LAYER_PENCIL_LOC_ID":
                animations[6].SetActive(true);
                break;
            case "INGE_LAYER_UP_DOWN_ARROW_LOC_ID":
                animations[7].SetActive(true);
                break;
            case "INGE_LAYER_DOUBLE_CLICK_ARROW_LOC_ID":
                animations[8].SetActive(true);
                break;
            case "INGE_LAYER_RESIZE_LOC_ID":
                animations[9].SetActive(true);
                break;
        }
    }

    private void Hypothesis3(string layerText)
    {
        foreach (GameObject couplingNumber in couplingNumbers)
        {
            couplingNumber.SetActive(false);
        }
        foreach (GameObject couplingAnimation in couplingAnimations)
        {
            couplingAnimation.SetActive(false);
        }
        switch(layerText)
        {
            case "INGE_LAYER_COUPLING_1_LOC_ID":
                couplingNumbers[0].SetActive(true);
                couplingNumbers[4].SetActive(true);
                couplingNumbers[5].SetActive(true);
                couplingNumbers[6].SetActive(true);
                couplingNumbers[7].SetActive(true);
                couplingNumbers[8].SetActive(true);
                couplingNumbers[11].SetActive(true);
                couplingNumbers[13].SetActive(true);
                couplingNumbers[16].SetActive(true);
                couplingAnimations[0].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_2_LOC_ID":
                couplingNumbers[0].SetActive(true);
                couplingNumbers[4].SetActive(true);
                couplingNumbers[5].SetActive(true);
                couplingNumbers[6].SetActive(true);
                couplingNumbers[7].SetActive(true);
                couplingNumbers[8].SetActive(true);
                couplingNumbers[11].SetActive(true);
                couplingNumbers[13].SetActive(true);
                couplingNumbers[16].SetActive(true);
                couplingAnimations[1].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_3_LOC_ID":
                couplingNumbers[6].SetActive(true);
                couplingNumbers[16].SetActive(true);
                couplingAnimations[2].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_4_LOC_ID":
                couplingNumbers[1].SetActive(true);
                couplingNumbers[9].SetActive(true);
                couplingNumbers[10].SetActive(true);
                couplingNumbers[12].SetActive(true);
                couplingNumbers[15].SetActive(true);
                couplingAnimations[3].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_5_LOC_ID":
                couplingNumbers[1].SetActive(true);
                couplingNumbers[2].SetActive(true);
                couplingAnimations[4].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_6_LOC_ID":
                couplingNumbers[2].SetActive(true);
                couplingNumbers[14].SetActive(true);
                couplingNumbers[4].SetActive(true);
                couplingNumbers[8].SetActive(true);
                couplingNumbers[9].SetActive(true);
                couplingNumbers[13].SetActive(true);
                couplingAnimations[5].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_7_LOC_ID":
                couplingNumbers[5].SetActive(true);
                couplingNumbers[13].SetActive(true);
                couplingAnimations[6].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_8_LOC_ID":
                couplingNumbers[3].SetActive(true);
                couplingNumbers[12].SetActive(true);
                couplingNumbers[14].SetActive(true);
                couplingNumbers[0].SetActive(true);
                couplingNumbers[6].SetActive(true);
                couplingAnimations[7].SetActive(true);
                break;
        }
    }
}
