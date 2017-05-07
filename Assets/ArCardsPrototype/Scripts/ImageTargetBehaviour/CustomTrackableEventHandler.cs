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
    [SerializeField] protected PlaySound PlaySoundRef;

    public Action OnTrackingFoundSimple;
    public Action<Transform, GameObject, PlaySound, bool> OnTrackingFound;

    public Action OnTrackingLostSimple;
    public Action<PlaySound> OnTrackingLost;

    private bool _isTrackingFound;

    public bool GetIsTrackingFound()
    {
        return _isTrackingFound;
    }

    [SerializeField] protected float Delay = 5.0f;
    private float _elapsedTime = 0.0f;
    private bool _isRequiredReset;

    public GameObject[] RandomElements;

    protected void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }

        if (RandomElements.Length > 0)
        {
            foreach (var re in RandomElements)
            {
                re.SetActive(false);
            }
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

                if (RandomElements.Length > 0)
                {
                    foreach (var re in RandomElements)
                    {
                        re.SetActive(false);
                    }
                }
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
        if (RandomElements.Length > 0 && _isRequiredReset)
        {
            var go = RandomElements[UnityEngine.Random.Range(0, RandomElements.Length)];
            go.SetActive(true);

            MainControllerTransform = go.transform.parent;
            TargetAnimatorsParent   = go.transform.parent.gameObject;
            PlaySoundRef = go.GetComponentInChildren<PlaySound>();
        }

        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        foreach (Renderer component in rendererComponents)
        {
            if (component.gameObject.GetComponent<TouchFingerQuad>() != null)
            {
                component.material.SetFloat("_ScaleX", 3.0f);
                component.material.SetFloat("_ScaleY", 3.0f);
            }
            else
            {
                component.enabled = true;
            }
        }

        //foreach (Collider component in colliderComponents)
        //{
        //    component.enabled = true;
        //}

        if (OnTrackingFoundSimple != null)
        {
            OnTrackingFoundSimple();
        }
        
        if (OnTrackingFound != null)
        {
            OnTrackingFound(MainControllerTransform, TargetAnimatorsParent, PlaySoundRef, _isRequiredReset);
            _isRequiredReset = false;
        }

        _isTrackingFound = true;

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " found");
    }


    private void TrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        //Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

        foreach (Renderer component in rendererComponents)
        {
            if (component.gameObject.GetComponent<TouchFingerQuad>() != null)
            {
                component.material.SetFloat("_ScaleX", 0.0f);
                component.material.SetFloat("_ScaleY", 0.0f);
            }
            else
            {
                component.enabled = false;
            }
        }

        //foreach (Collider component in colliderComponents)
        //{
        //    component.enabled = false;
        //}

        if (OnTrackingLostSimple != null)
        {   
            OnTrackingLostSimple();
        }

        if (OnTrackingLost != null)
        {
            OnTrackingLost(PlaySoundRef);
        }

        _isTrackingFound = false;

        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
    }
}
