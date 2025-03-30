using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script is for the flashing circle in which debris can fall. It flashes the circle
 * multiple times before debris falls in the area.
*/
public class DebrisWarningCircle : MonoBehaviour
{
    public int duration = 5;
    public bool warningComplete = false;

    private Material circleMat;
    private Color tempColor;

    // Start is called before the first frame update
    void Start()
    {
        circleMat = GetComponent<Renderer>().material;
        tempColor = circleMat.color;
        StartCoroutine(flashingCircle());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator flashingCircle()
    {
        for(int i = 0; i < duration; i++)
        {
            while (circleMat.color.a > 0)
            {
                tempColor = circleMat.color;
                tempColor.a -= 0.008f;
                circleMat.color = tempColor;
                yield return new WaitForSeconds(0.0001f);
            }

            while(circleMat.color.a < 1)
            {
                tempColor = circleMat.color;
                tempColor.a += 0.008f;
                circleMat.color = tempColor;
                yield return new WaitForSeconds(0.0001f);
            }

            yield return new WaitForSeconds(0.5f);
        }

        warningComplete = true;
    }
}
