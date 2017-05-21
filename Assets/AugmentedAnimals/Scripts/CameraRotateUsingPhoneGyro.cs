using System;
using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class CameraRotateUsingPhoneGyro : MonoBehaviour
{
    private Quaternion _initRotation = Quaternion.identity;
    private Vector3 _initPosition;

    [SerializeField] protected GyzoGizmo GyzoGizmoRef;

    private float _rotationOffsetX;
    [SerializeField] private float _rotationOffsetXSpeed = 2.0f;
    [SerializeField] private float _manDistance = 10.0f;

    [SerializeField] private float _zoomFactor = 1.0f;

    protected void Awake()
    {
        Input.gyro.enabled = true;
        _initRotation = gameObject.transform.rotation;
        _initPosition = gameObject.transform.position;
    }

    protected void OnEnable()
    {
        gameObject.transform.rotation = _initRotation;
        gameObject.transform.position = _initPosition;

        transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, transform.eulerAngles.y,
            transform.eulerAngles.z);

        EasyTouch.On_TouchStart2Fingers += OnTouchStart2Fingers;

        EasyTouch.On_PinchIn  += OnPinchIn;
        EasyTouch.On_PinchOut += OnPinchOut;
        EasyTouch.On_PinchEnd += OnPinchEnd;

        EasyTouch.On_Drag2Fingers += OnDrag2Fingers;

        _rotationOffsetX = 0.0f;
    }

    protected void Update()
    {
        gameObject.transform.Rotate(-Input.gyro.rotationRateUnbiased.x, 
                                    -Input.gyro.rotationRateUnbiased.y,
                                    Input.gyro.rotationRateUnbiased.z);
        transform.eulerAngles = new Vector3(GyzoGizmoRef.transform.eulerAngles.x + _rotationOffsetX,
                                            transform.eulerAngles.y,
                                            transform.eulerAngles.z);

        if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved)
        {
            var touchDeltaPosition = Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition;
            _rotationOffsetX += touchDeltaPosition.y * Time.deltaTime * _rotationOffsetXSpeed;
        }
    }

    private void OnTouchStart2Fingers(Gesture gesture)
    {
        EasyTouch.SetEnableTwist(false);
        EasyTouch.SetEnablePinch(true);
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
        EasyTouch.On_TouchStart2Fingers -= OnTouchStart2Fingers;
        EasyTouch.On_PinchIn -= OnPinchIn;
        EasyTouch.On_PinchOut -= OnPinchOut;
        EasyTouch.On_PinchEnd -= OnPinchEnd;
        EasyTouch.On_Drag2Fingers -= OnDrag2Fingers;
    }

    private void OnPinchIn(Gesture gesture)
    {
        Vector3 position = transform.position;
        transform.Translate(transform.right * gesture.deltaPinch * Time.deltaTime * _zoomFactor);

        if ((transform.position - _initPosition).sqrMagnitude > _manDistance)
        {
            transform.position = position;
        }
    }

    private void OnPinchOut(Gesture gesture)
    {
        Vector3 position = transform.position;
        transform.Translate(-transform.right * gesture.deltaPinch * Time.deltaTime * _zoomFactor);

        if ((transform.position - _initPosition).sqrMagnitude > _manDistance)
        {
            transform.position = position;
        }
    }

    private void OnPinchEnd(Gesture gesture)
    {
        EasyTouch.SetEnableTwist(true);
    }

    private void OnDrag2Fingers(Gesture gesture)
    {
        if (Math.Abs(gesture.GetSwipeOrDragAngle() - 90.0f) < 0.1f)
        {
            _rotationOffsetX += gesture.deltaPosition.y * Time.deltaTime * _rotationOffsetXSpeed;
        }
        else if (Math.Abs(gesture.GetSwipeOrDragAngle() + 90.0f) < 0.1f)
        {
            _rotationOffsetX += gesture.deltaPosition.y * Time.deltaTime * _rotationOffsetXSpeed;
        }
    }
}
