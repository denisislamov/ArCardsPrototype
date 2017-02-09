using UnityEngine;
using System.Collections;

public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] protected CustomTrackableEventHandler[] TrackableEventHandlers;

    [Space(5)]
    [SerializeField] protected UiTransformController UiTransformControllerRef;
    [SerializeField] protected UiAnimationController UiAnimationControllerRef;
    [SerializeField] protected AudioController AudioControllerRef;

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

    public void TrackingFound(Transform transformRef, GameObject animatorsParent, AudioSource audioSource, bool isRequiredReset)
    {
        UiTransformControllerRef.TargetTransform = transformRef;
        UiAnimationControllerRef.AnimatorsParent = animatorsParent;

        AudioControllerRef.AudioSourceRef        = audioSource;
        AudioControllerRef.Resume();

        if (!isRequiredReset)
        {
            return;
        }

        UiTransformControllerRef.Reset();
        UiAnimationControllerRef.Reset();

        AudioControllerRef.Play();
    }

    public void TrackingLost()
    {
        UiTransformControllerRef.TargetTransform = null;
        UiAnimationControllerRef.AnimatorsParent = null;

        AudioControllerRef.Pause();
    }
}
