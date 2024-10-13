using COSE.Coin;
using COSE.Hypothesis;
using COSE.Sphere;
using COSE.Text;
using UnityEngine;

public class IngeInteractionManager : MonoBehaviour
{
    [SerializeField] private IngeTextInteraction textInteraction;
    [SerializeField] private HypothesisInteraction hypothesisInteraction;
    [SerializeField] private GameObject citation;


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
            case "INGE_LAYER_CITATION_1_LOC_ID":
                citation.SetActive(true);
                break;
        }
    }

    private void ActivateCoinObjects(string coinId)
    {
        switch(coinId)
        {

        }
    }
}
