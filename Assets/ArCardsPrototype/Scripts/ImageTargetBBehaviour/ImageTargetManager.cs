using UnityEngine;
using System.Collections;

public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] protected TrackableEventHandler[] TrackableEventHandlers;
    [SerializeField] protected UiTransformController UiTransformController;

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

    public void TrackingFound(Transform value)
    {
        UiTransformController.TargetTransform = value;
    }

    public void TrackingLost()
    {
        UiTransformController.TargetTransform = null;
    }
}
