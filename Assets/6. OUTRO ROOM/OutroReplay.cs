using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutroReplay : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PersistenceManager.Instance != null)
            {
                PersistenceManager.Instance.ResetCoins();
            }
        }
    }
}
