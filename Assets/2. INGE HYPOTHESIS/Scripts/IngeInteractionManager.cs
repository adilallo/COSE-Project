using COSE.Coin;
using COSE.Hypothesis;
using COSE.Sphere;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

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
    [SerializeField] private GameObject hypothesis2barrier;
    [SerializeField] private GameObject[] hypothesis2Animations;
    [SerializeField] private GameObject hypothesis3barrier;
    [SerializeField] private GameObject hypothesis3Model;
    [SerializeField] private GameObject hypothesis3Couplings;
    [SerializeField] private GameObject[] hypothesis3Animations;
    [SerializeField] private GameObject[] hypothesis3Numbers;
    [SerializeField] private Outline[] hypothesis3Rectangles;
    [SerializeField] private Outline[] hypothesis3Layers;
    [SerializeField] private GameObject hypothesis4Buzzwords;
    [SerializeField] private GameObject[] hypothesis4Animations;
    [SerializeField] private GameObject[] hypothesis6Animations;
    [SerializeField] private GameObject[] hypothesis6Figures;
    [SerializeField] private GameObject hypothesis7Model;
    [SerializeField] private GameObject hypothesis7RedModel;
    [SerializeField] private GameObject[] colorSchemes;
    [SerializeField] private GameObject[] diagramHighlights;
    [SerializeField] private GameObject[] hypothesis8Screenshots;

    private Dictionary<string, int> diagramHighlightIndices = new Dictionary<string, int>
    {
        { "INGE_LAYER_DIAGRAM_SCROLLBAR",       0 },
        { "INGE_LAYER_DIAGRAM_FORMATTED_TEXT",  1 },
        { "INGE_LAYER_DIAGRAM_PENTAGON_BALL",   2 },
        { "INGE_LAYER_DIAGRAM_HISTORY_URL",     3 },
        { "INGE_LAYER_DIAGRAM_WHAT_IS_THIS",    4 },
        { "INGE_LAYER_DIAGRAM_COLOR_CHANGE",    5 },
        { "INGE_LAYER_GREY_FIELD",              6 },
    };

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
                foreach (GameObject couplingNumber in hypothesis3Numbers)
                {
                    couplingNumber.SetActive(true);
                }
                foreach (Outline couplingRect in hypothesis3Rectangles)
                {
                    couplingRect.enabled = false;
                }
                foreach (Outline couplingLayer in hypothesis3Layers)
                {
                    couplingLayer.enabled = false;
                }
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
                hypothesis7Model.SetActive(true);
                hypothesis7RedModel.SetActive(true);
                hypothesisInteraction.CurrentStateIndex = 5;
                hypothesisInteraction.MoveModel(hypothesis7Model);
                break;
            case "INGE_SPHERE_HYPOTHESIS_9_LOC_ID":
                colorSchemes[0].SetActive(true);
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
                ToggleGameObject(ingameTxt[0]);
                ToggleGameObject(ingameTxt[5]);
                break;
            case "INGE_LAYER_CITATION_1":
                citations[1].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCREENSHOT_MIDDLE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bookModelCitation[1].SetActive(true);
                ToggleGameObject(ingameTxt[0]);
                ToggleGameObject(ingameTxt[1]);
                ToggleGameObject(ingameTxt[3]);
                break;
            case "INGE_LAYER_CITATION_2":
                citations[2].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCREENSHOT_LEFT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bookModelCitation[2].SetActive(true);
                ToggleGameObject(ingameTxt[1]);
                ToggleGameObject(ingameTxt[2]);
                ToggleGameObject(ingameTxt[4]);
                ToggleGameObject(ingameTxt[6]);
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
                bool isFirstActive = hypothesis6Figures[0].activeSelf;
                hypothesis6Figures[0].SetActive(!isFirstActive);
                hypothesis6Figures[1].SetActive(isFirstActive);
                break;
            case "INGE_LAYER_HERO_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_HERO_4_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "INGE_LAYER_COLOR_SCHEME_1":
                colorSchemes[0].SetActive(false);
                colorSchemes[1].SetActive(true);
                break;
            case "INGE_LAYER_COLOR_SCHEME_2":
                colorSchemes[1].SetActive(false);
                colorSchemes[0].SetActive(true);
                break;
            case "INGE_LAYER_DIAGRAM_SCROLLBAR":
            case "INGE_LAYER_DIAGRAM_FORMATTED_TEXT":
            case "INGE_LAYER_DIAGRAM_PENTAGON_BALL":
            case "INGE_LAYER_DIAGRAM_HISTORY_URL":
            case "INGE_LAYER_DIAGRAM_WHAT_IS_THIS":
            case "INGE_LAYER_DIAGRAM_COLOR_CHANGE":
            case "INGE_LAYER_GREY_FIELD":
                ToggleDiagramHighlight(textKey);
                break;
            
            case "Hypothesis_8_Opaque":
                hypothesis8Screenshots[0].SetActive(true);
                hypothesis8Screenshots[1].SetActive(true);
                break;
            case "Hypothesis_8_Transparent":
                hypothesis8Screenshots[2].SetActive(true);
                hypothesis8Screenshots[3].SetActive(true);
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
            case "INGE_COIN_10_LOC_ID":
                hypothesisInteraction.MoveMultipleModels();
                break;
            case "INGE_COIN_13_LOC_ID":
                hypothesisInteraction.CurrentStateIndex = 6;
                hypothesisInteraction.MoveModel(hypothesis7Model);
                hypothesisInteraction.MoveModel(hypothesis7RedModel);
                break;
        }
    }

    private void ActivateHypothesisObjects(int currentIndex)
    {
        switch(currentIndex)
        {
            case 2:
                hypothesis2Icons.SetActive(true);
                hypothesis2barrier.SetActive(false);
                break;
            case 3:
                hypothesis1Model.SetActive(false);
                hypothesis3Model.SetActive(true);
                hypothesis3Couplings.SetActive(true);
                hypothesis3barrier.SetActive(false);
                break;
            case 4:
                hypothesis4Buzzwords.SetActive(true);
                break;
        }
    }

    private void ToggleGameObject(GameObject obj)
    {
        if (obj != null)
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    private void ToggleDiagramHighlight(string diagramLayer)
    {
        if (diagramHighlightIndices.TryGetValue(diagramLayer, out int index) && index >= 0 && index < diagramHighlights.Length)
        {
            ToggleGameObject(diagramHighlights[index]);
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
        foreach (Outline couplingLayer in hypothesis3Layers)
        {
            couplingLayer.enabled = false;
        }
        foreach (Outline couplingRectangle in hypothesis3Rectangles)
        {
            couplingRectangle.enabled = false;
        }
        switch (layerText)
        {
            case "INGE_LAYER_COUPLING_1_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Layers[0].enabled = true;
                hypothesis3Rectangles[0].enabled = true;
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Layers[4].enabled = true;
                hypothesis3Rectangles[4].enabled = true;
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled = true;
                hypothesis3Rectangles[5].enabled = true;
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Rectangles[6].enabled = true;
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Layers[7].enabled = true;
                hypothesis3Rectangles[7].enabled = true;
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Layers[8].enabled = true;
                hypothesis3Rectangles[8].enabled = true;
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Layers[11].enabled = true;
                hypothesis3Rectangles[11].enabled = true;
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Layers[13].enabled = true;
                hypothesis3Rectangles[13].enabled = true;
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Layers[16].enabled = true;
                hypothesis3Rectangles[16].enabled = true;
                hypothesis3Animations[0].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_2_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Layers[0].enabled = true;
                hypothesis3Rectangles[0].enabled = true;
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Layers[4].enabled = true;
                hypothesis3Rectangles[4].enabled = true;
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled = true;
                hypothesis3Rectangles[5].enabled = true;
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Rectangles[6].enabled = true;
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Layers[7].enabled = true;
                hypothesis3Rectangles[7].enabled = true;
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Layers[8].enabled = true;
                hypothesis3Rectangles[8].enabled = true;
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Layers[11].enabled = true;
                hypothesis3Rectangles[11].enabled = true;
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Layers[13].enabled = true;
                hypothesis3Rectangles[13].enabled = true;
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Layers[16].enabled = true;
                hypothesis3Rectangles[16].enabled = true;
                hypothesis3Animations[1].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_3_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Rectangles[6].enabled = true;
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Layers[16].enabled = true;
                hypothesis3Rectangles[16].enabled = true;
                hypothesis3Animations[2].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_4_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Layers[1].enabled = true;
                hypothesis3Rectangles[1].enabled = true;
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Layers[9].enabled = true;
                hypothesis3Rectangles[9].enabled = true;
                hypothesis3Numbers[10].SetActive(true);
                hypothesis3Layers[10].enabled = true;
                hypothesis3Rectangles[10].enabled = true;
                hypothesis3Numbers[12].SetActive(true);
                hypothesis3Layers[12].enabled = true;
                hypothesis3Rectangles[12].enabled = true;
                hypothesis3Numbers[15].SetActive(true);
                hypothesis3Layers[15].enabled = true;
                hypothesis3Rectangles[15].enabled = true;
                hypothesis3Animations[3].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_5_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Layers[1].enabled = true;
                hypothesis3Rectangles[1].enabled = true;
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis3Rectangles[2].enabled = true;
                hypothesis3Animations[4].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_6_LOC_ID":
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis3Rectangles[2].enabled = true;
                hypothesis3Numbers[14].SetActive(true);
                hypothesis3Layers[14].enabled = true;
                hypothesis3Rectangles[14].enabled = true;
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Layers[4].enabled = true;
                hypothesis3Rectangles[4].enabled = true;
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Layers[8].enabled = true;
                hypothesis3Rectangles[8].enabled = true;
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Layers[9].enabled = true;
                hypothesis3Rectangles[9].enabled = true;
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Layers[13].enabled = true;
                hypothesis3Rectangles[13].enabled = true;
                hypothesis3Animations[5].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_7_LOC_ID":
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled = true;
                hypothesis3Rectangles[5].enabled = true;
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Layers[13].enabled = true;
                hypothesis3Rectangles[13].enabled = true;
                hypothesis3Animations[6].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_8_LOC_ID":
                hypothesis3Numbers[3].SetActive(true);
                hypothesis3Layers[3].enabled = true;
                hypothesis3Rectangles[3].enabled = true;
                hypothesis3Numbers[12].SetActive(true);
                hypothesis3Layers[12].enabled = true;
                hypothesis3Rectangles[12].enabled = true;
                hypothesis3Numbers[14].SetActive(true);
                hypothesis3Layers[14].enabled = true;
                hypothesis3Rectangles[14].enabled = true;
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Layers[0].enabled = true;
                hypothesis3Rectangles[0].enabled = true;
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Rectangles[6].enabled = true;
                hypothesis3Animations[7].SetActive(true);
                break;

            case "INGE_LAYER_COUPLING_9_LOC_ID":
                for (int i = 0; i < hypothesis3Numbers.Length; i++)
                {
                    hypothesis3Numbers[i].SetActive(true);
                    hypothesis3Layers[i].enabled = true;
                    hypothesis3Rectangles[i].enabled = true;
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
        foreach (Outline couplingLayer in hypothesis3Layers)
        {
            couplingLayer.enabled = false;
        }
        switch (layerText)
        {
            case "INGE_LAYER_BACKGROUND_ELEMENTS_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Layers[0].enabled=true;
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Layers[1].enabled=true;
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled=true;
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Layers[4].enabled=true;
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled=true;
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Layers[8].enabled=true;
                hypothesis3Numbers[15].SetActive(true);
                hypothesis3Layers[15].enabled=true;
                hypothesis4Animations[0].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L1_LOC_ID":
                hypothesis3Numbers[0].SetActive(true);
                hypothesis3Layers[0].enabled = true;    
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis3Numbers[4].SetActive(true);
                hypothesis3Layers[4].enabled = true;
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled = true;
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Layers[7].enabled = true;
                hypothesis3Numbers[8].SetActive(true);
                hypothesis3Layers[8].enabled = true;
                hypothesis3Numbers[11].SetActive(true);
                hypothesis3Layers[11].enabled = true;
                hypothesis3Numbers[13].SetActive(true);
                hypothesis3Layers[13].enabled = true;
                hypothesis3Numbers[16].SetActive(true);
                hypothesis3Layers[16].enabled = true;
                hypothesis4Animations[1].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L2_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis4Animations[2].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L3_LOC_ID":
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Layers[7].enabled = true;
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Layers[9].enabled = true;
                hypothesis4Animations[3].SetActive(true);
                break;
            case "INGE_LAYER_SHRINK_MULTIPLY_LOC_ID":
                hypothesis3Numbers[5].SetActive(true);
                hypothesis3Layers[5].enabled = true;
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Layers[9].enabled = true;
                hypothesis4Animations[4].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L6_LOC_ID":
                hypothesis3Numbers[3].SetActive(true);
                hypothesis3Layers[3].enabled = true;
                hypothesis3Numbers[17].SetActive(true);
                hypothesis3Layers[17].enabled = true;
                hypothesis4Animations[5].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L7_LOC_ID":
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis4Animations[6].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L8_LOC_ID":
                hypothesis3Numbers[2].SetActive(true);
                hypothesis3Layers[2].enabled = true;
                hypothesis3Numbers[6].SetActive(true);
                hypothesis3Layers[6].enabled = true;
                hypothesis3Numbers[7].SetActive(true);
                hypothesis3Layers[7].enabled = true;
                hypothesis3Numbers[9].SetActive(true);
                hypothesis3Layers[9].enabled = true;
                hypothesis4Animations[7].SetActive(true);
                break;
            case "INGE_LAYER_HYPOTHESIS_4_L9_LOC_ID":
                hypothesis3Numbers[1].SetActive(true);
                hypothesis3Layers[1].enabled = true;
                hypothesis3Numbers[10].SetActive(true);
                hypothesis3Layers[10].enabled = true;
                hypothesis3Numbers[15].SetActive(true);
                hypothesis3Layers[15].enabled = true;
                hypothesis4Animations[8].SetActive(true);
                break;
        }
    }
}
