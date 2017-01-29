using UnityEngine;
using System.Collections;

public class AnimationByTap : MonoBehaviour
{
    [SerializeField] protected Animator[] AnimatorsRef;
    [SerializeField] protected string AnimationTriggerValueName;
    [SerializeField] protected CustomTrackableEventHandler CustomTrackableEventHandlerRef;

    protected void OnMouseDown()
    {
        if (CustomTrackableEventHandlerRef.GetIsTrackingFound())
        {
            Debug.Log("OnMouseDown");
            foreach (var animatorRef in AnimatorsRef)
            {
                animatorRef.SetTrigger(AnimationTriggerValueName);
            }
        }
    }
}
