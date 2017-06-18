using UnityEngine;
using System.Collections;

// TODO - test this
public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] protected CustomTrackableEventHandler[] TrackableEventHandlers;

    [Space(5)]
    [SerializeField] protected UiTransformController UiTransformControllerRef;
    [SerializeField] protected UiAnimationController UiAnimationControllerRef;

    [Space(10)]
    [SerializeField] protected MainUiController MainUiControllerRef;

    [SerializeField] private Timer _timer;
    
    public void TurnOnAllTrackableEventHandlers()
    {
        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.gameObject.SetActive(true);
        }
    }

    public void TurnOffAllTrackableEventHandlers()
    {
        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.gameObject.SetActive(false);
        }
    }

    protected void Awake()
    {
        MainUiControllerRef.OnHideMenu += TurnOffAllTrackableEventHandlers;
        MainUiControllerRef.OnShowMenu += TurnOnAllTrackableEventHandlers;

        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.OnTrackingFound += TrackingFound;
            trackableEventHandler.OnTrackingLost += TrackingLost;
        }
    }

    protected void OnDestroy()
    {
        foreach (var trackableEventHandler in TrackableEventHandlers)
        {
            trackableEventHandler.OnTrackingFound -= TrackingFound;
            trackableEventHandler.OnTrackingLost -= TrackingLost;
        }

        MainUiControllerRef.OnHideMenu -= TurnOffAllTrackableEventHandlers;
        MainUiControllerRef.OnShowMenu -= TurnOnAllTrackableEventHandlers;
    }

    public void TrackingFound(Transform transformRef, GameObject animatorsParent, PlaySound playSoundRef, bool isRequiredReset)
    {
        _timer.StopTimer();
        _timer.gameObject.SetActive(false);
        
        UiTransformControllerRef.TargetTransform = transformRef;
        UiAnimationControllerRef.AnimatorsParent = animatorsParent;

        if (playSoundRef != null)
        {
            playSoundRef.Resume();
        }

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

        if (playSoundRef != null)
        {
            playSoundRef.Pause();
        }
    }
}
