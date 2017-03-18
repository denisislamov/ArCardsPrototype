using UnityEngine;
using System.Collections;

public class SwichCollidersAndTouchQuad : MonoBehaviour
{
    private Collider[] _colliders;
    private Renderer _touchFingerQuadRenderer;

    protected void Awake()
    {
        _colliders = gameObject.GetComponentsInChildren<Collider>();
        _touchFingerQuadRenderer = gameObject.GetComponentInChildren<TouchFingerQuad>().gameObject.GetComponent<Renderer>();
    }

    public void SwichCollidersAndTouchQuadOn()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = true;
        }

        _touchFingerQuadRenderer.enabled = true;
    }

    public void SwichCollidersAndTouchQuadOff()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }

        _touchFingerQuadRenderer.enabled = false;
    }
}
