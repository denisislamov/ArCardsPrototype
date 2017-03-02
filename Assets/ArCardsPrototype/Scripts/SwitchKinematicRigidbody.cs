using UnityEngine;
using System.Collections;

public class SwitchKinematicRigidbody : MonoBehaviour {
    [SerializeField] protected Rigidbody RigidbodyRef;
    [SerializeField] protected Transform parent;
    [SerializeField] protected Vector3 ForceVector;

   public void KinematicRigidbodySwitchOn()
    {
        RigidbodyRef.isKinematic = true;
    }

    public void KinematicRigidbodySwitchOff()
    {
        RigidbodyRef.isKinematic = false;
        RigidbodyRef.gameObject.transform.parent = parent;
        RigidbodyRef.AddForce(ForceVector, ForceMode.Acceleration);
    }
}
