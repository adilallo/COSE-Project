using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using COSE.Text;
using COSE.Sphere;
using System;

namespace COSE.Hypothesis
{
    [System.Serializable]
    public class MovementState
    {
        public Vector3 targetPosition;
        public Quaternion targetRotation;
    }

    public class HypothesisInteraction : MonoBehaviour
    {
        [SerializeField] private List<GameObject> heroModel;
        [SerializeField] private GameObject hypothesis7;
        [SerializeField] public GameObject hypothesis8Screenshot1;
        [SerializeField] public GameObject hypothesis8Screenshot2;
        [SerializeField] public GameObject hypothesis9Scheme1;
        [SerializeField] public GameObject hypothesis9Scheme2;

        // currently used fields
        [SerializeField] private IngeTextInteraction textInteraction;
        [SerializeField] private GameObject hypothesisModel;
        [SerializeField] private List<MovementState> movementStates;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] public List<HypothesisLayerInteraction> mainHypothesisLayers;
        [SerializeField] private GameObject invisibleWall;
        [SerializeField] private GameObject diagram;
        private int currentStateIndex = -1;
        private bool coroutineStarted = false;

        // currently used properties
        public int CurrentStateIndex
        {
            get { return currentStateIndex; }
            set { currentStateIndex = value; }
        }


        private bool isMovementComplete = false;

        void Start()
        {
            if (movementStates.Count > 0)
            {
                hypothesisModel.transform.position = movementStates[0].targetPosition;
                hypothesisModel.transform.rotation = movementStates[0].targetRotation;
            }
            foreach (var layer in mainHypothesisLayers)
            {
                layer.Initialize();
            }
        }

        void Update()
        {

            if (currentStateIndex == 1 && !coroutineStarted && Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(MoveLayersSequentially(movementStates[currentStateIndex], 20.0f));
                coroutineStarted = true;
            }
            if (currentStateIndex == 2)
            {
                MoveAndRotateHypothesis(); 
            }
            if (currentStateIndex == 3)
            {
                MoveAndRotateHypothesis();
            }
            if (currentStateIndex == 4)
            {
                MoveAndRotateHypothesis();
            }
            if (currentStateIndex == 5)
            {
                MoveAndRotateHypothesis();
                ResetAllLayers();
                ActivateLayerByIndex(3);
            }
            if (currentStateIndex == 6)
            {
                MoveAndRotateHypothesis();
                ResetAllLayers();
                ActivateHeroModels();
            }
            if (currentStateIndex == 7)
            {
                hypothesis7.SetActive(true);
            }
            if (currentStateIndex == 9)
            {
                hypothesis9Scheme1.SetActive(true);
            }
        }

        private IEnumerator MoveLayersSequentially(MovementState targetState, float delayBetweenLayers)
        {
            yield return new WaitForSeconds(0.1f);

            float startTime = Time.time;
            float lastKeyPressTime = 0f;
            float debounceTime = 0.3f;

            // Calculate each layer's target position and rotation as if the parent had moved
            for (int i = 1; i < mainHypothesisLayers.Count; i++)
            {
                var layer = mainHypothesisLayers[i];

                // Calculate the new world position and rotation for the layer
                Vector3 targetLayerWorldPosition = hypothesisModel.transform.TransformPoint(layer.initialLocalPosition);
                Quaternion targetLayerWorldRotation = hypothesisModel.transform.rotation * layer.initialLocalRotation;
                // Calculate the offset from the parent's initial position and rotation to the target state
                Vector3 positionOffset = targetState.targetPosition - movementStates[0].targetPosition;
                Quaternion rotationOffset = targetState.targetRotation * Quaternion.Inverse(movementStates[0].targetRotation);
                // Apply the offset to the layer's target world position and rotation
                Vector3 finalLayerPosition = targetLayerWorldPosition + positionOffset;
                Quaternion finalLayerRotation = rotationOffset * targetLayerWorldRotation;

                StartCoroutine(MoveLayer(layer, finalLayerPosition, finalLayerRotation, movementSpeed, rotationSpeed));
                yield return new WaitUntil(() => Time.time >= startTime + delayBetweenLayers || (Input.GetKeyDown(KeyCode.Return) && Time.time - lastKeyPressTime > debounceTime));

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    lastKeyPressTime = Time.time;
                }
                startTime = Time.time;
            }
        }

        private IEnumerator MoveLayer(HypothesisLayerInteraction layer, Vector3 targetGlobalPosition, Quaternion targetGlobalRotation, float moveSpeed, float rotSpeed)
        {
            GameObject layerObject = layer.gameObject;
            layerObject.SetActive(true);

            while (layerObject.transform.position != targetGlobalPosition ||
                   layerObject.transform.rotation != targetGlobalRotation)
            {

                layerObject.transform.position = Vector3.MoveTowards(
                    layerObject.transform.position,
                    targetGlobalPosition,
                    moveSpeed * Time.deltaTime);

                layerObject.transform.rotation = Quaternion.RotateTowards(
                    layerObject.transform.rotation,
                    targetGlobalRotation,
                    rotSpeed * Time.deltaTime);

                if (currentStateIndex == 1)
                {
                    textInteraction.ActivateLayerText(layer.textKey);
                }

                yield return null;
            }

            if (layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L2_LOC_ID" || layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L11_LOC_ID" || layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L16_LOC_ID")
            {
                layerObject.SetActive(false);
            }

            if (layer == mainHypothesisLayers[mainHypothesisLayers.Count - 1])
            {
                invisibleWall.SetActive(false);
                diagram.SetActive(true);
            }
        }

        private void MoveAndRotateHypothesis()
        {
            MovementState currentState = movementStates[currentStateIndex];

            Vector3 newPosition = Vector3.MoveTowards(
                hypothesisModel.transform.position,
                currentState.targetPosition,
                movementSpeed * Time.deltaTime);
            hypothesisModel.transform.position = newPosition;

            Quaternion newRotation = Quaternion.RotateTowards(
                hypothesisModel.transform.rotation,
                currentState.targetRotation,
                rotationSpeed * Time.deltaTime);
            hypothesisModel.transform.rotation = newRotation;

            // Check if the hypothesis model has reached the target position and rotation
            if (hypothesisModel.transform.position == currentState.targetPosition &&
                hypothesisModel.transform.rotation == currentState.targetRotation)
            {
                if (!isMovementComplete)
                {
                    isMovementComplete = true;
                }
            }
            else
            {
                isMovementComplete = false;
            }
        }

        public void ResetAllLayers()
        {
            foreach (var layerInteraction in mainHypothesisLayers)
            {
                layerInteraction.gameObject.SetActive(false);

                var outline = layerInteraction.gameObject.GetComponent<Outline>() ?? layerInteraction.gameObject.GetComponentInChildren<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
        }

        private void ActivateLayerByIndex(int index)
        {
            if (index >= 0 && index < mainHypothesisLayers.Count)
            {
                var layer = mainHypothesisLayers[index];
                layer.gameObject.SetActive(true);
            }
        }

        private void ActivateHeroModels()
        {
            foreach (var model in heroModel)
            {
                model.SetActive(true);
            }
        }

        
    }
}
