using System;
using UnityEngine;
using System.IO;
using System.Collections;

public class CaptureScreenshot : MonoBehaviour
{
    [Space(10)]
    [Header("UI References")]
    [SerializeField]
    protected UnityEngine.UI.Button CaptureScreenshotButton;

    private SharePlugin _sharePlugin;
    private UtilsPlugin _utilsPlugin;

    protected void Awake()
    {
        Init();
    }

    private void Init()
    {
        CaptureScreenshotButton.onClick.AddListener(delegate { StartCoroutine(TakeImage()); });

        _utilsPlugin = UtilsPlugin.GetInstance();
        _utilsPlugin.SetDebug(0);

        _sharePlugin = SharePlugin.GetInstance();
        _sharePlugin.SetDebug(0);
    }

    public IEnumerator TakeImage()
    {
        bool photoSaved = false;

        Debug.Log("TakeImage Start");

        string date = System.DateTime.Now.ToString("dd_MM_yy_H_mm_ss");
        string screenshotFilename = "arCard" + "_" + date + ".png";

        string androidPath = "/../../../../DCIM/" + "ArCards" + "/" + screenshotFilename;
        string path = Application.persistentDataPath + androidPath;
        string pathonly = Path.GetDirectoryName(path);

        Directory.CreateDirectory(pathonly);
        Application.CaptureScreenshot(androidPath);

        AndroidJavaClass obj = new AndroidJavaClass("com.ryanwebb.androidscreenshot.MainActivity");

        while (!photoSaved)
        {
            photoSaved = obj.CallStatic<bool>("scanMedia", path);

            yield return new WaitForSeconds(.5f);
        }

        Debug.Log("TakeImage Done");
    }
}
