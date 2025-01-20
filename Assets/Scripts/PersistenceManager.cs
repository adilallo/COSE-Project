using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using COSE.Coin;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance { get; private set; }

    // --- New Fields ---
    // If you already stored this in an array, you can remove the old array/list approach.
    private HashSet<string> collectedCoins = new HashSet<string>();
    private HashSet<string> allCoins = new HashSet<string>();

    private int totalCoinsCollected = 0;
    [SerializeField] private int totalCoinsAvailable = 0; // We will recalc this dynamically
    private TextMeshProUGUI textMeshPro;

    private HashSet<string> visitedRooms = new HashSet<string>();
    private GameObject finalRoomObject;

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
        // Subscribe to coin events
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
        // 1) Find coin triggers in this new scene
        var rootObjects = scene.GetRootGameObjects();
        var allTransforms = rootObjects
        .SelectMany(root => root.GetComponentsInChildren<Transform>(true));

        var finalRoomTransform = allTransforms
            .FirstOrDefault(t => t.CompareTag("FinalRoom"));

        var coinsInScene = rootObjects
            .SelectMany(obj => obj.GetComponentsInChildren<CoinTrigger>(true))
            .ToArray();

        foreach (var coin in coinsInScene)
        {
            string coinUniqueID = GetCoinID(coin); // or just coin.coinText if public
            if (!allCoins.Contains(coinUniqueID))
            {
                allCoins.Add(coinUniqueID);
            }

            // If we already collected this coin in a previous scene, deactivate it
            if (collectedCoins.Contains(coinUniqueID))
            {
                coin.gameObject.SetActive(false);
            }
        }

        // Update total coins available based on discovered coins so far
        totalCoinsAvailable = allCoins.Count;

        // 2) Update the TextMeshPro UI in the new scene
        GameObject coinTextObject = GameObject.FindWithTag("CoinCounterText");
        if (coinTextObject != null)
        {
            textMeshPro = coinTextObject.GetComponent<TextMeshProUGUI>();
            UpdateCoinUIText();
        }

        // 3) Reassign the final room object when intro scene is loaded
        if (scene.name == "SCENE_INTRO")
        {
            if (finalRoomTransform != null)
            {
                finalRoomObject = finalRoomTransform.gameObject;
                Debug.Log($"Final room object reassigned: {finalRoomObject}");
            }
            if (finalRoomObject != null && visitedRooms.Count == 4)
            {
                finalRoomObject.SetActive(true);
            }
        }

        // 4) Track room visits
        if (IsRoomScene(scene.name) && !visitedRooms.Contains(scene.name))
        {
            visitedRooms.Add(scene.name);
            Debug.Log($"Room visited: {scene.name}");
            CheckAllRoomsVisited();
        }
    }

    private void UpdateCoinCount(string coinTextOrID)
    {
        // If coin is not already in collectedCoins, add it
        if (!collectedCoins.Contains(coinTextOrID))
        {
            collectedCoins.Add(coinTextOrID);
            totalCoinsCollected++;
        }

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

    private bool IsRoomScene(string sceneName)
    {
        // Define the list of room names
        return sceneName == "SCENE_JIAWEN_HYPOTHESIS"
            || sceneName == "SCENE_INGE_HYPOTHESIS"
            || sceneName == "SCENE_DANIELA_HYPOTHESIS"
            || sceneName == "SCENE_YANNICK_HYPOTHESIS";
    }

    private void CheckAllRoomsVisited()
    {
        if (visitedRooms.Count == 4)
        {
            ActivateFinalRoom();
        }
    }

    private void ActivateFinalRoom()
    {
        if (finalRoomObject == null)
        {
            finalRoomObject = GameObject.FindWithTag("FinalRoom");
        }

        if (finalRoomObject != null)
        {
            finalRoomObject.SetActive(true);
            Debug.Log("All rooms visited. Final room is now available.");
        }
    }

    // Helper to get the coin's unique ID (replace with coin.coinID if that's your unique property)
    private string GetCoinID(CoinTrigger coin)
    {
        return coin.GetType()
                   .GetField("coinText",
                             System.Reflection.BindingFlags.NonPublic
                           | System.Reflection.BindingFlags.Instance)
                   ?.GetValue(coin) as string;
    }

    // Optionally provide a getter if you need the total count externally
    public int GetTotalCoinsCollected()
    {
        return totalCoinsCollected;
    }

    // Optional: If you want a Reset method that clears progress
    public void ResetCoins()
    {
        collectedCoins.Clear();
        totalCoinsCollected = 0;
        // If you reload the scenes, all coins reappear
        UpdateCoinUIText();
        Debug.Log("Coins reset.");
    }

    public void ResetVisitedRooms()
    {
        // Clear the visited rooms list
        visitedRooms.Clear();

        // Deactivate the final room if it's currently active
        if (finalRoomObject != null)
        {
            finalRoomObject.SetActive(false);
        }

        Debug.Log("Visited rooms reset and final room deactivated.");
    }
}
