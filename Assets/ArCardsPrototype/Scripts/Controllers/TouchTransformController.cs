using UnityEngine;
using HedgehogTeam.EasyTouch;

public class TouchTransformController : MonoBehaviour
{
    [SerializeField] protected UiTransformController UiTransformControllerRef;
    [SerializeField] protected float ScaleFactor = 0.2f;

    protected void OnEnable()
    {
        EasyTouch.On_PinchIn += OnPinchIn;
        EasyTouch.On_PinchOut += OnPinchOut;
        EasyTouch.On_Drag2Fingers += OnDragTwoFingers;
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
        EasyTouch.On_PinchIn -= OnPinchIn;
        EasyTouch.On_PinchOut -= OnPinchOut;
        EasyTouch.On_Drag2Fingers -= OnDragTwoFingers;
    }

    private void OnPinchIn(Gesture gesture)
    {
        float zoom = Time.deltaTime * gesture.deltaPinch * ScaleFactor;

        UiTransformControllerRef.GetScaleSliderRef().value -= zoom;
        UiTransformControllerRef.GetScaleSliderRef().onValueChanged.Invoke(UiTransformControllerRef.GetScaleSliderRef().value);
    }


    private void OnPinchOut(Gesture gesture)
    {
        float zoom = Time.deltaTime * gesture.deltaPinch * ScaleFactor;

        UiTransformControllerRef.GetScaleSliderRef().value += zoom;
        UiTransformControllerRef.GetScaleSliderRef().onValueChanged.Invoke(UiTransformControllerRef.GetScaleSliderRef().value);
    }

    private void OnDragTwoFingers(Gesture gesture)
    {
        Debug.Log(gesture.swipe.ToString());

        switch (gesture.swipe)
        {
            case EasyTouch.SwipeDirection.Up:
                UiTransformControllerRef.GetXNegativeRotationAxisButton().onClick.Invoke();
                break;
            case EasyTouch.SwipeDirection.Down:
                UiTransformControllerRef.GetXPositiveRotationAxisButton().onClick.Invoke();
                break;
            case EasyTouch.SwipeDirection.Left:
                UiTransformControllerRef.GetYPositiveRotationAxisButton().onClick.Invoke();
                break;
            case EasyTouch.SwipeDirection.Right:
                UiTransformControllerRef.GetYNegativeRotationAxisButton().onClick.Invoke();
                break;
        }
    }
}
