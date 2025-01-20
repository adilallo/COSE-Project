using COSE.Coin;
using COSE.Sphere;
using System.Linq;
using UnityEngine;
public class OutroInteractionManager : MonoBehaviour
{
    [SerializeField] private OutroTextInteraction textInteraction;
    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject backPortal;
    [SerializeField] private GameObject exitPortal;
    [SerializeField] private GameObject replayPortal;
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
                if (!UI.activeSelf)
                {
                    UI.SetActive(true);
                }
                finalBoards.SetActive(true);
                foreach (var wall in greyWalls)
                {
                    wall.SetActive(false);
                }
                foreach (var wall in finalWalls)
                {
                    wall.SetActive(true);
                }
                backPortal.SetActive(true);
                exitPortal.SetActive(true);
                replayPortal.SetActive(true);
                break;
            case "OUTRO_COIN_2_LOC_ID":
                if (PersistenceManager.Instance.GetTotalCoinsCollected() == PersistenceManager.Instance.GetTotalCoinsAvailable())
                {
                    animatedObjects.SetActive(true);
                    UI.SetActive(false);
                    textInteraction.DeactivateAllTexts();
                }
                else
                {
                    textInteraction.ActivateLayerText(coinId);
                }
                
                break;
        }
    }
}