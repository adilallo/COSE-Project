using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace COSE.Coin
{
    public class CoinManager : MonoBehaviour
    {
        public static CoinManager Instance { get; private set; }

        private int totalCoinsCollected = 0;
        [SerializeField] private int totalCoinsAvailable = 48;
        private TextMeshProUGUI textMeshPro;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            CoinTrigger.OnCoinTriggered += UpdateCoinCount;
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {
            CoinTrigger.OnCoinTriggered -= UpdateCoinCount;
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Find the TextMeshPro object with the tag "CoinCounterText" in the new scene
            GameObject coinTextObject = GameObject.FindWithTag("CoinCounterText");
            if (coinTextObject != null)
            {
                textMeshPro = coinTextObject.GetComponent<TextMeshProUGUI>();
                UpdateCoinUIText(); // Update the text when the scene loads
            }
        }

        private void UpdateCoinCount(string coinText)
        {
            totalCoinsCollected++;
            Debug.Log($"Total Coins Collected: {totalCoinsCollected}/{totalCoinsAvailable}");
            UpdateCoinUIText();
        }

        private void UpdateCoinUIText()
        {
            if (textMeshPro != null)
            {
                textMeshPro.text = $"{totalCoinsCollected}/{totalCoinsAvailable}";
            }
        }

        public int GetTotalCoinsCollected()
        {
            return totalCoinsCollected;
        }
    }
}
