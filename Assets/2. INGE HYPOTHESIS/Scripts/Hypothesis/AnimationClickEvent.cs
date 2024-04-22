using UnityEngine;
using UnityEngine.Video;

namespace COSE.Hypothesis
{
    public class AnimationClickEvent : MonoBehaviour
    {
        public static event System.Action<VideoClip> OnAnimationClicked;
        public static AnimationClickEvent currentOutlinedAnimation;
        public VideoClip videoClip;
        [SerializeField] private int animationId;

        private Outline outline;

        private void Start()
        {
            outline = GetComponent<Outline>() ?? GetComponentInChildren<Outline>();
            outline.enabled = false;
        }

        void OnMouseDown()
        {
            OnAnimationClicked?.Invoke(videoClip);
            Debug.Log($"Animation {animationId} was clicked.");

            if (currentOutlinedAnimation != null && currentOutlinedAnimation != this)
            {
                currentOutlinedAnimation.DisableOutline();
            }

            EnableOutline();
            currentOutlinedAnimation = this;
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
