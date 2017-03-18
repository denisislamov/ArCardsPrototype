using UnityEngine;
using System.Collections;

public class OpenLinkByButton : MonoBehaviour
{
    [SerializeField] protected UnityEngine.UI.Button Button;
    [SerializeField] protected string Url;

    protected void Awake()
    {
        Button.onClick.AddListener(OnClickButton);
    }

    private void OnClickButton()
    {
        Application.OpenURL(Url);
    }
}
