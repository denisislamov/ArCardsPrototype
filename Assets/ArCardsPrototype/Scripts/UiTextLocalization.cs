using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UiTextLocalization : MonoBehaviour
{
    private Text _text;
    [SerializeField] private string _key;
    [SerializeField] private LocalizationData DictionaryRef;

    private void Awake()
    {
        _text = GetComponent<Text>();
        _text.text = DictionaryRef.GetElement(_key).Replace("\\n", "\n"); ;
    }
}
