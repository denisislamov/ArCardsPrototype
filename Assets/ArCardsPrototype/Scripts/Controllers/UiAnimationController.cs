using UnityEngine;
using System.Collections;

public class UiAnimationController : MonoBehaviour
{
    public GameObject AnimatorsParent { get; set; }
    [SerializeField] protected string BoolAnimationName = "Reset";

    // Ui Ref
    [Space(10)]
    [Header("UI References")]
    [SerializeField] protected UnityEngine.UI.Button ResetAnimationButton;

    private void Awake()
    {
        ResetAnimationButton.onClick.AddListener(delegate { Reset(); });
    }

    public void Reset()
    {
        if (AnimatorsParent != null)
        {
            var animators = AnimatorsParent.GetComponentsInChildren<Animator>();

            foreach (var animator in animators)
            {
                animator.SetTrigger(BoolAnimationName);
            }
        }
    }
}
