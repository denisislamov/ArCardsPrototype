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

    public void TrackingFound(Transform transformRef, GameObject animatorsParent, PlaySound playSoundRef, bool isRequiredReset)
    {
        UiTransformControllerRef.TargetTransform = transformRef;
        UiAnimationControllerRef.AnimatorsParent = animatorsParent;

        playSoundRef.Resume();

        if (!isRequiredReset)
        {
            return;
        }

        UiTransformControllerRef.Reset();
        UiAnimationControllerRef.Reset();
    }

    public void TrackingLost(PlaySound playSoundRef)
    {
        UiTransformControllerRef.TargetTransform = null;
        UiAnimationControllerRef.AnimatorsParent = null;

        playSoundRef.Pause();
    }
}
