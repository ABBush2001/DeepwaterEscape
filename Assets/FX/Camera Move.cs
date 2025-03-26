using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float speed = 5f; // Adjust movement speed

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right Arrow
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down Arrow

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
    }
}

