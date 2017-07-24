using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] private float _time = 20.0f;
    [SerializeField] private UnityEvent _onTimeOver = new UnityEvent();
    private Coroutine _coroutine;
    
    public void StartTimer()
    {
        if (_coroutine != null || !gameObject.activeSelf)
        {
            return;
        }
        
        _coroutine = StartCoroutine(TimerAsync());
    }

    public void StopTimer()
    {
        if (_coroutine == null)
        {
            return;
        }
        
        StopCoroutine(TimerAsync());
        _coroutine = null;
    }
    
    private IEnumerator TimerAsync()
    {
        yield return new WaitForSecondsRealtime(_time);
        _onTimeOver.Invoke();
        _coroutine = null;
    }
}
