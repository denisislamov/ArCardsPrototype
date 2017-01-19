using UnityEngine;
using Vuforia;
using System;

public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    private TrackableBehaviour _trackableBehaviour;

    [SerializeField] protected Transform MainControllerTransform;

    public Action<Transform> OnTrackingFound;
    public Action OnTrackingLost;

    void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED  ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            TrackingFound();
        }
        else
        {
            TrackingLost();
        }
    }

    private void TrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }


        if (OnTrackingFound != null)
        {
            OnTrackingFound(MainControllerTransform);
        }

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " found");
    }


    private void TrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        if (OnTrackingLost != null)
        {
            OnTrackingLost();
        }

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
    }
}
