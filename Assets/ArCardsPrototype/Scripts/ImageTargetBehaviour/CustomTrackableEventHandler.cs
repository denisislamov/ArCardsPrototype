using UnityEngine;
using Vuforia;
using System;
using System.Collections;

public class CustomTrackableEventHandler : MonoBehaviour,
                                           ITrackableEventHandler
{
    private TrackableBehaviour _trackableBehaviour;
    
    public Transform MainControllerTransform;
    public GameObject TargetAnimatorsParent;
    private PlaySound[] _playSoundRefs;
    public LanguageDepencePlaySound LanguageDepencePlaySoundValue;
    public AudioClip MusicAudioClip;
    
    [Space(10)]
    public Action OnTrackingFoundSimple;
    public Action<CustomTrackableEventHandler> OnTrackingFound;

    public Action OnTrackingLostSimple;
    public Action<CustomTrackableEventHandler> OnTrackingLost;
    
    [Space(10)]
    public bool ShowTranslationUi;
    private bool _isTrackingFound;

    public bool GetIsTrackingFound()
    {
        return _isTrackingFound;
    }
    
    [Space(10)]
    [SerializeField] protected float Delay = 5.0f;
    private float _elapsedTime = 0.0f;
    
    [HideInInspector]
    public bool IsRequiredReset;

    [Space(10)]
    public GameObject[] RandomElements;
    private void Start()
    {
        _trackableBehaviour = GetComponent<TrackableBehaviour>();
        _playSoundRefs = GetComponentsInChildren<PlaySound>();
        if (_trackableBehaviour)
        {
            _trackableBehaviour.RegisterTrackableEventHandler(this);
        }
        
        if (RandomElements.Length <= 0)
        {
            return;
        }

        for (var index = 0; index < RandomElements.Length; index++)
        {
            var re = RandomElements[index];
            re.SetActive(false);
        }
    }

    private  void Update()
    {
        if (IsRequiredReset || _isTrackingFound)
        {
            return;
        }
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime <= Delay)
        {
            return;
        }
        _elapsedTime = 0.0f;
        IsRequiredReset = true;

        if (RandomElements.Length <= 0)
        {
            return;
        }
        
        foreach (var re in RandomElements)
        {
            re.SetActive(false);
        }

        TrackingLost();
    }


    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
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

    protected virtual void TrackingFound()
    {
        if (RandomElements.Length > 0 && IsRequiredReset)
        {
            var go = RandomElements[UnityEngine.Random.Range(0, RandomElements.Length)];
            go.SetActive(true);

            MainControllerTransform = go.transform.parent;
            TargetAnimatorsParent   = go.transform.parent.gameObject;
            _playSoundRefs = go.GetComponentsInChildren<PlaySound>();
        }

        var rendererComponents = GetComponentsInChildren<Renderer>(true);

        for (var index = 0; index < rendererComponents.Length; index++)
        {
            var component = rendererComponents[index];
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
        
        var animators = GetComponentsInChildren<Animator>(true);
        for (var index = 0; index < animators.Length; index++)
        {
            var animator = animators[index];
            animator.speed = 1.0f;
        }

        if (OnTrackingFoundSimple != null)
        {
            OnTrackingFoundSimple();
        }
        
        if (OnTrackingFound != null)
        {
            OnTrackingFound(this);
            OnTrackingFound(this);
            IsRequiredReset = false;
        }

        _isTrackingFound = true;
        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " found");
    }


    protected virtual void TrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);

        for (var index = 0; index < rendererComponents.Length; index++)
        {
            var component = rendererComponents[index];
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
        
        if (OnTrackingLostSimple != null)
        {   
            OnTrackingLostSimple();
        }

        if (OnTrackingLost != null)
        {
            OnTrackingLost(this);
        }

        _isTrackingFound = false;
        
        var animators = GetComponentsInChildren<Animator>(true);
        for (var index = 0; index < animators.Length; index++)
        {
            var animator = animators[index];
            animator.speed = 0.0f;
        }
        
        Debug.Log("Trackable " + _trackableBehaviour.TrackableName + " lost");
    }

    public void ResumeSounds()
    {
        if (_playSoundRefs == null)
        {
            return;
        }
        
        foreach (var playSoundRef in _playSoundRefs)
        {
            playSoundRef.Resume();
        }
    }
    
    public void PauseSounds()
    {
        if (_playSoundRefs == null)
        {
            return;
        }
        
        foreach (var playSoundRef in _playSoundRefs)
        {
            playSoundRef.Pause();
        }
    }
}
