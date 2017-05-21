using UnityEngine;
using System.Collections;

public class ActionByTime : MonoBehaviour
{
    [SerializeField] private float _time = 4.0f;
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        StartCoroutine(Counter());
    }

    private void OnDisable()
    {
        StopCoroutine(Counter());
    }

    private IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(_time / 2.0f);
            _animator.SetTrigger("Action");
            yield return new WaitForSecondsRealtime(_time / 2.0f);
        }
    }
}
