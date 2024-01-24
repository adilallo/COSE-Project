using UnityEngine;
using System;

namespace COSE.Sphere
{
    public class SphereTrigger : MonoBehaviour
    {
        public enum SphereType { Hypothesis, Conclusion }
        public SphereType type; // Set this in the inspector
        public int sphereIndex; // Set this in the inspector

        public static event Action<int, SphereType> OnSphereTriggered;

        private Collider sphereCollider;

        void Start()
        {
            sphereCollider = GetComponent<Collider>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                OnSphereTriggered?.Invoke(sphereIndex, type);
                sphereCollider.enabled = false;
            }
        }
    }
}
