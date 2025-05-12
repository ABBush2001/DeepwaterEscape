using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class FinalCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;

    private bool skipped = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer1.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        if(vp.gameObject.tag == "Video1")
        {
            videoPlayer2.Play();
            videoPlayer2.loopPointReached += OnVideoFinished;
            Destroy(videoPlayer1.gameObject);
        }
        else if(vp.gameObject.tag == "Video2")
        {
            skipped = true;
            SceneManager.LoadScene("Credits");
        }
    }

}
