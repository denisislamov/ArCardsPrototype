using UnityEngine;

public class UiAnimationController : MonoBehaviour
{
    public GameObject AnimatorsParent { get; set; }
    
    [SerializeField] protected string BoolAnimationName = "Reset";

    [Space(10)]
    [Header("UI References")]
    [SerializeField] protected UnityEngine.UI.Button ResetAnimationButton;

    private void Awake()
    {
        ResetAnimationButton.onClick.AddListener(Reset);
    }

    public void Reset()
    {
        if (AnimatorsParent == null)
        {
            return;
        }
        
        var animators = AnimatorsParent.GetComponentsInChildren<Animator>();
        foreach (var animator in animators)
        {
            animator.SetTrigger(BoolAnimationName);
        }
    }
}
