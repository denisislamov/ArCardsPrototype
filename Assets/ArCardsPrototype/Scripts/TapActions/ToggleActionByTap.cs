using UnityEngine;
using System.Collections;

public class ToggleActionByTap : StateMachineBehaviour
{
    public bool IsRevert;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        var colliders = animator.gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = IsRevert;
        }

        animator.gameObject.GetComponentInChildren<TouchFingerQuad>().gameObject.GetComponent<Renderer>().enabled = IsRevert;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var colliders = animator.gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = !IsRevert;
        }

        animator.gameObject.GetComponentInChildren<TouchFingerQuad>().gameObject.GetComponent<Renderer>().enabled = !IsRevert;
    }
}
