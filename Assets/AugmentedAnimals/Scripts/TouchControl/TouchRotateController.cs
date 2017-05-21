using UnityEngine;
using UnityEngine.EventSystems;

public class TouchRotateController : MonoBehaviour
{
    [SerializeField] protected float Speed = 1.0f;
    [SerializeField] protected Transform Target;

    private Quaternion _initRotation;

    protected void Awake()
    {
        _initRotation = transform.rotation;
    }

    protected void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            int id = touch.fingerId;
            if (EventSystem.current.IsPointerOverGameObject(id))
            {
                return;
            }
        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            Target.Rotate(0.0f, Input.GetAxis("Mouse X") * Speed, 0.0f);
        }
#endif

        if (Input.touchCount != 1 || Input.GetTouch(0).phase != TouchPhase.Moved)
        {
            return;
        }

        var touchDeltaPosition = Input.GetTouch(0).deltaPosition;
        Target.Rotate(0.0f, touchDeltaPosition.x * Speed, 0.0f);
    }

    public void DropRotation()
    {
        transform.rotation = _initRotation;
    }
}
