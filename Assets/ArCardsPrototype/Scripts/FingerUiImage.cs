using UnityEngine;

public class FingerUiImage : MonoBehaviour
{
    [SerializeField] protected Transform Target;
    [SerializeField] protected Camera Camera;

    [SerializeField] protected RectTransform CurrentRectTransform;
    [SerializeField] protected RectTransform CanvasRect;

    protected void Update()
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera, Target.position);
        CurrentRectTransform.anchoredPosition = screenPoint - CanvasRect.sizeDelta / 2.0f;
    }
}
