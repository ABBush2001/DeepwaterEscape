using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


/*
 * This script handles the cutscene that plays at the start of level 3. It works in
 * much the same way as the cutscene in the ocean floor scene works.
*/
public class Cutscene2 : MonoBehaviour
{
    //variables
    public Camera camera1;
    public Camera camera2;
    public Camera camera3;
    public Camera mainCamera;
    public GameObject camNode1;
    public GameObject camNode2;
    public GameObject camNode3;
    public GameObject camNode4;
    public GameObject camNode5;
    public GameObject camNode6;

    public GameObject textBox;
    public TextMeshProUGUI skipText;

    public CheckpointManager checkPointManager;

    public float moveSpeed = 10;

    public GameObject player;
    //public GameObject camera; Not used.
    public GameObject canvas;

    public static bool hasPlayedCutscene = false;

    Coroutine lastCoroutine = null;

    private bool levelStarted = false;

    //begin the cutscene
    void Start()
    {
        checkPointManager = GameObject.Find("CheckpointManager").GetComponent<CheckpointManager>();

        if (checkPointManager.currentCheckpoint == "" && !hasPlayedCutscene)
        {
            player.SetActive(false);
            canvas.SetActive(false);
            mainCamera.enabled = false;
            camera1.enabled = true;
            camera1.transform.SetPositionAndRotation(camNode1.transform.position, camera1.transform.rotation);
            lastCoroutine = StartCoroutine(StartMovingCamera());
        }
        else
        {
            // Cutscene already played: immediately set up gameplay
            player.SetActive(true);
            canvas.SetActive(true);
            mainCamera.enabled = true;
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = false;
        }
    }

    //exit out of the cutscene if the skip key is pressed
    void Update()
    {
        if (textBox && Input.GetKeyDown(KeyCode.E) && !levelStarted && checkPointManager.currentCheckpoint == "")
        {
            levelStarted = true;
            player.SetActive(true);
            //camera.SetActive(true);
            canvas.SetActive(true);

            textBox.SetActive(false);
            skipText.enabled = false;
            StopCoroutine(lastCoroutine);
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = false;
            mainCamera.enabled = true;

            mainCamera.gameObject.GetComponent<CameraFadeIn>().fadein = true;
        }
    }

    //play the cutscene
    IEnumerator StartMovingCamera()
    {
        //camera 1
        while (camera1.transform.position != camNode2.transform.position)
        {
            camera1.gameObject.transform.position = Vector3.MoveTowards(camera1.gameObject.transform.position, camNode2.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera1.enabled = false;
        camera2.enabled = true;

        //camera 2
        while (camera2.transform.position != camNode4.transform.position)
        {
            camera2.gameObject.transform.position = Vector3.MoveTowards(camera2.gameObject.transform.position, camNode4.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera2.enabled = false;
        camera3.enabled = true;

        //camera 3
        while (camera3.transform.position != camNode6.transform.position)
        {
            camera3.gameObject.transform.position = Vector3.MoveTowards(camera3.gameObject.transform.position, camNode6.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera3.gameObject.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(3f);

        //toggle main camera back on

        camera3.gameObject.SetActive(false);
        player.SetActive(true);
        //camera.SetActive(true);
        canvas.SetActive(true);
        mainCamera.enabled = true;

        mainCamera.gameObject.GetComponent<CameraFadeIn>().fadein = true;

        levelStarted = true;
    }
}
