using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadMainScene : MonoBehaviour
{
    [SerializeField] protected string MainSceneName;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(MainSceneName);
        yield return async;
    }
}
