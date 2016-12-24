using UnityEngine;
using System.Collections;

public class UiAnimationController : MonoBehaviour
{
    [SerializeField] protected Animator AnimatorRef;
    [SerializeField] protected string BoolAnimationName = "Crouch";

    // Ui Ref
    [Space(10)]
    [Header("UI References")]
    [SerializeField] protected UnityEngine.UI.Button ChangeAnimationButton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        ChangeAnimationButton.onClick.AddListener(delegate { ChangeAnimationButtonOnClick(); });
    }

    private void ChangeAnimationButtonOnClick()
    {
        bool value = AnimatorRef.GetBool(BoolAnimationName);
        AnimatorRef.SetBool(BoolAnimationName, !value);
    }
}
