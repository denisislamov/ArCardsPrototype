using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioSourceRef;

    [SerializeField] protected UnityEngine.UI.Button ResetButton;

    private bool _isPaused = false;

    protected void Awake()
    {
        ResetButton.onClick.AddListener(Play);
    }

    public void Play()
    {
        if (AudioSourceRef == null)
        {
            return;
        }

        AudioSourceRef.Play();
        _isPaused = false;
    }

    public void Stop()
    {
        if (AudioSourceRef == null)
        {
            return;
        }

        AudioSourceRef.Stop();
        _isPaused = false;
    }

    public void Pause()
    {
        if (AudioSourceRef == null)
        {
            return;
        }

        AudioSourceRef.Pause();
        _isPaused = true;
    }

    public void Resume()
    {
        if (AudioSourceRef == null)
        {
            return;
        }

        if (_isPaused)
        {
            AudioSourceRef.UnPause();
            _isPaused = false;
        }
        else
        {
            Play();
        }
    }


}
