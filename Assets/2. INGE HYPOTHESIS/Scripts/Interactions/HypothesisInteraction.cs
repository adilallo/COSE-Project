using UnityEngine;
using System.Collections.Generic;

namespace COSE.Interactions
{
    [System.Serializable]
    public class MovementState
    {
        public Vector3 targetPosition;
        public Quaternion targetRotation;
    }

    public class HypothesisInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject hypothesisModel;
        [SerializeField] private List<MovementState> movementStates;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        private int currentStateIndex = -1;

        void Start()
        {
            if (movementStates.Count > 0)
            {
                hypothesisModel.transform.position = movementStates[0].targetPosition;
                hypothesisModel.transform.rotation = movementStates[0].targetRotation;
            }
        }

        void Update()
        {
            if (currentStateIndex >= 0)
            {
                MoveAndRotateHypothesis();
            }
        }

        public void ActivateState(int stateIndex)
        {
            if (stateIndex >= 0 && stateIndex < movementStates.Count)
            {
                currentStateIndex = stateIndex;
            }
        }

        private void MoveAndRotateHypothesis()
        {
            MovementState currentState = movementStates[currentStateIndex];

            // Move
            Vector3 newPosition = Vector3.MoveTowards(
                hypothesisModel.transform.position,
                currentState.targetPosition,
                movementSpeed * Time.deltaTime);
            hypothesisModel.transform.position = newPosition;

            // Rotate
            Quaternion newRotation = Quaternion.RotateTowards(
                hypothesisModel.transform.rotation,
                currentState.targetRotation,
                rotationSpeed * Time.deltaTime);
            hypothesisModel.transform.rotation = newRotation;
        }
    }
}
