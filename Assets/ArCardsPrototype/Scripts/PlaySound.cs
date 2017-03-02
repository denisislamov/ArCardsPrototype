﻿using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] protected AudioClip[] AudioClips;

    [Space(10)]
    [SerializeField] protected AudioSource AudioSourceRef;
    [SerializeField] protected AudioSource BackgroundMusicAudioSourceRef;

    private float _defaultBackgroundVolume;

    private bool _isPaused;

    protected void Awake()
    {
        if (BackgroundMusicAudioSourceRef != null)
        {
            _defaultBackgroundVolume = BackgroundMusicAudioSourceRef.volume;
        }
    }

    public void PlaySoundByIndex(int index)
    {
        AudioSourceRef.clip = AudioClips[index];
        AudioSourceRef.Play();

        if (BackgroundMusicAudioSourceRef != null)
        {
            BackgroundMusicAudioSourceRef.volume = _defaultBackgroundVolume/2;
        }

        _isPaused = false;
    }

    public void StopSound()
    {
        AudioSourceRef.Stop();

        if (BackgroundMusicAudioSourceRef != null)
        {
            BackgroundMusicAudioSourceRef.volume = _defaultBackgroundVolume;
        }

        _isPaused = false;
    }

    public void Resume()
    {
        if (!_isPaused)
        {
            return;
        }

        AudioSourceRef.UnPause();

        if (BackgroundMusicAudioSourceRef != null)
        {
            BackgroundMusicAudioSourceRef.volume = _defaultBackgroundVolume/2;
        }

        _isPaused = false;

    }

    public void Pause()
    {
        AudioSourceRef.Pause();

        if (BackgroundMusicAudioSourceRef != null)
        {
            BackgroundMusicAudioSourceRef.volume = _defaultBackgroundVolume;
        }

        _isPaused = true;
    }
}