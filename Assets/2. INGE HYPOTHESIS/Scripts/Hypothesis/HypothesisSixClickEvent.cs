using UnityEngine;

namespace COSE.Hypothesis
{
    public class HypothesisSixClickEvent : MonoBehaviour
    {
        public static event System.Action<int> OnHeroModelClicked;
        public static HypothesisSixClickEvent currentOutlinedHero;
        [SerializeField] private int heroId;

        private Outline outline;

        private void Start()
        {
            outline = GetComponent<Outline>() ?? GetComponentInChildren<Outline>();
            outline.enabled = false;
        }

        void OnMouseDown()
        {
            OnHeroModelClicked?.Invoke(heroId);
            Debug.Log($"Hero model with ID {heroId} was clicked.");

            if (currentOutlinedHero != null && currentOutlinedHero != this)
            {
                currentOutlinedHero.DisableOutline();
            }

            EnableOutline();
            currentOutlinedHero = this;
        }

        public void EnableOutline()
        {
            if (outline != null)
            {
                outline.enabled = true;
            }
        }

        public void DisableOutline()
        {
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }
}
