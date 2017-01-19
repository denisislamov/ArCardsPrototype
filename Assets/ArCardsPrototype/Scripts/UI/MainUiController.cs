using UnityEngine;
using System.Collections;


public class MainUiController : MonoBehaviour
{
    [SerializeField] protected GameObject[] UIElements;
    [SerializeField] protected UnityEngine.UI.Button[] MoveNextButton;

    private int _currentScreen = 0;

    private void Awake()
    {
        foreach (var button in MoveNextButton)
        {
            button.onClick.AddListener(delegate { GoToNextScreen(); });
        }
    }

    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoToPrevScreen();
        }
    }

    private void GoToNextScreen()
    {
        UIElements[_currentScreen].SetActive(false);
        _currentScreen++;

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
        UIElements[_currentScreen].SetActive(true);
    }
}
