using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class TouchZoomCamera : MonoBehaviour
{
    [SerializeField] protected Camera CameraRef;

    private const float MIN_FOV = 50.0f;
    private const float MAX_FOV = 70.0f;


    protected void OnEnable()
    {
        EasyTouch.On_PinchIn += On_PinchIn;
        EasyTouch.On_PinchOut += On_PinchOut;
    }

    protected void OnDisable()
    {
        UnsubscribeEvent();
    }

    protected void OnDestroy()
    {
        UnsubscribeEvent();
    }

    private void UnsubscribeEvent()
    {
        EasyTouch.On_PinchIn  -= On_PinchIn;
        EasyTouch.On_PinchOut -= On_PinchOut;
    }

    private void On_PinchIn(Gesture gesture)
    {

        var zoom = Time.deltaTime * gesture.deltaPinch;
        CameraRef.fieldOfView += zoom;

        if (CameraRef.fieldOfView > MAX_FOV)
        {
            CameraRef.fieldOfView = MAX_FOV;
        }
    }

    private void On_PinchOut(Gesture gesture)
    {
        var zoom = Time.deltaTime * gesture.deltaPinch;
        CameraRef.fieldOfView -= zoom;

        if (CameraRef.fieldOfView < MIN_FOV)
        {
            CameraRef.fieldOfView = MIN_FOV;
        }
    }
}
