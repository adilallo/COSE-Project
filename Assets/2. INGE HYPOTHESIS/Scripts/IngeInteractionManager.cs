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
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[0].SetActive(true);
                break;
            case "INGE_LAYER_ALL_ARROW_IN_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[1].SetActive(true);
                break;
            case "INGE_LAYER_LATERAL_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[2].SetActive(true);
                break;
            case "INGE_LAYER_MAGNIFYING_GLASS_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[3].SetActive(true);
                break;
            case "INGE_LAYER_MARKER_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[4].SetActive(true);
                break;
            case "INGE_LAYER_MEGAPHONE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[5].SetActive(true);
                break;
            case "INGE_LAYER_PENCIL_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[6].SetActive(true);
                break;
            case "INGE_LAYER_UP_DOWN_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[7].SetActive(true);
                break;
            case "INGE_LAYER_DOUBLE_CLICK_ARROW_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[8].SetActive(true);
                break;
            case "INGE_LAYER_RESIZE_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                foreach (GameObject animation in animations)
                {
                    animation.SetActive(false);
                }
                animations[9].SetActive(true);
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
}
