using UnityEngine;
using System.Collections;

public class MainUiController : MonoBehaviour
{
    [SerializeField] protected GameObject[] UIElements;
    [SerializeField] protected UnityEngine.UI.Button[] MoveNextButton;

    private int _currentScreen = 0;

    private int _firstIndexToSkip = 3;
    private int _lastIndexToSkip  = 5;
    private int _isFirstTimeRun   = 1;

    protected void Awake()
    {
        _isFirstTimeRun = PlayerPrefs.GetInt("IsFirstTimeRun", 1);
        PlayerPrefs.SetInt("IsFirstTimeRun", 0);

        foreach (var button in MoveNextButton)
        {
            button.onClick.AddListener(delegate { GoToNextScreen(); });
        }

        if (UIElements.Length <= _lastIndexToSkip)
        {
            Debug.LogErrorFormat("Array UIElements has {0} elements and _lastIndexToSkip has {1}", UIElements.Length, _lastIndexToSkip);
        }
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_currentScreen != UIElements.Length - 1)
            {
                GoToPrevScreen();
            }
        }
    }

    private void GoToNextScreen()
    {
        UIElements[_currentScreen].SetActive(false);
        _currentScreen++;

        if (_isFirstTimeRun == 0)
        {
            if (_currentScreen == _firstIndexToSkip)
            {
                _currentScreen = _lastIndexToSkip + 1;
            }
        }

        if (_currentScreen < UIElements.Length)
        {
            UIElements[_currentScreen].SetActive(true);
        }
    }

    private void GoToPrevScreen()
    {
        if (_currentScreen == 0)
        {
            return;
        }

        if (_currentScreen < UIElements.Length)
        {
            UIElements[_currentScreen].SetActive(false);
        }

        _currentScreen--;

        if (_isFirstTimeRun == 0)
        {
            if (_currentScreen == _lastIndexToSkip)
            {
                _currentScreen = _firstIndexToSkip - 1;
            }
        }

        UIElements[_currentScreen].SetActive(true);
    }

    [ContextMenu("Reset IsFirstTimeRun from PlayerPrefs")]
    public void ResetIsFirstTimeRunInPlayerPrefs()
    {
        PlayerPrefs.SetInt("IsFirstTimeRun", 1);
    }
}
