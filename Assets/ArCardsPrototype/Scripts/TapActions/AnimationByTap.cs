﻿using UnityEngine;
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

    protected void Awake()
    {
        CustomTrackableEventHandlerRef.OnTrackingFoundSimple += SubscribeEvent;
        CustomTrackableEventHandlerRef.OnTrackingLostSimple += UnsubscribeEvent;
    }

    protected void SubscribeEvent()
    {
       EasyTouch.On_SimpleTap += OnSimpleTap;
    }

    protected void OnDisable()
    {
        UnsubscribeEvent();
    }

    protected void OnDestroy()
    {
        UnsubscribeEvent();

        CustomTrackableEventHandlerRef.OnTrackingFoundSimple -= SubscribeEvent;
        CustomTrackableEventHandlerRef.OnTrackingLostSimple -= UnsubscribeEvent;
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
        if (gameObject.GetComponent<Collider>() == null)
        {
            return;
        }

        if (!_isMouseOver)
        {
            return;
        }

        if (CustomTrackableEventHandlerRef != null && !CustomTrackableEventHandlerRef.GetIsTrackingFound())
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
}
