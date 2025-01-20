using UnityEngine;
using System.Collections;

// Quits the player when the user hits escape

public class ESCAPE : MonoBehaviour
{
    public static ESCAPE Instance { get; private set; }

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
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("QUIT!"); 
            Application.Quit();
        }
    }
}
