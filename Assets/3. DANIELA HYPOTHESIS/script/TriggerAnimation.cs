using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public GameObject cube;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Animator animator = cube.GetComponent<Animator>();
            animator.SetTrigger("roomTrigger");
        }
    }
}
