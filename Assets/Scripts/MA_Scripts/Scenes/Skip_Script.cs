using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

public class Skip_Script : MonoBehaviour
{
    public TMP_Text textOff;
    public VideoPlayer videoPlayer;
    public GameObject border;

    private bool skipped = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && skipped == false)
        {
            skipped = true;
            LoadScene();
            textOff.gameObject.SetActive(false);
            border.SetActive(false);
        }
    }

    public void LoadScene()
    {
        //SceneManager.LoadScene("1.Submarine");
        videoPlayer.Stop();
    }
}
