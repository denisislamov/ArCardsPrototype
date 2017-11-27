using UnityEngine;
using System.Collections;

public class RestartAudioSource : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public void StopBackgroundMusic()
    {
        _audioSource.Stop();
    }

    public void StartBackgroundMusic()
    {
        _audioSource.Play();
    }
}
