using COSE.Coin;
using COSE.Sphere;
using UnityEngine;

public class YannickExtraInteractionManager : MonoBehaviour
{
    [SerializeField] private YannickTextInteraction textInteraction;
    [SerializeField] private GameObject[] textCitations;
    [SerializeField] private GameObject[] westphal;
    [SerializeField] private GameObject[] cardManuals;
    [SerializeField] private GameObject[] ascii;

    private void OnEnable()
    {
        LayerInteraction.OnLayerClicked += ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked += ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered += ActivateSphereObjects;
    }

    private void OnDisable()
    {
        LayerInteraction.OnLayerClicked -= ActivateLayerObjects;
        GroupLayerInteraction.OnLayerGroupClicked -= ActivateLayerObjects;
        SphereTrigger.OnSphereTriggered -= ActivateSphereObjects;
    }

    private void ActivateSphereObjects(string sphereId)
    {
        switch(sphereId)
        {
            case "YANNICK_SPHERE_Y16_2_LOC_ID":
                ascii[0]?.SetActive(true);
                ascii[1]?.SetActive(true);
                break;
        }
    }

    private void ActivateLayerObjects(string textKey)
    {
        switch (textKey)
        {
            case "YANNICK_LAYER_BOOK_CITATION_2":
                textCitations[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_BOOK_CITATION_3":
                textCitations[1]?.SetActive(true);
                break;
            case "YANNICK_LAYER_WESTPHAL_2_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_WESTPHAL_3_LOC_ID":
                textInteraction.ActivateLayerText(textKey);
                westphal[1]?.SetActive(true);
                break;
            case "YANNICK_LAYER_BOOK_CITATION_4":
                textCitations[2]?.SetActive(true);
                break;
            case "YANNICK_LAYER_CARD_MANUAL_1":
                cardManuals[0]?.SetActive(true);
                break;
            case "YANNICK_LAYER_CARD_MANUAL_2":
                cardManuals[1]?.SetActive(true);
                break;
            case "YANNICK_LAYER_CARD_MANUAL_3":
                cardManuals[2]?.SetActive(true);
                break;
            case "YANNICK_LAYER_BOOK_CITATION_5":
                textCitations[3].SetActive(true);
                break;
            case "YANNICK_LAYER_BOOK_CITATION_6":
                textCitations[4].SetActive(true);
                break;
        }
    }
}
