using UnityEngine;

namespace COSE.Interactions
{
    public class HypothesisInteraction : MonoBehaviour
    {
        [SerializeField] private GameObject hypothesis1;
        [SerializeField] private float targetYPosition1; // Assign a Transform to mark the target position
        [SerializeField] private float speed = 2f;
        private bool isActive1 = false;

        void Update()
        {
            if (isActive1)
            {
                hypothesis1.SetActive(true);
                float step = speed * Time.deltaTime;
                Vector3 currentPosition = hypothesis1.transform.position;
                Vector3 targetPosition = new Vector3(currentPosition.x, targetYPosition1, currentPosition.z);

                Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, step);
                hypothesis1.transform.position = newPosition;

                // Check if the movement along Y is complete
                if (Mathf.Abs(newPosition.y - targetYPosition1) < 0.001f)
                {
                    isActive1 = false;
                }
            }
        }

        public void ActivateMovement1()
        {
            isActive1 = true;
        }
    }
}
