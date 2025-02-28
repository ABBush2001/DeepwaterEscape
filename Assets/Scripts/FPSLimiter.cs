using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script imposes an FPS cap which triggers if V-Sync isn't turned on for whatever reason. 
 * Attempts to cap it to the reported monitor refresh rate, but won't go below fallbackMaxFrameRate.
 * 
 */
public class FPSLimiter : MonoBehaviour
{
    [Tooltip("Minimum framerate cap that only applies if V-Sync isn't working. Don't turn this below 60.")]
    public int fallbackMaxFrameRate = 144;
    private double reportedRefreshRate;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 1;
        reportedRefreshRate = Screen.currentResolution.refreshRateRatio.value;

        if (reportedRefreshRate < fallbackMaxFrameRate) {
            Application.targetFrameRate = fallbackMaxFrameRate;
        }

        else
        {
            Application.targetFrameRate = (int)reportedRefreshRate;
        }
    }
}
