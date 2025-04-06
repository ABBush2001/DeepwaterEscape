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

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            if(instructions.enabled)
            {
                instructions.enabled = false;
            }
            else
            {
                instructions.enabled = true;
            }
        }
    }
}
