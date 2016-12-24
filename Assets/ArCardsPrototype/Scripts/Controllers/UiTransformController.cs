using UnityEngine;
using System.Collections;

public class UiTransformController : MonoBehaviour
{
    [SerializeField] public Transform TargetTransform;

    // Ui Ref
    [Space(10)]
    [Header("UI References")]

    // Translation Buttons
    [SerializeField] protected UnityEngine.UI.Button XPositivePositionAxisButton;
    [SerializeField] protected UnityEngine.UI.Button XNegativePositionAxisButton;
    [SerializeField] protected UnityEngine.UI.Button ZPositivePositionAxisButton;
    [SerializeField] protected UnityEngine.UI.Button ZNegativePositionAxisButton;
    [SerializeField] protected float TranslationValue = 0.1f;
    
    // Rotation Buttons
    [Space(5)]
    [SerializeField] protected UnityEngine.UI.Button XPositiveRotationAxisButton;
    [SerializeField] protected UnityEngine.UI.Button XNegativeRotationAxisButton;
    [SerializeField] protected UnityEngine.UI.Button YPositiveRotationAxisButton;
    [SerializeField] protected UnityEngine.UI.Button YNegativeRotationAxisButton;
    [SerializeField] protected float RotationValue = 10.0f;

    public UnityEngine.UI.Button GetXPositiveRotationAxisButton() { return XPositiveRotationAxisButton; }
    public UnityEngine.UI.Button GetXNegativeRotationAxisButton() { return XNegativeRotationAxisButton; }
    public UnityEngine.UI.Button GetYPositiveRotationAxisButton() { return YPositiveRotationAxisButton; }
    public UnityEngine.UI.Button GetYNegativeRotationAxisButton() { return YNegativeRotationAxisButton; }
    
    public float GetRotationValue() { return RotationValue; }

    // Scale Slider
    [Space(5)]
    [SerializeField] protected UnityEngine.UI.Slider ScaleSlider;

    public UnityEngine.UI.Slider GetScaleSliderRef() {return ScaleSlider; }

    private void Init()
    {
        XPositivePositionAxisButton.onClick.AddListener(delegate { Move(AxisType.X, TranslationValue); });
        XNegativePositionAxisButton.onClick.AddListener(delegate { Move(AxisType.X, -TranslationValue); });
        ZPositivePositionAxisButton.onClick.AddListener(delegate { Move(AxisType.Z, TranslationValue); });
        ZNegativePositionAxisButton.onClick.AddListener(delegate { Move(AxisType.Z, -TranslationValue); });

        XPositiveRotationAxisButton.onClick.AddListener(delegate { Rotate(AxisType.X, RotationValue); });
        XNegativeRotationAxisButton.onClick.AddListener(delegate { Rotate(AxisType.X, -RotationValue); });
        YPositiveRotationAxisButton.onClick.AddListener(delegate { Rotate(AxisType.Y, RotationValue); });
        YNegativeRotationAxisButton.onClick.AddListener(delegate { Rotate(AxisType.Y, -RotationValue); });

        ScaleSlider.onValueChanged.AddListener(delegate { Scale(ScaleSlider.value); });
    }

    private void Awake()
    {
        Init();
    }

    public enum AxisType
    {
        X = 0,
        Y = 1,
        Z = 2
    }

    private void Move(AxisType axis, float value)
    {
        if (TargetTransform != null)
        {
            switch (axis)
            {
                case AxisType.X:

                    TargetTransform.Translate(value, 0.0f, 0.0f, Space.Self);
                    break;
                case AxisType.Y:
                    TargetTransform.Translate(0.0f, value, 0.0f, Space.Self);
                    break;
                case AxisType.Z:
                    TargetTransform.Translate(0.0f, 0.0f, value, Space.Self);
                    break;
            }
        }
    }

    private void Rotate(AxisType axis, float value)
    {
        if (TargetTransform != null)
        {
            switch (axis)
            {
                case AxisType.X:
                    TargetTransform.Rotate(value, 0.0f, 0.0f, Space.Self);
                    break;
                case AxisType.Y:
                    TargetTransform.Rotate(0.0f, value, 0.0f, Space.Self);
                    break;
                case AxisType.Z:
                    TargetTransform.Rotate(0.0f, 0.0f, value, Space.Self);
                    break;
            }
        }
    }

    private void Scale(float value)
    {
        if (TargetTransform != null)
        {
            TargetTransform.localScale = new Vector3(value, value, value);
        }
    }

    public void SetScaleSlider()
    {
        ScaleSlider.value = TargetTransform.localScale.x;
    }
}
