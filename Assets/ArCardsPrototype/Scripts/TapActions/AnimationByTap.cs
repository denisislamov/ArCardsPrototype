using UnityEngine;
using HedgehogTeam.EasyTouch;

public class AnimationByTap : MonoBehaviour
{
    [SerializeField] protected Animator[] AnimatorsRef;
    [SerializeField] protected string AnimationTriggerValueName;
    [SerializeField] protected CustomTrackableEventHandler CustomTrackableEventHandlerRef;

    private bool _isMouseOver;

    [Space(10)]
    [SerializeField] protected bool IsRandomAnimationIndex;
    [SerializeField] protected int MinIndex = 1;
    [SerializeField] protected int MaxIndex = 1;

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

        if (CustomTrackableEventHandlerRef != null && !CustomTrackableEventHandlerRef.GetIsTrackingFound())
        {
            return;
        }

        foreach (var animatorRef in AnimatorsRef)
        {
            if (!IsRandomAnimationIndex)
            {
                animatorRef.SetTrigger(AnimationTriggerValueName);
            }
            else
            {
                var suffix = "_" + Random.Range(MinIndex, MaxIndex + 1);
                animatorRef.SetTrigger(AnimationTriggerValueName + suffix);
            }
        }
    }
}
