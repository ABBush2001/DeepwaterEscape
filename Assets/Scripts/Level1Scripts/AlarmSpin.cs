using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmSpin : MonoBehaviour
{
    public int rotateSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotateSpeed, 0 * Time.deltaTime); //rotates (rotateSpeed) degrees per second around y axis
    }
}
