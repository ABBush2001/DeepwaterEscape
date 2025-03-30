using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script handles falling debris. It spawns debris on a Node
 * above the warning circle, then moves the degree towards the circle,
 * destroying it once it arrives
*/
public class FallingDebris : MonoBehaviour
{
    public GameObject startingNode;
    public GameObject warningCircle;
    public GameObject fallingDebris;

    private bool isFalling = false;
    private GameObject temp;

    private void Update()
    {
        if (warningCircle.GetComponent<DebrisWarningCircle>().warningComplete)
        {
            StartCoroutine(beginFall());
            warningCircle.GetComponent<DebrisWarningCircle>().warningComplete = false;
        }

        if (isFalling)
        {
            temp.transform.Rotate(Vector3.one * 50f * Time.deltaTime);
        }
    }

    IEnumerator beginFall()
    {
        temp = Instantiate(fallingDebris);
        temp.transform.SetPositionAndRotation(startingNode.transform.position, startingNode.transform.rotation);
        isFalling = true;

        while(temp.transform.position.y > warningCircle.transform.position.y)
        {
            temp.transform.SetPositionAndRotation(new Vector3(temp.transform.position.x, temp.transform.position.y - 0.1f, temp.transform.position.z), temp.transform.rotation);
            yield return new WaitForSeconds(0.05f);
        }
    }

}
