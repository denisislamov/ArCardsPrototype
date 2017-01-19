using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] protected string MainSceneName;

    IEnumerator Start()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(MainSceneName);
        yield return async;
    }
}
