using UnityEngine;
using System.Collections;

public class SwichKinematicRigidbody : MonoBehaviour
{
    [SerializeField] protected Rigidbody RigidbodyRef;
    [SerializeField] protected Transform Parent;
    [SerializeField] protected Vector3 ForceVector;

    public void KinematicRigidbodySwitchOn()
    {
        RigidbodyRef.isKinematic = true;
    }

    public void KinematicRigidbodySwitchOff()
    {
        RigidbodyRef.isKinematic = false;
        RigidbodyRef.gameObject.transform.parent = Parent;
        RigidbodyRef.AddForce(ForceVector, ForceMode.Acceleration);
    }
}
