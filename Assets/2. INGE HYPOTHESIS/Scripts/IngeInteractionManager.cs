using COSE.Coin;
using COSE.Hypothesis;
using COSE.Sphere;
using System;
using UnityEngine;

public class IngeInteractionManager : MonoBehaviour
{
    [SerializeField] private IngeTextInteraction textInteraction;
    [SerializeField] private HypothesisInteraction hypothesisInteraction;
    [SerializeField] private GameObject[] bookModelCitation;
    [SerializeField] private GameObject[] ingameTxt;
    [SerializeField] private GameObject[] citations;
    [SerializeField] private GameObject[] coins;
    [SerializeField] private GameObject hypothesis1Model;
    [SerializeField] private GameObject hypothesis2Icons;
    [SerializeField] private GameObject[] hypothesis2Animations;
    [SerializeField] private GameObject hypothesis3Model;
    [SerializeField] private GameObject hypothesis3Couplings;
    [SerializeField] private GameObject[] hypothesis3Animations;
    [SerializeField] private GameObject[] hypothesis3Numbers;
    [SerializeField] private GameObject hypothesis4Buzzwords;
    [SerializeField] private GameObject[] hypothesis4Animations;
    [SerializeField] private GameObject[] hypothesis6Animations;
    [SerializeField] private GameObject[] hypothesis6Figures;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked += ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered += ActivateSphereObjects;
        CoinTrigger.OnCoinTriggered += ActivateCoinObjects;
        HypothesisInteraction.OnHypothesisMovementComplete += ActivateHypothesisObjects;
    }

    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked -= ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered -= ActivateSphereObjects;
        CoinTrigger.OnCoinTriggered -= ActivateCoinObjects;
        HypothesisInteraction.OnHypothesisMovementComplete -= ActivateHypothesisObjects;
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
                hypothesisInteraction.MoveModel(hypothesis1Model);
                break;
            case "INGE_SPHERE_HYPOTHESIS_3_LOC_ID":
                hypothesisInteraction.CurrentStateIndex = 3;
                hypothesis2Icons.SetActive(false);
                hypothesisInteraction.MoveModel(hypothesis1Model);
                break;
            case "INGE_SPHERE_HYPOTHESIS_4_LOC_ID":
                hypothesisInteraction.CurrentStateIndex = 4;
                hypothesisInteraction.MoveModel(hypothesis3Model);
                break;
            case "INGE_SPHERE_11_LOC_ID":
                hypothesis6Animations[0].SetActive(true);
                break;
            case "INGE_SPHERE_12_LOC_ID":
                hypothesis6Animations[1].SetActive(true);
                break;
            case "INGE_SPHERE_13_LOC_ID":
                hypothesis6Animations[2].SetActive(true);
                break;
            case "INGE_SPHERE_15_LOC_ID":
                hypothesis6Figures[0].SetActive(true);
                break;
            case "INGE_SPHERE_HYPOTHESIS_7_LOC_ID":
                hypothesis1Model.SetActive(true);
                hypothesisInteraction.CurrentStateIndex = 5;
                hypothesisInteraction.MoveModel(hypothesis1Model);
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
                Hypothesis3("INGE_LAYER_COUPLING_9_LOC_ID");
                break;
            case "INGE_LAYER_BACKGROUND_ELEMENTS_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_BACKGROUND_ELEMENTS_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L1_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L2_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L3_LOC_ID");
                break;
            case "INGE_LAYER_SHRINK_MULTIPLY_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_SHRINK_MULTIPLY_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L6_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L6_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L7_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L7_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L8_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L8_LOC_ID");
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L9_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                Hypothesis4("INGE_LAYER_HYPOTHESIS_4_L9_LOC_ID");
                break;
            case "INGE_LAYER_HERO_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_HERO_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_TOOL_1":
                hypothesis6Figures[1].SetActive(true);
                hypothesis6Figures[0].SetActive(false);
                break;
            case "INGE_LAYER_HERO_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_HERO_4_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
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
            case "INGE_COIN_07_LOC_ID":
                hypothesis6Animations[3].SetActive(true);
                break;
        }
    }

    private void ActivateHypothesisObjects(int currentIndex)
    {
        switch(currentIndex)
        {
            case 2:
                hypothesis2Icons.SetActive(true);
                break;
            case 3:
                hypothesis1Model.SetActive(false);
                hypothesis3Model.SetActive(true);
                hypothesis3Couplings.SetActive(true);
                break;
            case 4:
                hypothesis4Buzzwords.SetActive(true);
                break;
        }
    }

    private void Hypothesis2(string layerText)
    {
        foreach (GameObject animation in hypothesis2Animations)
        {
            animation.SetActive(false);
        }
        switch (layerText)
        {
            case "INGE_LAYER_ALL_DIRECTIONS_ARROW_LOC_ID":
                hypothesis2Animations[0].SetActive(true);
                break;
            case "INGE_LAYER_ALL_ARROW_IN_LOC_ID":
                hypothesis2Animations[1].SetActive(true);
                break;
            case "INGE_LAYER_LATERAL_ARROW_LOC_ID":
                hypothesis2Animations[2].SetActive(true);
                break;
            case "INGE_LAYER_MAGNIFYING_GLASS_LOC_ID":
                hypothesis2Animations[3].SetActive(true);
                break;
            case "INGE_LAYER_MARKER_LOC_ID":
                hypothesis2Animations[4].SetActive(true);
                break;
            case "INGE_LAYER_MEGAPHONE_LOC_ID":
                hypothesis2Animations[5].SetActive(true);
                break;
            case "INGE_LAYER_PENCIL_LOC_ID":
                hypothesis2Animations[6].SetActive(true);
                break;
            case "INGE_LAYER_UP_DOWN_ARROW_LOC_ID":
                hypothesis2Animations[7].SetActive(true);
                break;
            case "INGE_LAYER_DOUBLE_CLICK_ARROW_LOC_ID":
                hypothesis2Animations[8].SetActive(true);
                break;
            case "INGE_LAYER_RESIZE_LOC_ID":
                hypothesis2Animations[9].SetActive(true);
                break;
        }
    }

    private void Hypothesis3(string layerText)
    {
        foreach (GameObject couplingNumber in hypothesis3Numbers)
        {
            couplingNumber.SetActive(false);
        }
        foreach (GameObject couplingAnimation in hypothesis3Animations)
        {
            couplingAnimation.SetActive(false);
        }
        switch(layerText)
        {
            case "INGE_LAYER_COUPLING_1_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Animations[0].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_2_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Animations[1].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_3_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Animations[2].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_4_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Numbers[10].SetActive(true);
                hypothesis3Numbers[12].SetActive(true);
                hypothesis3Numbers[15].SetActive(true);
                hypothesis3Animations[3].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_5_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Animations[4].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_6_LOC_ID":
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Numbers[14].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Animations[5].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_7_LOC_ID":
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Animations[6].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_8_LOC_ID":
                hypothesis3Numbers[3].SetActive(true);
                hypothesis3Numbers[12].SetActive(true);
                hypothesis3Numbers[14].SetActive(true);
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Animations[7].SetActive(true);
                break;
            case "INGE_LAYER_COUPLING_9_LOC_ID":
                foreach (GameObject couplingNumber in hypothesis3Numbers)
                {
                    couplingNumber.SetActive(true);
                }
                hypothesis3Animations[8].SetActive(true);
                break;
        }
    }

    private void Hypothesis4(string layerText)
    {
        foreach (GameObject couplingNumber in hypothesis3Numbers)
        {
            couplingNumber.SetActive(false);
        }
        foreach (GameObject animations in hypothesis4Animations)
        {
            animations.SetActive(false);
        }
        switch (layerText)
        {
            case "INGE_LAYER_BACKGROUND_ELEMENTS_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Numbers[15].SetActive(true);
                hypothesis4Animations[0].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L1_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Numbers[16].SetActive(true);
                hypothesis4Animations[1].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L2_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[2].SetActive(true);
                hypothesis4Animations[2].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L3_LOC_ID":
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[9].SetActive(true);
                hypothesis4Animations[3].SetActive(true);
                break;
            case "INGE_LAYER_SHRINK_MULTIPLY_LOC_ID":
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Numbers[10].SetActive(true);
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Numbers[8].SetActive(true);
                hypothesis4Animations[4].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L6_LOC_ID":
                hypothesis3Numbers[3].SetActive(true);
                hypothesis3Numbers[17].SetActive(true);
                hypothesis4Animations[5].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L7_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[2].SetActive(true);
                hypothesis4Animations[6].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L8_LOC_ID":
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Numbers[9].SetActive(true);
                hypothesis4Animations[7].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L9_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Numbers[10].SetActive(true);
                hypothesis3Numbers[15].SetActive(true);
                hypothesis4Animations[8].SetActive(true);
                break;
        }
    }
}
