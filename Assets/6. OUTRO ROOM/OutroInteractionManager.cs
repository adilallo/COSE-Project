using COSE.Coin;
using COSE.Sphere;
using UnityEngine;
public class OutroInteractionManager : MonoBehaviour
{
    [SerializeField] private OutroTextInteraction textInteraction;
    [SerializeField] private GameObject outroCoin2;
    [SerializeField] private GameObject animatedObjects;
    [SerializeField] private GameObject finalBoards;
    [SerializeField] private GameObject[] greyWalls;
    [SerializeField] private GameObject[] finalWalls;

    private void OnEnable()
    {
        CoinTrigger.OnCoinTriggered += ActivateCoinObjects;
    }

    private void OnDisable()
    {
        CoinTrigger.OnCoinTriggered -= ActivateCoinObjects;
    }

    private void ActivateCoinObjects(string coinId)
    {
        switch (coinId)
        {
            case "OUTRO_COIN_1_LOC_ID":
                outroCoin2.SetActive(true);
                break;
            case "OUTRO_COIN_2":
                animatedObjects.SetActive(true);
                finalBoards.SetActive(true);
                foreach (var wall in greyWalls)
                {
                    wall.SetActive(false);
                }
                foreach (var wall in finalWalls)
                {
                    wall.SetActive(true);
                }
                textInteraction.DeactivateAllTexts();
                break;
        }
    }
}