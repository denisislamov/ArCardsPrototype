using UnityEngine;

public class LanguageDepencePlaySound : MonoBehaviour
{
    [SerializeField] private PlaySound _playSound;
    [SerializeField] private SystemLanguage[] _languages;
    private SystemLanguage _currentLanguage;
    
    public void PlaySoundByDefaultLanguage()
    {
        for (var index = 0; index < _languages.Length; index++)
        {
            if (_languages[index] != _currentLanguage)
            {
                continue;
            }
            
            _playSound.PlaySoundByIndex(index);
            return;
        }
    }
    
    public void SetLanguage(int index)
    {
        _currentLanguage = _languages[index];
    }
    
    private void Awake()
    {
        _currentLanguage = Application.systemLanguage;
        //_currentLanguage = SystemLanguage.English;
    }

    public void ResetLanguage()
    {
        _currentLanguage = Application.systemLanguage;
    }
}