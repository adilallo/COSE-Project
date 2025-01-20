using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDControl : MonoBehaviour
{
    [SerializeField] private GameObject HUD;
    [SerializeField] private Scrollbar scrollbar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            HUD.SetActive(!HUD.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            scrollbar.value -= 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            scrollbar.value += 0.1f;
        }
    }
}
