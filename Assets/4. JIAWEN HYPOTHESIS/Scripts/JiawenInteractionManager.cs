using COSE.Coin;
using COSE.Sphere;
using UnityEngine;

public class JiawenInteractionManager : MonoBehaviour
{
    [SerializeField] private JiawenTextInteraction textInteraction;
    [SerializeField] private GameObject citationJiawen1;
    [SerializeField] private GameObject citationJiawen2;
    [SerializeField] private GameObject signMap;
    [SerializeField] private GameObject[] arrow;
    [SerializeField] private GameObject[] portalGreenNonactive;
    [SerializeField] private GameObject[] portalRedactive;
    [SerializeField] private GameObject portalYellowNonactive;
    [SerializeField] private GameObject portalYellowActive;
    [SerializeField] private GameObject hyp1Video;
    [SerializeField] private GameObject hyp1Circles;
    [SerializeField] private GameObject timeMatrix;
    [SerializeField] private GameObject userClock;

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
            case "JIAWEN_SPHERE_READ_ME_LOC_ID":
                portalGreenNonactive[0]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_ARROW_TURN_OFF_LOC_ID":
                foreach (GameObject arrow in arrow)
                {
                    arrow.SetActive(false);
                }
                textInteraction.DeactivateAllTexts();
                break;
            case "JIAWEN_SPHERE_HYPOTHESIS_2_LOC_ID":
                portalGreenNonactive[1]?.SetActive(true);
                portalYellowNonactive.SetActive(true);
                break;
            case "JIAWEN_SPHERE_HYPOTHESIS_4_LOC_ID":
                portalRedactive[0]?.SetActive(true);
                break;
            case "JIAWEN_SPHERE_HYPOTHESIS_6_LOC_ID":
                portalYellowActive.SetActive(true);
                break;
        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        switch(textKey)
        {
            case "JIAWEN_LAYER_GAUSSIAN_BOARD_LOC_ID":
                citationJiawen1.SetActive(true);
                textInteraction.ActivateLayerText(textKey);
                break;
            case "JIAWEN_LAYER_RANDOM_BOARD":
                citationJiawen2.SetActive(true);
                break;
            case "JIAWEN_LAYER_EXPLANATION_BOARD":
                signMap.SetActive(true);
                break;
            case "JAIWEN_LAYER_MAP_LABEL_1":
                arrow[0]?.SetActive(true);
                break;
            case "JAIWEN_LAYER_MAP_LABEL_2":
                arrow[1]?.SetActive(true);
                break;
            case "JAIWEN_LAYER_MAP_LABEL_3":
                arrow[2]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_WATCH":
                hyp1Video.SetActive(true);
                hyp1Circles.SetActive(true);
                break;
            case "JIAWEN_LAYER_CLOCK":
                userClock.SetActive(true);
                break;
            case "JIAWEN_LAYER_SMALL_FILTERS_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                portalGreenNonactive[2]?.SetActive(true);
                break;
        }
    }

    private void ActivateCoinObjects(string coinText)
    {
        switch(coinText)
        {
            case "JIAWEN_COIN_2_LOC_ID":
                timeMatrix.SetActive(true);
                break;
        }
    }
}