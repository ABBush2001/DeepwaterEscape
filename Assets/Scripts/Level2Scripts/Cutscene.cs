using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script handles an opening cutscene for the oceanfloor 
 * scene to help make the scene more interesting
*/
public class Cutscene : MonoBehaviour
{
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

    public TextMeshProUGUI instructions;

    Coroutine lastCoroutine = null;

    private bool levelStarted = false;

    [SerializeField] float moveSpeed = 10;

    public GameObject playerAnimator;
    private Animator animControl;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera.enabled = false;
        camera1.transform.SetPositionAndRotation(camNode1.transform.position, camera1.transform.rotation);
        lastCoroutine = StartCoroutine(startMovingCamera());
        animControl = playerAnimator.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textBox && Input.GetKeyDown(KeyCode.E) && !levelStarted)
        {
            levelStarted = true;

            textBox.SetActive(false);
            skipText.enabled = false;
            instructions.enabled = true;
            StopCoroutine(lastCoroutine);
            camera1.enabled = false;
            camera2.enabled = false;
            camera3.enabled = false;
            mainCamera.enabled = true;
            animControl.SetTrigger("IntroAnim");
            mainCamera.gameObject.GetComponent<CameraFadeIn>().fadein = true;
            
        }
    }

    IEnumerator startMovingCamera()
    {
        while (camera1.transform.position.z < camNode2.transform.position.z)
        {
            //camera1.transform.SetPositionAndRotation(new Vector3(camera1.transform.position.x, camera1.transform.position.y, camera1.transform.position.z + 0.1f), camera1.transform.rotation);
            camera1.gameObject.transform.position = Vector3.MoveTowards(camera1.gameObject.transform.position, camNode2.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera1.enabled = false;
        camera2.enabled = true;

        while (camera2.transform.position.x < camNode4.transform.position.x)
        {
            //camera2.transform.SetPositionAndRotation(new Vector3(camera2.transform.position.x + 0.1f, camera2.transform.position.y, camera2.transform.position.z), camera2.transform.rotation);
            camera2.gameObject.transform.position = Vector3.MoveTowards(camera2.gameObject.transform.position, camNode4.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera2.enabled = false;
        camera3.enabled = true;

        while (camera3.transform.position.x > camNode6.transform.position.x)
        {
            //camera3.transform.SetPositionAndRotation(new Vector3(camera3.transform.position.x - 0.1f, camera3.transform.position.y, camera3.transform.position.z), camera3.transform.rotation);
            camera3.gameObject.transform.position = Vector3.MoveTowards(camera3.gameObject.transform.position, camNode6.transform.position, Time.deltaTime * moveSpeed);
            yield return new WaitForSeconds(0.01f);
        }

        camera3.gameObject.GetComponent<CameraFadeOut>().fadeOut = true;
        yield return new WaitForSeconds(3f);

        camera3.gameObject.SetActive(false);
        mainCamera.enabled = true;

        mainCamera.gameObject.GetComponent<CameraFadeIn>().fadein = true;
        instructions.enabled = true;
        animControl.SetTrigger("IntroAnim");

        levelStarted = true;
    }
}
