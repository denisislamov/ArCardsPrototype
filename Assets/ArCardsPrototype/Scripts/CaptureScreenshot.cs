using System;
using UnityEngine;

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
        CaptureScreenshotButton.onClick.AddListener(delegate { ShareImage(); });

        _utilsPlugin = UtilsPlugin.GetInstance();
        _utilsPlugin.SetDebug(0);

        _sharePlugin = SharePlugin.GetInstance();
        _sharePlugin.SetDebug(0);
    }

    public void ShareImage()
    {
        string screenShotName = "ar_card_screehshot.jpg";
        string folderPath = _utilsPlugin.CreateFolder("MyScreenShots", 0);
        string path = "";

        if (!folderPath.Equals("", StringComparison.Ordinal))
        {
            path = folderPath + "/" + screenShotName;

            StartCoroutine(AUP.Utils.TakeScreenshot(path, screenShotName));
            _sharePlugin.ShareImage("subject", "subjectContent", path);
        }
    }
}
