using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData  ", menuName = "Data/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [System.Serializable]
    public class Element
    {
        public SystemLanguage Language;
        public string Data;
    }

    [System.Serializable]
    public class ElementsWithKey
    {
        public string Key;
        public Element[] Elements;
    }

    public ElementsWithKey[] Dictionary;

    [Space(10)]
    public SystemLanguage LanguageForTest = SystemLanguage.Russian;

    public string GetElement(string key)
    {
        foreach (var elementWithKey in Dictionary)
        {
            if (elementWithKey.Key == key)
            {
                foreach (var element in elementWithKey.Elements)
                {
#if UNITY_EDITOR
                    if (element.Language == LanguageForTest)
                    {
                        return element.Data;
                    }
#else
                    if (element.Language == Application.systemLanguage)
                    {
                        return element.Data;
                    }
#endif
                }
            }
        }

        return "";
    }
}
