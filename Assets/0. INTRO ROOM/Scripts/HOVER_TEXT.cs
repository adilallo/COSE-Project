using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HOVER_TEXT : MonoBehaviour
{

    public GameObject TEXT;
   
   void Start()
    {
       TEXT.SetActive(false);
    }

  void OnTriggerEnter(Collider other)
    {
        TEXT.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        TEXT.SetActive(false);
    }
}
