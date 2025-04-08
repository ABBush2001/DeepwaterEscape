using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Manager : MonoBehaviour
{
    [SerializeField] private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueComplete)
        {
            GameObject.Find("DialogueManager").GetComponent<DialogueManager>().dialogueComplete = false;
            StartCoroutine(fadeToNextScene());
        }*/
    }

    
}
