using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame2 : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F4))
        {
            Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfwdjz4HT0iWeojGLPPhOp7fo7Z4mVy0J8iz__-lf81F_aDhA/viewform?usp=header");
            Application.Quit();
        }
    }
}

