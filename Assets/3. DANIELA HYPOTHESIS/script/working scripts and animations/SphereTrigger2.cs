using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTrigger2 : MonoBehaviour
{
    public string playerTag;
    public Animator animator1;
    public Animator animator2;
    public AnimationClip animation1;
    public AnimationClip animation2;

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag) && !hasTriggered)
        {
            hasTriggered = true;
            animator1.SetTrigger("roomTriggerObject1");
            animator2.SetTrigger("roomTriggerActive1");
        }
    }
}