using UnityEngine;
using UnityEngine.Audio;

public class VolumeLoader : MonoBehaviour
{
    [SerializeField][Tooltip("The master audio mixer.")] private AudioMixer masterMixer;

    private const float minVol = -80f; // Unity's mute level

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve values
        float masterVol = PlayerPrefs.GetFloat("MasterVolume");
        float BGMVol = PlayerPrefs.GetFloat("BGMVolume");
        float SFXVol = PlayerPrefs.GetFloat("SFXVolume");

        // If values are above 0, convert them to suitable audio mixer values and set values.
        // We set values only *if* they're above 0 to avoid(?) game being muted on first boot.
        if (PlayerPrefs.GetFloat("MasterVolume") > 0) {
            masterVol = (float)(Mathf.Log10(masterVol) * 20);
            masterMixer.SetFloat("MasterVolume", masterVol);
        }

        if (PlayerPrefs.GetFloat("BGMVolume") > 0) {
            BGMVol = (float)(Mathf.Log10(BGMVol) * 20);
            masterMixer.SetFloat("BGMVolume", BGMVol);
        }

        if (PlayerPrefs.GetFloat("SFXVolume") > 0) {
            SFXVol = (float)(Mathf.Log10(SFXVol) * 20);
            masterMixer.SetFloat("SFXVolume", SFXVol);
        }
    }
}
