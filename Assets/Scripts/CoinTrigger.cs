using System;
using UnityEngine;

namespace COSE.Coin
{
    public class CoinTrigger : MonoBehaviour
    {
        [SerializeField] private string coinText;

        public static event Action<string> OnCoinTriggered;

        private GameObject coinObject;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnCoinTriggered?.Invoke(coinText);
                Debug.Log("Coin Triggered: " + coinText);
                coinObject = this.gameObject;
                coinObject.SetActive(false);
            }
        }
    }
}
