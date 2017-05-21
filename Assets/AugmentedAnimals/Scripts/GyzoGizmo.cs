using UnityEngine;

public class GyzoGizmo : MonoBehaviour
{
    protected void Start()
    {
        Input.gyro.enabled = true;
    }

    protected void Update()
    {
        transform.rotation = Quaternion.Euler(90.0f, 0.0f, 0.0f) *
                             new Quaternion(Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);
    }
}
