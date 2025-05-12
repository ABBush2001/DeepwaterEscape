using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * This script handles object collection. It is attached to an object
 * that the player should be able to collect. A UI element will display
 * upon collection. When the given 'pick-up' button is clicked, the object
 * is destroyed and the UI element is set inactive
*/
public class ObjectPickup : MonoBehaviour
{
    //variables
    [SerializeField] private GameObject pickupPrompt;
    private bool promptOn;
    public AudioSource pickUp;
    public GameObject alarm;
    private bool alarmStart = false;

    [SerializeField] private TextAsset inkJson;

    public TextMeshProUGUI gunX;
    public TextMeshProUGUI panX;
    public TextMeshProUGUI tankX;

    private bool isGun = false;
    private bool isPan = false;
    private bool isTank = false;

    //initially set prompt to false
    private void Start()
    {
        pickupPrompt.SetActive(false);
        promptOn = false;
        //alarm.SetActive(false);
    }

    private void Update()
    {
        if (promptOn == true && Input.GetKeyDown(KeyCode.E))
        {
            //check if level 1
            if (SceneManager.GetActiveScene().name == "1.Submarine")
            {
                //set items in level manager

                GameObject levelManager = GameObject.Find("LevelManager");

                if (gameObject.CompareTag("Button"))
                {
                    alarm.SetActive(true);
                    alarmStart = true;
                    levelManager.GetComponent<LevelOneManager>().alarmStart = true;
                    DialogueManager.GetInstance().EnterDialogueMode(inkJson);
                    levelManager.GetComponent<ChangeWaves>().updateMats();
                    GameObject.Find("AlarmSFX").GetComponent<AudioSource>().Play();
                    //levelManager.GetComponent<TimerAlterDisplay>().timerRunning = true;
                    //StartCoroutine(countdownDialogue());
                    //levelManager.GetComponent<LevelOneManager>().turnOnObjects();
                }
                else
                {
                    pickUp.Play();
                    if (!levelManager.GetComponent<LevelOneManager>().getItem1())
                    {
                        levelManager.GetComponent<LevelOneManager>().setItem1();
                    }
                    else if (!levelManager.GetComponent<LevelOneManager>().getItem2())
                    {
                        levelManager.GetComponent<LevelOneManager>().setItem2();
                    }
                    else if (!levelManager.GetComponent<LevelOneManager>().getItem3())
                    {
                        levelManager.GetComponent<LevelOneManager>().setItem3();
                    }

                    //update UI
                    if (isGun)
                    {
                        gunX.enabled = true;
                    }
                    else if (isPan)
                    {
                        panX.enabled = true;
                    }
                    else if (isTank)
                    {
                        tankX.enabled = true;
                    }

                    Destroy(this.gameObject, 1);
                }
                
            }
            promptOn = false;
            pickupPrompt.SetActive(false);
            GetComponent<Renderer>().enabled = false;
        }

        //if in level 1 and dialogue for the button is complete
        if(SceneManager.GetActiveScene().name == "1.Submarine" && alarmStart && DialogueManager.GetInstance().dialogueComplete)
        {
            GameObject levelManager = GameObject.Find("LevelManager");
            levelManager.GetComponent<LevelOneManager>().BeginAlarm();
            levelManager.GetComponent<TimerAlterDisplay>().timerRunning = true;
            this.gameObject.SetActive(false);
        }
    }

    //activates prompt on entry
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.tag == "Gun")
            {
                isGun = true;
            }
            else if(this.tag == "Panacea")
            {
                isPan = true;
            }
            else if(this.tag == "Tank")
            {
                isTank = true;
            }

            pickupPrompt.SetActive(true);
            promptOn = true;
        }
    }
    //closes prompt on exit
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.tag == "Gun")
            {
                isGun = false;
            }
            else if (this.tag == "Panacea")
            {
                isPan = false;
            }
            else if (this.tag == "Tank")
            {
                isTank = false;
            }

            pickupPrompt.SetActive(false);
            promptOn = false;
        }

    }

    IEnumerator countdownDialogue()
    {
        GameObject levelManager = GameObject.Find("LevelManager");

        yield return new WaitForSeconds(1);
        DialogueManager.GetInstance().EnterDialogueMode(inkJson);

        /*while(!DialogueManager.GetInstance().dialogueComplete)
        {
            continue;
        }*/
        //levelManager.GetComponent<TimerAlterDisplay>().timerRunning = true;
        //levelManager.GetComponent<LevelOneManager>().turnOnObjects();
        //Destroy(this.gameObject, 1);
    }
}
