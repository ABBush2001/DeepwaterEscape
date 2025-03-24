using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorShift : MonoBehaviour
{
    public Light light;
    public Color startColor = Color.blue;
    public Color endColor = Color.red;
    public float cycleSpeed = 1f;
    private float time = 0f;

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime * cycleSpeed;
        if(time > 1)
        {
            var temp = endColor;
            endColor = startColor;
            startColor = temp;
            time = 0;
        }
        light.color = Color.Lerp(startColor, endColor, time);
    }
}
