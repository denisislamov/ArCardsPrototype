using UnityEngine;
using System.Collections;

public class AndroidBackButtonRoutine : MonoBehaviour
{
    [SerializeField]
    protected UiAnimationController UiAnimationControllerRef;

    protected void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UiAnimationControllerRef.Reset();
        }
    }
}
