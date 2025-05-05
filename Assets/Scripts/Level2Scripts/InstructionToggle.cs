using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * This script handles the opening instructions for the OceanFloor scene
*/
public class InstructionToggle : MonoBehaviour
{
    public TextMeshProUGUI instructions;
    public GameObject instructionsBorder;

    public GameObject cutsceneSystem;

    // Update is called once per frame
    void Update()
    {
        if (cutsceneSystem.GetComponent<Cutscene>().getLevelStarted())
        {
            cutsceneSystem.GetComponent<Cutscene>().setLevelStarted(true);
            StartCoroutine(toggleOffInstructions());
        }
    }

    IEnumerator toggleOffInstructions()
    {
        yield return new WaitForSeconds(10f);
        instructions.enabled = false;
        instructionsBorder.SetActive(false);
    }
}
