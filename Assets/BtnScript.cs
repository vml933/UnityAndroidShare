using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BtnScript : MonoBehaviour
{

    private Button btn1;

    private string ScreenshotName = "screenshot.png";
    private string text = "hihi this is share text";

    void Start()
    {
        btn1 = this.GetComponent<Button>();
        btn1.onClick.AddListener(() =>
        {
            DoCaptureAndShare();
        });
    }

    private void DoCaptureAndShare()
    {
        string screenShotPath = Application.persistentDataPath + "/" + ScreenshotName;
        if (File.Exists(screenShotPath)) File.Delete(screenShotPath);

        Application.CaptureScreenshot(ScreenshotName);

        StartCoroutine(delayedShare(screenShotPath, text));
    }

    IEnumerator delayedShare(string screenShotPath, string text)
    {
        while (!File.Exists(screenShotPath))
        {
            yield return new WaitForSeconds(0.5f);
        }

        Share(screenShotPath, text);
    }

    private void Share(string imagePath, string text)
    {
        using (var javaUnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (var currentActivity = javaUnityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                using (var androidPlugin = new AndroidJavaObject("com.mark.myshareplugin.ShareWork", currentActivity))
                {
                    androidPlugin.Call("DoShare", imagePath, text);
                }
            }
        }


    }
}
