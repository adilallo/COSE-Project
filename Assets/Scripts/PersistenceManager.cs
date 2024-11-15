using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using COSE.Coin;

public class PersistenceManager : MonoBehaviour
{
    public static PersistenceManager Instance { get; private set; }

    private int totalCoinsCollected = 0;
    [SerializeField] private int totalCoinsAvailable = 48;
    private TextMeshProUGUI textMeshPro;

    private HashSet<string> visitedRooms = new HashSet<string>();
    private GameObject finalRoomObject; // Removed [SerializeField] to handle dynamically

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
        // Update the TextMeshPro UI in the new scene
        GameObject coinTextObject = GameObject.FindWithTag("CoinCounterText");
        if (coinTextObject != null)
        {
            textMeshPro = coinTextObject.GetComponent<TextMeshProUGUI>();
            UpdateCoinUIText();
        }

        // Reassign the final room object when intro scene is loaded
        if (scene.name == "SCENE_INTRO")
        {
            finalRoomObject = GameObject.FindWithTag("FinalRoom");
            if (finalRoomObject != null && visitedRooms.Count == 4)
            {
                // If all rooms have already been visited, make sure the final room is activated
                finalRoomObject.SetActive(true);
            }
        }

        // Track room visits
        if (IsRoomScene(scene.name) && !visitedRooms.Contains(scene.name))
        {
            visitedRooms.Add(scene.name);
            Debug.Log($"Room visited: {scene.name}");
            CheckAllRoomsVisited();
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

    private bool IsRoomScene(string sceneName)
    {
        // Define the list of room names
        return sceneName == "SCENE_JIAWEN_HYPOTHESIS" || sceneName == "SCENE_INGE_HYPOTHESIS" || sceneName == "SCENE_DANIELA_HYPOTHESIS" || sceneName == "SCENE_YANNICK_HYPOTHESIS";
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
            // Try to find the final room object if not already assigned
            finalRoomObject = GameObject.FindWithTag("FinalRoom");
        }

        if (finalRoomObject != null)
        {
            finalRoomObject.SetActive(true);
            Debug.Log("All rooms visited. Final room is now available.");
        }
    }

    public int GetTotalCoinsCollected()
    {
        return totalCoinsCollected;
    }
}
