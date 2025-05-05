using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpeningInstructions : MonoBehaviour
{
    public GameObject UIBorder;
    public TextMeshProUGUI instructions;

    // Start is called before the first frame update
    void Start()
    {
        UIBorder.SetActive(true);
        instructions.enabled = true;
        StartCoroutine(waitForFade());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            UIBorder.SetActive(false);
            instructions.enabled = false;
            Time.timeScale = 1;
        }
    }

    IEnumerator waitForFade()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }
}
