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
        [SerializeField] private GameObject hypothesisModel;
        [SerializeField] private List<MovementState> movementStates;
        [SerializeField] private List<GameObject> heroModel;
        [SerializeField] private GameObject hypothesis7;
        [SerializeField] public GameObject hypothesis8Screenshot1;
        [SerializeField] public GameObject hypothesis8Screenshot2;
        [SerializeField] public GameObject hypothesis9Scheme1;
        [SerializeField] public GameObject hypothesis9Scheme2;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;

        [SerializeField] public List<HypothesisLayerInteraction> mainHypothesisLayers;

        public static event Action<string> OnCouplingActivated;
        public static event Action<bool> OnLastLayerFinished;

        public int currentStateIndex = -1;
        [HideInInspector] public bool isMovementComplete = false;
        [HideInInspector] public bool isLastLayerFinished = false;

        private int currentCouplingIndex = -1;
        private bool coroutineStarted = false;

        private Dictionary<string, int> stateMap = new Dictionary<string, int>()
        {
            { "INGE_SPHERE_ENTRY_LOC_ID", 0 },
            { "INGE_SPHERE_HYPOTHESIS_1_LOC_ID", 1 },
            { "INGE_SPHERE_HYPOTHESIS_2_LOC_ID", 2 },
            { "INGE_SPHERE_HYPOTHESIS_3_LOC_ID", 3 },
            { "INGE_SPHERE_HYPOTHESIS_4_LOC_ID", 4 },
            { "INGE_SPHERE_HYPOTHESIS_5_LOC_ID", 5 },
            { "INGE_SPHERE_HYPOTHESIS_6_LOC_ID", 6 },
            { "INGE_SPHERE_HYPOTHESIS_7_LOC_ID", 7 },
            { "INGE_SPHERE_HYPOTHESIS_8_LOC_ID", 8 },
            { "INGE_SPHERE_HYPOTHESIS_9_LOC_ID", 9 },
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

                if (Input.GetKeyDown(KeyCode.X))
                {
                    CycleCouplings(1);
                }
                else if (Input.GetKeyDown(KeyCode.Z))
                {
                    CycleCouplings(-1);
                }
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

        private void CycleCouplings(int direction)
        {
            currentCouplingIndex += direction;
            if (currentCouplingIndex >= couplings.Count) currentCouplingIndex = 0;
            else if (currentCouplingIndex < 0) currentCouplingIndex = couplings.Count - 1;

            ActivateCoupling(couplings[currentCouplingIndex]);
        }

        private void ActivateCoupling(List<int> coupling)
        {
            foreach (var layer in mainHypothesisLayers)
            {
                layer.gameObject.SetActive(false);
            }

            foreach (int id in coupling)
            {
                var layerToActivate = mainHypothesisLayers.Find(layer => layer.layerIndex == id);
                if (layerToActivate != null)
                {
                    layerToActivate.gameObject.SetActive(true);
                }
            }

            bool isTightlyCoupled = currentCouplingIndex != couplings.Count - 1;
            OnCouplingActivated?.Invoke(isTightlyCoupled ? "Tightly Coupled" : "Loosely Coupled");
        }

        public void ActivateState(string stateIdentifier)
        {
            if (stateMap.TryGetValue(stateIdentifier, out int stateIndex))
            {
                currentStateIndex = stateIndex;
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

        public IEnumerator MoveLayersSequentially(MovementState targetState, float delayBetweenLayers)
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
                yield return new WaitUntil(() => Time.time >= startTime + delayBetweenLayers || (Input.GetKeyDown(KeyCode.X) && Time.time - lastKeyPressTime > debounceTime));

                if (Input.GetKeyDown(KeyCode.X))
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
                    HypothesisLayerInteraction.NotifyLayerMoved(layer.textKey);
                }

                yield return null;
            }

            if (layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L2_LOC_ID" || layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L11_LOC_ID" || layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L16_LOC_ID")
            {
                layerObject.SetActive(false);
            }

            if (layer == mainHypothesisLayers[mainHypothesisLayers.Count - 1])
            {
                isLastLayerFinished = true;
                OnLastLayerFinished?.Invoke(isLastLayerFinished);
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
    }
}
