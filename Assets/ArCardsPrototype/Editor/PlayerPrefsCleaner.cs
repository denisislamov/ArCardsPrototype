using UnityEngine;
using UnityEditor;

public class PlayerPrefsCleaner
{
    [MenuItem("PlayerPrefsCleaner/DeleteAll")]
    static void DoSomething()
    {
        PlayerPrefs.DeleteAll();
    }
}
