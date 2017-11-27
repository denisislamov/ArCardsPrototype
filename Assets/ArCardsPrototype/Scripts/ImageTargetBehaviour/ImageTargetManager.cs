using UnityEngine;

public class ImageTargetManager : MonoBehaviour
{
    [SerializeField] protected CustomTrackableEventHandler[] TrackableEventHandlers;

    [Space(5)]
    [SerializeField] protected UiTransformController UiTransformControllerRef;
    [SerializeField] protected UiAnimationController UiAnimationControllerRef;

    private LanguageDepencePlaySound _languageDepencePlaySoundValue;
    
    [Space(10)]
    [SerializeField] protected MainUiController MainUiControllerRef;

    [Space(10)] 
    [SerializeField] protected AudioSource MusicSource;
    [SerializeField] protected AudioClip DefaultAudioClip;
    
    [Space(10)]
    [SerializeField] private Timer _timer;

    [Space(10)] 
    [SerializeField] private GameObject _translationUi;

    private CustomTrackableEventHandler _lasTrackableEventHandler;
    
    protected void Awake()
    {
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
    }

    private void TrackingFound(CustomTrackableEventHandler value)
    {
        _timer.StopTimer();
        _timer.gameObject.SetActive(false);
        
        UiTransformControllerRef.TargetTransform = value.MainControllerTransform;
        UiAnimationControllerRef.AnimatorsParent = value.TargetAnimatorsParent;
        _languageDepencePlaySoundValue = value.LanguageDepencePlaySoundValue;

        value.ResumeSounds();
        
        _translationUi.SetActive(value.ShowTranslationUi);

        if (value.MusicAudioClip != null)
        {
            MusicSource.clip = value.MusicAudioClip;
            MusicSource.Play();
        }

        if (!value.IsRequiredReset)
        {
            return;
        }

        UiTransformControllerRef.Reset();
        UiAnimationControllerRef.Reset();

        _lasTrackableEventHandler = value;
    }

    private void TrackingLost(CustomTrackableEventHandler value)
    {
        UiTransformControllerRef.TargetTransform = null;
        UiAnimationControllerRef.AnimatorsParent = null;

        if (_languageDepencePlaySoundValue != null)
        {
            _languageDepencePlaySoundValue.ResetLanguage();
            _languageDepencePlaySoundValue = null;
        }
        
        _translationUi.SetActive(false);

        if (MusicSource.clip != DefaultAudioClip)
        {
            MusicSource.clip = DefaultAudioClip;
            MusicSource.Play();
        }

        value.PauseSounds();

        _lasTrackableEventHandler = null;
    }

    public void PlayRequiredLanguage(int index)
    {
        if (_languageDepencePlaySoundValue == null)
        {
            return;
        }
        
        _languageDepencePlaySoundValue.SetLanguage(index);
        UiAnimationControllerRef.Reset();
    }
}
