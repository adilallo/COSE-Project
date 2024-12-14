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
    [SerializeField] private GameObject[] portalRedActive;
    [SerializeField] private GameObject portalYellowNonactive;
    [SerializeField] private GameObject portalYellowActive;
    [SerializeField] private GameObject portalGreenActive;
    [SerializeField] private GameObject hyp1Video;
    [SerializeField] private GameObject hyp1Circles;
    [SerializeField] private GameObject timeMatrix;
    [SerializeField] private GameObject userClock;
    [SerializeField] private GameObject[] filters;
    [SerializeField] private GameObject[] screens;
    [SerializeField] private GameObject[] undertitles;
    [SerializeField] private GameObject[] coins;
    [SerializeField] private GameObject slotMachine;
    [SerializeField] private GameObject smallText;

    private bool roadA = false;
    private bool roadB = false;
    private bool roadC = false;

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
                portalRedActive[0]?.SetActive(true);
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
            case "JIAWEN_LAYER_BIG_ROAD_A":
                filters[0]?.SetActive(true);
                textInteraction.DeactivateAllTexts();
                roadA = true;
                RoadsClicked();
                break;
            case "JIAWEN_LAYER_BIG_ROAD_B":
                filters[1]?.SetActive(true);
                undertitles[1]?.SetActive(true);
                textInteraction.DeactivateAllTexts();
                roadB = true;
                RoadsClicked();
                break;
            case "JIAWEN_LAYER_BIG_ROAD_C":
                filters[2]?.SetActive(true);
                undertitles[0]?.SetActive(true);
                textInteraction.DeactivateAllTexts();
                roadC = true;
                RoadsClicked();
                break;
            case "JIAWEN_LAYER_FILTER_LEFT_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[3]?.SetActive(true);
                undertitles[2]?.SetActive(true);
                slotMachine.SetActive(true);
                coins[2]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_MIDDLE_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[0]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_MIDDLE_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                coins[0]?.SetActive(true);
                screens[1]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_MIDDLE_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[2]?.SetActive(true);
                undertitles[1]?.SetActive(true);
                coins[1]?.SetActive(true);
                portalRedActive[2]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_RIGHT_1_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[4]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_RIGHT_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[5]?.SetActive(true);
                break;
            case "JIAWEN_LAYER_FILTER_RIGHT_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                screens[6]?.SetActive(true);
                coins[3]?.SetActive(true);
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
            case "JIAWEN_COIN_6_LOC_ID":
                portalRedActive[1]?.SetActive(true);
                break;
            case "JIAWEN_COIN_6_B1_LOC_ID":
                portalRedActive[1]?.SetActive(true);
                break;
            case "JIAWEN_COIN_8_LOC_ID":
                portalGreenActive.SetActive(true);
                break;
            case "JIAWEN_BIG_COIN_2_LOC_ID":
                smallText.SetActive(true);
                break;
        }
    }

    private void RoadsClicked()
    {         if (roadA && roadB && roadC)
        {
            coins[4]?.SetActive(true);
        }
    }
}