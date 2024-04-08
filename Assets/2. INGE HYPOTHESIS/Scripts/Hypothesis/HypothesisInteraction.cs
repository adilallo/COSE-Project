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

    [System.Serializable]
    public class LayerInteraction
    {
        public GameObject layerObject;
        public string textKey;
        public int layerIndex;
        [HideInInspector] public Vector3 initialLocalPosition;
        [HideInInspector] public Quaternion initialLocalRotation;

        public void Initialize()
        {
            initialLocalPosition = layerObject.transform.localPosition;
            initialLocalRotation = layerObject.transform.localRotation;
        }

        public static event Action<string> OnLayerMoved;

        public static void NotifyLayerMoved(string textKey)
        {
            OnLayerMoved?.Invoke(textKey);
        }
    }

    public class HypothesisInteraction : MonoBehaviour
    {
        public delegate void LastLayerFinishedHandler();
        public static event LastLayerFinishedHandler OnLastLayerFinished;

        [SerializeField] private GameObject hypothesisModel;
        [SerializeField] private TextInteraction textInteraction;
        [SerializeField] private List<MovementState> movementStates;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;

        [SerializeField] public List<LayerInteraction> mainHypothesisLayers;

        public int currentStateIndex = -1;
        private int currentCouplingIndex = -1;
        public bool isSphereTwoTriggered = false;
        public bool isSphereThreeTriggered = false;
        public bool isSphereFourTriggered = false;
        public bool isSphereFiveTriggered = false;
        public bool isSphereSixTriggered = false;
        public bool isMovementComplete = false;
        private bool coroutineStarted = false;

        private bool _isLastLayerFinished = false;
        public bool IsLastLayerFinished
        {
            get { return _isLastLayerFinished; }
            private set
            {
                _isLastLayerFinished = value;
                if (_isLastLayerFinished)
                {
                    OnLastLayerFinished?.Invoke();
                }
            }
        }

        private Dictionary<string, int> stateMap = new Dictionary<string, int>()
        {
            { "INGE_SPHERE_ENTRY_LOC_ID", 0 },
            { "INGE_SPHERE_HYPOTHESIS_1_LOC_ID", 1 },
            { "INGE_SPHERE_HYPOTHESIS_2_LOC_ID", 2 }
        };

        private List<List<int>> couplings = new List<List<int>>()
        {
                new List<int>{2, 11, 13, 16},
                new List<int>{1, 12, 8},
                new List<int>{2, 3},
                new List<int>{3, 15},
                new List<int>{4, 13, 15},
                new List<int>{5, 9},
                new List<int>{5, 6, 7, 9, 14, 17},
                new List<int>{6, 14},
                new List<int>{10, 13},
                new List<int>{1, 3, 5, 6, 7, 8, 9, 10, 12, 13, 14, 15, 17}
        };

        private void OnEnable()
        {
            SphereTrigger.OnSphereTriggered += ActivateState;
        }

        private void OnDisable()
        {
            SphereTrigger.OnSphereTriggered -= ActivateState;
        }

        void Start()
        {
            if (movementStates.Count > 0)
            {
                hypothesisModel.transform.position = movementStates[0].targetPosition;
                hypothesisModel.transform.rotation = movementStates[0].targetRotation;
            }
            // Initialize each layer's relative position and rotation
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

            if (isSphereTwoTriggered)
            {
                MoveAndRotateHypothesis(); 
            }

            if (isSphereThreeTriggered)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    CycleCouplings(1);
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    CycleCouplings(-1);
                }
            }

            if (isSphereFiveTriggered)
            {
                DeactivateAllOutlinesAndObjects();
                ActivateLayerByIndex(3);
            }

            if (isSphereSixTriggered)
            {
                DeactivateAllOutlinesAndObjects();
            }
        }



        void CycleCouplings(int direction)
        {
            currentCouplingIndex += direction;
            if (currentCouplingIndex >= couplings.Count) currentCouplingIndex = 0;
            else if (currentCouplingIndex < 0) currentCouplingIndex = couplings.Count - 1;

            ActivateCoupling(couplings[currentCouplingIndex]);
        }

        void ActivateCoupling(List<int> coupling)
        {
            // First, reset activation of all layers
            foreach (var layer in mainHypothesisLayers)
            {
                layer.layerObject.SetActive(false); // Deactivate all first, or adjust based on your logic for visibility
            }

            // Then, activate only the layers in the current coupling
            foreach (int id in coupling)
            {
                var layerToActivate = mainHypothesisLayers.Find(layer => layer.layerIndex == id);
                if (layerToActivate != null)
                {
                    layerToActivate.layerObject.SetActive(true);
                }
            }

            bool isTightlyCoupled = currentCouplingIndex != couplings.Count - 1; // Assuming the last list is loosely coupled
            textInteraction.ActivateCouplingText(coupling, isTightlyCoupled);
        }

        public void ActivateState(string stateIdentifier)
        {
            if (stateMap.TryGetValue(stateIdentifier, out int stateIndex))
            {
                if (stateIndex >= 0 && stateIndex < movementStates.Count)
                {
                    currentStateIndex = stateIndex;
                }
            }
            else
            {
                Debug.LogError($"State identifier {stateIdentifier} not found.");
            }
        }

        public IEnumerator MoveLayersSequentially(MovementState targetState, float delayBetweenLayers)
        {
            // Wait before starting the sequence to ensure all initialization is complete
            yield return new WaitForSeconds(0.1f);

            float startTime = Time.time;
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

                // Start moving the layer after a delay
                StartCoroutine(MoveLayer(layer, finalLayerPosition, finalLayerRotation, movementSpeed, rotationSpeed));
                yield return new WaitUntil(() => Time.time >= startTime + delayBetweenLayers || Input.GetKeyDown(KeyCode.Q));
                startTime = Time.time;
            }
        }

        private IEnumerator MoveLayer(LayerInteraction layer, Vector3 targetGlobalPosition, Quaternion targetGlobalRotation, float moveSpeed, float rotSpeed)
        {
            // Now move and rotate the layer towards its target global position and rotation
            GameObject layerObject = layer.layerObject;
            layerObject.SetActive(true);

            while (layerObject.transform.position != targetGlobalPosition ||
                   layerObject.transform.rotation != targetGlobalRotation)
            {
                // Check if the 'Q' key is pressed to break out of the animation
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    // Optionally, directly set the final position and rotation
                    layerObject.transform.position = targetGlobalPosition;
                    layerObject.transform.rotation = targetGlobalRotation;
                    break;
                }

                layerObject.transform.position = Vector3.MoveTowards(
                    layerObject.transform.position,
                    targetGlobalPosition,
                    moveSpeed * Time.deltaTime);

                layerObject.transform.rotation = Quaternion.RotateTowards(
                    layerObject.transform.rotation,
                    targetGlobalRotation,
                    rotSpeed * Time.deltaTime);

                // Activate the text associated with this layer
                if (currentStateIndex == 1)
                {
                    LayerInteraction.NotifyLayerMoved(layer.textKey);
                }

                yield return null;
            }

            if (layerObject.name == "Layer_2_GREY_FIELD_EPHEMERAL" || layerObject.name == "Layer_11_URL_colour_field" || layerObject.name == "Layer_16_search_color_field_EPHEMERAL")
            {
                layerObject.SetActive(false);
            }

            if (layer == mainHypothesisLayers[mainHypothesisLayers.Count - 1])
            {
                IsLastLayerFinished = true;
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

        public void ActivateLayerByIndex(int index)
        {
            if (index >= 0 && index < mainHypothesisLayers.Count)
            {
                var layer = mainHypothesisLayers[index];
                // Activate the layer however appropriate, e.g., setting it active, highlighting, etc.
                // For example:
                layer.layerObject.SetActive(true);
            }
        }

        public void DeactivateAllOutlinesAndObjects()
        {
            foreach (var layerInteraction in mainHypothesisLayers)
            {
                layerInteraction.layerObject.SetActive(false);

                var outline = layerInteraction.layerObject.GetComponent<Outline>() ?? layerInteraction.layerObject.GetComponentInChildren<Outline>();
                if (outline != null)
                {
                    outline.enabled = false;
                }
            }
        }
    }
}
