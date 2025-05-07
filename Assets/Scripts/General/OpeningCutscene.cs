using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/*
 * This script handles the game's opening cutscene
*/

public class OpeningCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject loading;

    public GameObject cutsceneButton;

    private bool skipped = false;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        skipped = true;
        cutsceneButton.SetActive(false);
        loading.GetComponent<loading>().LoadNextScene("1.Submarine");
    }

    private void Update()
    {
        if ((Input.GetKeyDown("e") || Input.GetKeyDown("escape")) && skipped == false)
        {
            skipped = true;
            cutsceneButton.SetActive(false);
            loading.GetComponent<loading>().LoadNextScene("1.Submarine");
        }
    }
}
