using UnityEngine;
using System.Collections;

public class PickupItem : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    private Vector3 _startScale;
    private Transform _oldParent;

    [SerializeField] protected Transform Target;
    [SerializeField] protected Transform Anchor;


    public void Start()
    {
        _startPosition = Target.localPosition;
        _startRotation = Target.localRotation;
        _startScale    = Target.localScale;

        _oldParent = Target.parent;
    }

    public void Pickup()
    {
        Target.parent = Anchor;
    }

    public void Put()
    {
        Target.parent = _oldParent;

        Target.localPosition = _startPosition;
        Target.localRotation = _startRotation;
        Target.localScale = _startScale;
    }
}
