using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using COSE.Text;
using COSE.Sphere;
using System;
using System.Threading.Tasks;

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
        [SerializeField] private IngeTextInteraction textInteraction;
        [SerializeField] private GameObject hypothesisModel;
        [SerializeField] private List<MovementState> movementStates;
        [SerializeField] private float movementSpeed = 2f;
        [SerializeField] private float rotationSpeed = 2f;
        [SerializeField] public List<HypothesisLayerInteraction> mainHypothesisLayers;
        [SerializeField] private GameObject invisibleWall;
        [SerializeField] private List<GameObject> heroModel;
        [SerializeField] private List<MovementState> heroMovementStates;
        [SerializeField] private GameObject diagram;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip vanishClip;

        private int currentStateIndex = -1;
        private bool coroutineStarted = false;
        private string currentActiveTextKey;

        public static event Action<int> OnHypothesisMovementComplete;

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

        private void SetCurrentActiveTextKey(string key)
        {
            currentActiveTextKey = key;
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

            string initialTextKey = layer.textKey;
            SetCurrentActiveTextKey(initialTextKey);

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

            if (layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L2_LOC_ID" ||
                layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L11_LOC_ID" ||
                layer.textKey == "INGE_LAYER_HYPOTHESIS_1_L16_LOC_ID")
            {
                audioSource.clip = vanishClip;
                audioSource.Play();
                layerObject.SetActive(false);
                yield return new WaitForSeconds(2.0f);

                if (layer.textKey == initialTextKey && currentActiveTextKey == initialTextKey)
                {
                    textInteraction.DeactivateAllTexts();
                }
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

        public void MoveMultipleModels()
        {
            StartCoroutine(MoveAndRotateMultipleModelsCoroutine(heroModel.ToArray(), heroMovementStates.ToArray()));
        }

        private IEnumerator MoveAndRotateMultipleModelsCoroutine(GameObject[] models, MovementState[] targetStates)
        {
            if (models.Length != targetStates.Length)
            {
                Debug.LogError("Number of models and target states must match.");
                yield break;
            }

            bool[] isMovementComplete = new bool[models.Length];
            for (int i = 0; i < isMovementComplete.Length; i++)
            {
                isMovementComplete[i] = false;
            }

            while (true)
            {
                bool allModelsReachedTarget = true;

                for (int i = 0; i < models.Length; i++)
                {
                    if (!isMovementComplete[i])
                    {
                        GameObject model = models[i];
                        MovementState targetState = targetStates[i];

                        // If the target state position is in local space, transform it to world space
                        Vector3 targetWorldPosition = model.transform.parent != null
                            ? model.transform.parent.TransformPoint(targetState.targetPosition)
                            : targetState.targetPosition;

                        // Move model
                        Vector3 newPosition = Vector3.MoveTowards(
                            model.transform.position,
                            targetWorldPosition,
                            movementSpeed * Time.deltaTime);
                        model.transform.position = newPosition;

                        // Rotate model
                        Quaternion newRotation = Quaternion.RotateTowards(
                            model.transform.rotation,
                            targetState.targetRotation,
                            rotationSpeed * Time.deltaTime);
                        model.transform.rotation = newRotation;

                        // Check if model reached target position and rotation
                        if (Vector3.Distance(model.transform.position, targetWorldPosition) < 0.01f &&
                            Quaternion.Angle(model.transform.rotation, targetState.targetRotation) < 0.01f)
                        {
                            isMovementComplete[i] = true;
                        }
                        else
                        {
                            allModelsReachedTarget = false;
                        }
                    }
                }

                // Break loop if all models have reached their target positions
                if (allModelsReachedTarget)
                {
                    break;
                }

                yield return null;
            }
        }
    }
}
