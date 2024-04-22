using UnityEngine;
using System;

namespace COSE.Sphere
{
    public class SphereTrigger : MonoBehaviour
    {
        public string sphereText;

        public static event Action<string> OnSphereTriggered;

        private Collider sphereCollider;

        void Start()
        {
            sphereCollider = GetComponent<Collider>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnSphereTriggered?.Invoke(sphereText);
                Debug.Log("Sphere Triggered: " + sphereText);
                sphereCollider.enabled = false;
            }
        }
    }
}
