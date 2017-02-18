using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class AnimationBySwipe : MonoBehaviour
{
    [SerializeField] protected Animator[] AnimatorsRef;
    [SerializeField] protected string AnimationTriggerValueName;
    [SerializeField] protected CustomTrackableEventHandler CustomTrackableEventHandlerRef;

    private bool _isMouseOver = true;

    protected void OnEnable()
    {
        Debug.Log(gameObject.name + " OnEnable()");
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
        Debug.Log(gameObject.name + " AnimationByTap.OnSwipeEnd ()");
        if (!_isMouseOver)
        {
            return;
        }

        if (!CustomTrackableEventHandlerRef.GetIsTrackingFound())
        {
            return;
        }

        foreach (var animatorRef in AnimatorsRef)
        {
            animatorRef.SetTrigger(AnimationTriggerValueName);
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
