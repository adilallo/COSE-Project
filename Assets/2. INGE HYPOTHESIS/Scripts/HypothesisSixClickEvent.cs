using UnityEngine;

namespace COSE.Hypothesis
{
    public class HypothesisSixClickEvent : MonoBehaviour
    {
        public static event System.Action<int> OnHeroModelClicked;
        [SerializeField] private int heroId; // Make sure this is visible in the Inspector now.

        void OnMouseDown()
        {
            OnHeroModelClicked?.Invoke(heroId);
            Debug.Log($"Hero model with ID {heroId} was clicked.");
        }
    }
}
