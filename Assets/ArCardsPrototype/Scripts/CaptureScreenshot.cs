using System;
using UnityEngine;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;

public class CaptureScreenshot : MonoBehaviour
{
    [Space(10)]
    [Header("UI References")]
    [SerializeField]
    protected UnityEngine.UI.Button CaptureScreenshotButton;

    private SharePlugin _sharePlugin;
    private UtilsPlugin _utilsPlugin;

    [SerializeField] protected AudioSource SnapAudioSource;
    [SerializeField] protected GameObject Splash;

#if UNITY_IPHONE
    [DllImport("__Internal")]
    private static extern bool saveToGallery(string path);
#endif

    protected void Awake()
    {
        CaptureScreenshotButton.onClick.AddListener(delegate { StartCoroutine(TakeImage()); });

#if UNITY_ANDROID
        _utilsPlugin = UtilsPlugin.GetInstance();
        _utilsPlugin.SetDebug(0);

        _sharePlugin = SharePlugin.GetInstance();
        _sharePlugin.SetDebug(0);
#endif
    }

    public IEnumerator TakeImage()
    {
      
        var photoSaved = false;
        Debug.Log("TakeImage Start");

        var date = DateTime.Now.ToString("dd_MM_yy_H_mm_ss");
        var screenshotFilename = "arCard" + "_" + date + ".png";


        // ANDROID
#if UNITY_ANDROID
        var androidPath = "/../../../../DCIM/" + "ArCards" + "/" + screenshotFilename;
        var path = Application.persistentDataPath + androidPath;
        var pathonly = Path.GetDirectoryName(path);

        if (pathonly != null)
        {
            Directory.CreateDirectory(pathonly);
        }
        Application.CaptureScreenshot(androidPath);

        var obj = new AndroidJavaClass("com.ryanwebb.androidscreenshot.MainActivity");

        SnapAudioSource.Play();
        Splash.SetActive(true);
        yield return new WaitForSeconds(.2f);
        Splash.SetActive(false);

        while (!photoSaved)
        {
            photoSaved = obj.CallStatic<bool>("scanMedia", path);

            yield return new WaitForSeconds(.5f);
        }
#endif

        // IOS
#if UNITY_IPHONE
        string iosPath = Application.persistentDataPath + "/" + screenshotFilename;

        Application.CaptureScreenshot(screenshotFilename);

        SnapAudioSource.Play();
        Splash.SetActive(true);
        yield return new WaitForSeconds(.2f);
        Splash.SetActive(false);

        while (!photoSaved)
        {
            photoSaved = saveToGallery(iosPath);

            yield return new WaitForSeconds(.5f);
        }

        UnityEngine.iOS.Device.SetNoBackupFlag(iosPath);
#endif

        Debug.Log("TakeImage Done");
    }
}
