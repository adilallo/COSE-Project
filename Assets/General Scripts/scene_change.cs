using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scene_change : MonoBehaviour
{
   
    public string LoadLevel;

  void OnTriggerEnter(Collider other)
     
    {
        if(other.tag == "Player")

        { 
            SceneManager.LoadScene(LoadLevel);
        }
        
    }
}
