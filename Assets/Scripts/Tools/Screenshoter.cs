using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshoter : MonoBehaviour
{
    string m_Path = System.Environment.CurrentDirectory + "\\截屏";
    private string GetFileName()
    {
        if (!Directory.Exists(m_Path))
        {
            Directory.CreateDirectory(m_Path);
        }
        return string.Format
        (
            "{0}/ScreenShot_{1}_{2}_{3}.png",
            m_Path,
            DateTime.Now.ToLongDateString(),
            DateTime.Now.ToString("HH时mm分ss秒"),
            DateTime.Now.Millisecond
        );
    }

    public void Screenshot()
    {
        string fileName = GetFileName();
        ScreenCapture.CaptureScreenshot(fileName, 0);
        ToastSystem.Show("保存截图成功！文件路径：" + fileName);
    }
}
