using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyrotate : MonoBehaviour
{
    public float rotationSpeed = 1.0f; // degrees per second

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * rotationSpeed);
    }

}
