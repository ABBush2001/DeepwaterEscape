using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;  // Ensure this is included

public class Resolutions : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropDown; // Ensure this is assigned in the Inspector

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;
    public List<Resolution> FilteredResolutions => filteredResolutions;
    private int currentResolutionIndex = 0;

    [HideInInspector] public Resolution resolution;

    private void Start()
    {
        if (resolutionDropDown == null)
        {
            Debug.LogError("Resolution Dropdown is not assigned!");
            return;
        }

        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        HashSet<string> uniqueResolutions = new HashSet<string>();

        resolutionDropDown.ClearOptions();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string resString = resolutions[i].width + "x" + resolutions[i].height;
            if (uniqueResolutions.Add(resString)) // Adds only if unique
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);

            // Match the current screen resolution
            if (savedResolutionIndex == -1 &&
                filteredResolutions[i].width == Screen.currentResolution.width &&
                filteredResolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
            else if (i == savedResolutionIndex)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();

        // Apply the correct resolution at startup
        SetResolution(currentResolutionIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutionIndex < 0 || resolutionIndex >= filteredResolutions.Count)
        {
            Debug.LogError("Invalid resolution index selected!");
            return;
        }

        resolution = filteredResolutions[resolutionIndex];

        // Save user selection
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
        PlayerPrefs.Save();

        int screenMode = PlayerPrefs.GetInt("ScreenMode", 0); // Default to FullScreenWindow
        FullScreenMode mode = (FullScreenMode)screenMode;

        Screen.fullScreenMode = mode;
        Screen.SetResolution(resolution.width, resolution.height, mode != FullScreenMode.Windowed);

        Debug.Log($"Resolution set to: {resolution.width} x {resolution.height}, Mode: {mode}");
    }
}
