using UnityEngine;
using System.Collections;

public class SpawnParticle : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem ParticleSystemRef;

    public void Play()
    {
        ParticleSystemRef.gameObject.SetActive(true);
    }

    public void Stop()
    {
        ParticleSystemRef.gameObject.SetActive(false);
    }
}
