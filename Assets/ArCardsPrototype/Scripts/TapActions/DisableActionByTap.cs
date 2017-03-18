using UnityEngine;
using System.Collections;

public class DisableActionByTap : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var colliders = animator.gameObject.GetComponentsInChildren<Collider>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        animator.gameObject.GetComponentInChildren<TouchFingerQuad>().gameObject.GetComponent<Renderer>().enabled = false;
    }
}
