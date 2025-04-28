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

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        loading.GetComponent<loading>().LoadNextScene("1.Submarine");
    }

    private void Update()
    {
        if (Input.GetKeyDown("e") || Input.GetKeyDown("escape"))
        {
            loading.GetComponent<loading>().LoadNextScene("1.Submarine");
        }
    }
}
