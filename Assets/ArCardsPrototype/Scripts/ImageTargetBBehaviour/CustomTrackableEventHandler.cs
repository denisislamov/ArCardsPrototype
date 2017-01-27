using UnityEngine;
using Vuforia;
using System;

// TODO - проверить новые механики
public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    private TrackableBehaviour _trackableBehaviour;

    [SerializeField] protected Transform MainControllerTransform;
    [SerializeField] protected GameObject TargetAnimatorsParent;

    public Action<Transform, GameObject, bool> OnTrackingFound;
    public Action OnTrackingLost;

    private bool _isTrackingFound;

    [SerializeField] protected float Delay = 5.0f;
    private float _elapsedTime = 0.0f;
    private bool _isRequiredReset;

    protected void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
    }

    protected void Update()
    {
        if (!_isRequiredReset && !_isTrackingFound)
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > Delay)
            {
                _elapsedTime = 0.0f;
                _isRequiredReset = true;
            }
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
            OnTrackingFound(MainControllerTransform, TargetAnimatorsParent, _isRequiredReset);
            _isRequiredReset = false;
        }

        _isTrackingFound = true;

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

        _isTrackingFound = false;

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
    }
}
