using UnityEngine;

public class CollisionAnimationTrigger : MonoBehaviour
{
public Animator animator;
    public string tagToCompare = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagToCompare))
        {
            animator.SetTrigger("AnimationActivator"); // replace with the actual name of the trigger parameter
        }
    }
}
