using UnityEngine;
using HedgehogTeam.EasyTouch;

public class AnimationByTap : MonoBehaviour
{
    [SerializeField] protected Animator[] AnimatorsRef;
    [SerializeField] protected string AnimationTriggerValueName;
    [SerializeField] protected CustomTrackableEventHandler CustomTrackableEventHandlerRef;

    private bool _isMouseOver;

    protected void OnEnable()
    {
        Debug.Log(gameObject.name + " OnEnable()");
        EasyTouch.On_SimpleTap += OnSimpleTap;
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
        EasyTouch.On_SimpleTap -= OnSimpleTap;
    }

    protected void OnMouseOver()
    {
        _isMouseOver = true;
    }

    protected void OnMouseExit()
    {
        _isMouseOver = false;
    }

    private void OnSimpleTap(Gesture gesture)
    {
        Debug.Log(gameObject.name + " AnimationByTap.OnSimpleTap ()");
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
}
