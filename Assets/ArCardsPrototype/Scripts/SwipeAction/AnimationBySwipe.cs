using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class AnimationBySwipe : MonoBehaviour
{
    [SerializeField] protected Animator[] AnimatorsRef;
    [SerializeField] protected string AnimationTriggerValueName;
    [SerializeField] protected CustomTrackableEventHandler CustomTrackableEventHandlerRef;

    private bool _isMouseOver = true;

    [Space(10)]
    [SerializeField] protected bool IsRandomAnimationIndex;
    [SerializeField] protected int MinIndex = 1;
    [SerializeField] protected int MaxIndex = 1;


    protected void OnEnable()
    {
        EasyTouch.On_SwipeEnd += OnSwipeEnd;
    }

    protected void OnDisable()
    {
        UnsubscribeEvent();
    }

    protected void OnDestroy()
    {
        UnsubscribeEvent();
    }

    protected void UnsubscribeEvent()
    {
        EasyTouch.On_SwipeEnd -= OnSwipeEnd;
    }

    private void OnSwipeEnd(Gesture gesture)
    {
        if (!_isMouseOver)
        {
            return;
        }

        if (!CustomTrackableEventHandlerRef.GetIsTrackingFound())
        {
            return;
        }

        var suffix = "_" + Random.Range(MinIndex, MaxIndex + 1);
        foreach (var animatorRef in AnimatorsRef)
        {
            if (!IsRandomAnimationIndex)
            {
                animatorRef.SetTrigger(AnimationTriggerValueName);
            }
            else
            {
                animatorRef.SetTrigger(AnimationTriggerValueName + suffix);
            }
        }
    }

    protected void OnMouseOver()
    {
        _isMouseOver = true;
    }

    protected void OnMouseExit()
    {
        _isMouseOver = false;
    }
}
