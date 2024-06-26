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
                codeFragmentsText.SetActive(true);
                metadataLibraiesText.SetActive(true);
                joanSystemText.SetActive(true);
                break;
        }
    }

    private void ActivateCoinObjects(string coinText)
    {
        switch(coinText)
        {
            case "YANNICK_COIN_Y5_LOC_ID":
                table[1]?.SetActive(true);
                table[2]?.SetActive(true);
                break;
            case "YANNICK_COIN_Y6_LOC_ID":
                table[3]?.SetActive(true);
                table[4]?.SetActive(true);
                westphal[1]?.SetActive(true);
                break;
        }
    }
}
