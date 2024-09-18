using COSE.Coin;
using COSE.Sphere;
using UnityEngine;

public class YannickInteractionManager : MonoBehaviour
{
    [SerializeField] private YannickTextInteraction textInteraction;
    [SerializeField] private GameObject[] invisibleCollider;
    [SerializeField] private GameObject[] textCitations;
    [SerializeField] private GameObject modelZip;
    [SerializeField] private GameObject[] portals;
    [SerializeField] private GameObject[] table;
    [SerializeField] private GameObject[] westphal;
    [SerializeField] private GameObject[] coins;
    [SerializeField] private GameObject codeFragmentsText;
    [SerializeField] private GameObject metadataLibraiesText;
    [SerializeField] private GameObject joanSystemText;
    [SerializeField] private GameObject[] bitmapHeader;
    [SerializeField] private GameObject[] spheres;
    [SerializeField] private GameObject code3D;
    [SerializeField] private GameObject[] lids;
    [SerializeField] private GameObject cssObjects;
    [SerializeField] private GameObject[] bookModels;
    [SerializeField] private GameObject ghostlyAnimation;

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
            case "YANNICK_LAYER_INVISIBLE_LAYER_1_LOC_ID":
                invisibleCollider[0]?.SetActive(false);
                invisibleCollider[1]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y07_LOC_ID":
                portals[0]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y09_LOC_ID":
                code3D.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y10_LOC_ID":
                spheres[3]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y11_LOC_ID":
                westphal[5]?.SetActive(true);
                westphal[6]?.SetActive(true);
                coins[3]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y19_LOC_ID":
                portals[1]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y21_LOC_ID":
                portals[3]?.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y22_LOC_ID":
                cssObjects.SetActive(true);
                break;
            case "YANNICK_SPHERE_Y23_LOC_ID":
                westphal[9]?.SetActive(true);
                spheres[5]?.SetActive(true);
                coins[4]?.SetActive(true);
                bookModels[2].SetActive(true);
                break;
            case "YANNICK_SPHERE_Y24_LOC_ID":
                ghostlyAnimation.SetActive(true);
                break;
        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        switch(textKey)
        {
            case "YANNICK_LAYER_BOOK_CITATION_1":
                textCitations[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_WESTPHAL_0_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                modelZip.SetActive(true);
                break;
            case "YANNICK_LAYER_MODEL_ZIP_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_WESTPHAL_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_TOOL_0_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNIC_LAYER_TABLE_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                table[0]?.SetActive(false);
                break;
            case "YANNICK_LAYER_TOOL_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[0]?.SetActive(true);
                coins[0]?.SetActive(true);
                coins[1]?.SetActive(true);
                coins[2]?.SetActive(true);
                break;
            case "YANNICK_LAYER_WESTPHAL_2":
                
                
                break;
            case "YANNICK_LAYER_TOOL_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                bitmapHeader[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_BITMAP_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[2]?.SetActive(true);
                bitmapHeader[1]?.SetActive(true);
                spheres[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_TOOL_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[3]?.SetActive(true);
                spheres[1]?.SetActive(true);
                break;
            case "YANNICK_LAYER_BOX_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                lids[0]?.SetActive(false);
                break;
            case "YANNICK_LAYER_BOX_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                lids[1]?.SetActive(false);
                break;
            case "YANNICK_LAYER_BOX_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                lids[2]?.SetActive(false);
                break;
            case "YANNICK_LAYER_BOX_4_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                lids[3]?.SetActive(false);
                break;
            case "YANNICK_LAYER_TOOL_4_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[4]?.SetActive(true);
                spheres[2]?.SetActive(true);
                break;
            case "YANNICK_LAYER_TRAFFIC_LIGHT_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_TRAFFIC_LIGHT_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                portals[2]?.SetActive(true);
                break;
            case "YANNICK_LAYER_TRAFFIC_LIGHT_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_TOOL_5_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[7]?.SetActive(true);
                break;
            case "YANNICK_LAYER_WESTPHAL_4":
                westphal[8]?.SetActive(true);
                break;
            case "YANNICK_LAYER_TOOL_6_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                spheres[4]?.SetActive(true);
                bookModels[0].SetActive(true);
                break;
            case "YANNICK_LAYER_CARD_MANUAL_4":
                bookModels[1].SetActive(true);
                break;
            case "YANNICK_LAYER_ANIMATION_GHOSTLY_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_BOOK_CITATION_2":
                textCitations[1]?.SetActive(true);
                break;
            case "YANNICK_LAYER_DETECTIVE_HAT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_NEAT_TEXT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
            case "YANNICK_LAYER_LEAKY_TEXT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                break;
        }
    }

    private void ActivateCoinObjects(string coinText)
    {
        switch(coinText)
        {
            case "YANNICK_COIN_Y5_LOC_ID":
                codeFragmentsText.SetActive(true);
                metadataLibraiesText.SetActive(true);
                table[1]?.SetActive(true);
                table[2]?.SetActive(true);
                break;
            case "YANNICK_COIN_Y6_LOC_ID":
                joanSystemText.SetActive(true);
                table[3]?.SetActive(true);
                table[4]?.SetActive(true);
                westphal[1]?.SetActive(true);
                break;
        }
    }
}
