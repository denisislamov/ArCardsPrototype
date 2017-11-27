using UnityEngine;
using System.Collections;

public class LanguageDepenceMusic : MonoBehaviour
{
	[System.Serializable]
	private class MusicWithLanguage
	{
		public SystemLanguage Language;
		public AudioClip Music;
	}
	
	[SerializeField] private MusicWithLanguage[] _musicWithLanguages;
	[SerializeField] private CustomTrackableEventHandler _customTrackableEventHandler;
	private SystemLanguage _currentLanguage;
	
	public void SwitchMusicToDefaultLanguage()
	{
		for (var index = 0; index < _musicWithLanguages.Length; index++)
		{
			var musicWithLanguage = _musicWithLanguages[index];
			if (musicWithLanguage.Language != _currentLanguage) continue;

			_customTrackableEventHandler.MusicAudioClip = musicWithLanguage.Music;

			return;
		}
	}
	
	private void Awake()
	{
		_currentLanguage = Application.systemLanguage;
		//_currentLanguage = SystemLanguage.English;
		SwitchMusicToDefaultLanguage();
	}

	public void ResetLanguage()
	{
		_currentLanguage = Application.systemLanguage;
	}
}
