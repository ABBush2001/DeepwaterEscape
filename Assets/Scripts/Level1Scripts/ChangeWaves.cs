using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script changes the waves that appear on the computer screens in level 1 to be red
 * when the alarm is set off. It does this by changing the material on the screens.
*/

public class ChangeWaves : MonoBehaviour
{
    public Material redFlatlineMat;
    public Material redStaticMat;
    public Material redSonarMat;

    public GameObject flatline1;
    public GameObject flatline2;
    public GameObject flatline3;
    public GameObject flatline4;
    public GameObject staticFuzz;
    public GameObject sonar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateMats()
    {
        flatline1.GetComponent<MeshRenderer>().material = redFlatlineMat;
        flatline2.GetComponent<MeshRenderer>().material = redFlatlineMat;
        flatline3.GetComponent<MeshRenderer>().material = redFlatlineMat;
        flatline4.GetComponent<MeshRenderer>().material = redFlatlineMat;
        staticFuzz.GetComponent<MeshRenderer>().material = redStaticMat;
        sonar.GetComponent<MeshRenderer>().material = redSonarMat;
    }
}
