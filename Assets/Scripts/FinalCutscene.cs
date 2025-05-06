using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FinalCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private bool skipped = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        skipped = true;
        SceneManager.LoadScene("Credits");
    }

}
