using COSE.Coin;
using UnityEngine;

public class JiawenExtraInteractionManager : MonoBehaviour
{
    [SerializeField] private GameObject annotatedScreen;
    [SerializeField] private GameObject yellowRoomActive;
    private void OnEnable()
    {
        CoinTrigger.OnCoinTriggered += ActivateCoinObjects;
    }

    private void OnDisable()
    {
        CoinTrigger.OnCoinTriggered -= ActivateCoinObjects;
    }

    private void ActivateCoinObjects(string coinText)
    {
        switch(coinText)
        {
            case "JIAWEN_COIN_10_LOC_ID":
                annotatedScreen.SetActive(true);
                break;
            case "JIAWEN_COIN_11_LOC_ID":
                yellowRoomActive.SetActive(true);
                break;
        }
        
    }
}
