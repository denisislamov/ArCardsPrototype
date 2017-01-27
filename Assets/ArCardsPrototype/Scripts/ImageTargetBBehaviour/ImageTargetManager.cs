using UnityEngine;
using System.Collections;

public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] protected CustomTrackableEventHandler[] TrackableEventHandlers;

    [Space(5)]
    [SerializeField] protected UiTransformController UiTransformControllerRef;
    [SerializeField] protected UiAnimationController UiAnimationControllerRef;

    protected void OnEnable()
    {
        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.OnTrackingFound += TrackingFound;
            trackableEventHandler.OnTrackingLost += TrackingLost;
        }
    }

    protected void OnDisable()
    {
        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.OnTrackingFound -= TrackingFound;
            trackableEventHandler.OnTrackingLost -= TrackingLost;
        }
    }

    public void TrackingFound(Transform transformRef, GameObject animatorsParent, bool isRequiredReset)
    {
        UiTransformControllerRef.TargetTransform = transformRef;
        UiAnimationControllerRef.AnimatorsParent  = animatorsParent;

        if (isRequiredReset)
        {
            UiTransformControllerRef.Reset();
            UiAnimationControllerRef.Reset();
        }
    }

    public void TrackingLost()
    {
        UiTransformControllerRef.TargetTransform = null;
        UiAnimationControllerRef.AnimatorsParent = null;
    }
}
