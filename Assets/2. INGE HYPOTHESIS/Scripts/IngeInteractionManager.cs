using COSE.Coin;
using COSE.Sphere;
using COSE.Text;
using UnityEngine;

public class IngeInteractionManager : MonoBehaviour
{
    [SerializeField] private IngeTextInteraction textInteraction;
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

        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        switch(textKey)
        {
            case "INGE_LAYER_ENTRY_IMAGE_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
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
