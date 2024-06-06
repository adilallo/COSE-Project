using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_change_level : MonoBehaviour
{
    [SerializeField]
    private string loadLevel;
    [SerializeField]
    private Vector3 spawnPosition = Vector3.zero;
    [SerializeField]
    private bool useSpawnPosition = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (useSpawnPosition)
            {
                SceneManager.LoadScene(loadLevel, LoadSceneMode.Single);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                SceneManager.LoadScene(loadLevel);
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == loadLevel)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player)
            {
                player.transform.position = spawnPosition;
            }
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
