using UnityEngine;
using System.Collections.Generic;

namespace COSE.Interactions
{
    public class SphereEventManager : MonoBehaviour
    {
        [SerializeField] private SphereInteraction sphereInteraction;
        [SerializeField] private SphereConclusions sphereConclusions;

        void OnEnable()
        {
            SphereTrigger.OnSphereTriggered += HandleSphereTriggered;
        }

        void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= HandleSphereTriggered;
        }

        private void HandleSphereTriggered(int index, SphereTrigger.SphereType type)
        {
            if (type == SphereTrigger.SphereType.Hypothesis)
            {
                sphereInteraction.HandleSphereTriggered(index);
            }
            else if (type == SphereTrigger.SphereType.Conclusion)
            {
                sphereConclusions.HandleConclusionSphereTriggered(index);
            }
        }
    }
}
