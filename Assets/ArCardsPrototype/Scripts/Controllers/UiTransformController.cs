using UnityEngine;
using System.Collections;

public class UiTransformController : MonoBehaviour
{
    [SerializeField] public Transform TargetTransform;

    // Ui Ref
    [Space(10)]
    [Header("UI References")]
    
    // Rotation Buttons
    [Space(5)]
    [SerializeField] protected float RotationFactor = 20.0f;

    // Scale Slider
    [Space(5)]
    [SerializeField] protected UnityEngine.UI.Button ZoomIn;
    [SerializeField] protected UnityEngine.UI.Button ZoomOut;

    [SerializeField] protected float MinScale = 0.32768f;
    [SerializeField] protected float MaxScale = 3.0517578125f;

    private void Init()
    {
        ZoomIn.onClick.AddListener(delegate { Scale(-0.1f); });
        ZoomOut.onClick.AddListener(delegate { Scale(0.1f); });

    }

    protected void Awake()
    {
        Init();
    }

    protected void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        if (TargetTransform != null)
        {
            TargetTransform.Rotate(ETCInput.GetAxis("Vertical") * RotationFactor * Time.deltaTime, 
                                   ETCInput.GetAxis("Horizontal") * RotationFactor * Time.deltaTime, 
                                   0.0f, Space.Self);
        }
    }

    private void Scale(float value)
    {
        if (TargetTransform != null)
        {
            TargetTransform.localScale -= new Vector3(value, value, value);
        
            if (TargetTransform.localScale.x < MinScale)
            {
                TargetTransform.localScale = new Vector3(MinScale, MinScale, MinScale);
            }

            if (TargetTransform.localScale.x > MaxScale)
            {
                TargetTransform.localScale = new Vector3(MaxScale, MaxScale, MaxScale);
            }
        }
    }
}
