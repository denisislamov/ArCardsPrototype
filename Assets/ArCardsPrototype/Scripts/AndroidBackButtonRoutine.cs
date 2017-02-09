using UnityEngine;
using System.Collections;

public class AndroidBackButtonRoutine : MonoBehaviour
{
    [SerializeField]
    protected UiAnimationController UiAnimationControllerRef;

    public bool _isClickOneTime = false;

    [SerializeField] protected float Delay = 2.0f;
    private float _currentDelay;

    protected void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiAnimationControllerRef.Reset();

            if (_isClickOneTime)
            {
                Debug.Log("Application quit!");
                Application.Quit();
            }
            else
            {
                _isClickOneTime = true;
            }
        }

        if (_isClickOneTime)
        {
            _currentDelay += Time.deltaTime;

            if (_currentDelay >= Delay)
            {
                _currentDelay = 0.0f;
                _isClickOneTime = false;
            }
        }
    }
}
