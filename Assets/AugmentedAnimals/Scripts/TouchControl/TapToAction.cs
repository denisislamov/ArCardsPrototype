using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class TapToAction : MonoBehaviour
{
    [SerializeField] protected Animator AnimatorRef;

    protected void OnEnable()
    {
        EasyTouch.On_DoubleTap += On_DoubleTap;
    }

    protected void OnDisable()
    {
        UnsubscribeEvent();
    }

    protected void OnDestroy()
    {
        UnsubscribeEvent();
    }

    protected void UnsubscribeEvent()
    {
        EasyTouch.On_DoubleTap -= On_DoubleTap;
    }

    private bool _isMouseOverObject = false;

    protected void OnMouseEnter()
    {
        _isMouseOverObject = true;
    }

    protected void OnMouseExit()
    {
        _isMouseOverObject = false;
    }

    private void On_DoubleTap(Gesture gesture)
    {
        if (_isMouseOverObject)
        {
            AnimatorRef.SetTrigger("Action");
        }
    }
}
