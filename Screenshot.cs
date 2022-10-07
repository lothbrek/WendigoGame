using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            TakeScreenshot();
        }
    }

    public void TakeScreenshot()
    {
        string screenshotName;
        string folderPath = Directory.GetCurrentDirectory() + "/Screenshots/";

        int rand = Random.Range(0, 10000);

        screenshotName = "Screenshot_" + System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + ".png";

        if (!System.IO.Directory.Exists(folderPath))
            System.IO.Directory.CreateDirectory(folderPath);
            
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));
    }
    
}
