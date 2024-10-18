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

        public static event Action<int> OnHypothesisMovementComplete;

        // currently used properties
        public int CurrentStateIndex
        {
            get { return currentStateIndex; }
            set { currentStateIndex = value; }
        }

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
                StartCoroutine(MoveLayersSequentially(movementStates[currentStateIndex], 16.0f));
                coroutineStarted = true;
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

                DisableAllOutlines();

                StartCoroutine(MoveLayer(layer, finalLayerPosition, finalLayerRotation, movementSpeed, rotationSpeed));
                yield return new WaitUntil(() => Time.time >= startTime + delayBetweenLayers || (Input.GetKeyDown(KeyCode.Return) && Time.time - lastKeyPressTime > debounceTime));

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    lastKeyPressTime = Time.time;
                }
                startTime = Time.time;
            }
        }

        private void DisableAllOutlines()
        {
            foreach (HypothesisLayerInteraction layer in mainHypothesisLayers)
            {
                layer.OutlineLayer(false);
            }
        }

        private IEnumerator MoveLayer(HypothesisLayerInteraction layer, Vector3 targetGlobalPosition, Quaternion targetGlobalRotation, float moveSpeed, float rotSpeed)
        {
            GameObject layerObject = layer.gameObject;
            layerObject.SetActive(true);
            layer.OutlineLayer(true);

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
                textInteraction.ActivateLayerText(layer.textKey);


                yield return null;
            }

            layer.OutlineLayer(false);

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

        public void MoveModel(GameObject model)
        {
            StartCoroutine(MoveAndRotateModelCoroutine(model));
        }

        private IEnumerator MoveAndRotateModelCoroutine(GameObject model)
        {
            MovementState currentState = movementStates[currentStateIndex];

            while (model.transform.position != currentState.targetPosition || model.transform.rotation != currentState.targetRotation)
            {
                Vector3 newPosition = Vector3.MoveTowards(
                    model.transform.position,
                    currentState.targetPosition,
                    movementSpeed * Time.deltaTime);
                model.transform.position = newPosition;

                Quaternion newRotation = Quaternion.RotateTowards(
                    model.transform.rotation,
                    currentState.targetRotation,
                    rotationSpeed * Time.deltaTime);
                model.transform.rotation = newRotation;

                yield return null;
            }

            OnHypothesisMovementComplete?.Invoke(CurrentStateIndex);
        }
    }
}
