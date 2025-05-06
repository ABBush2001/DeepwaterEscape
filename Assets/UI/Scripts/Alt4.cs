using UnityEngine;

public class Alt4 : MonoBehaviour
{
    void Update()
    {
        // this won't intercept an alt-f4
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.F4))
        {
            // Feedback form
            //Application.OpenURL("https://docs.google.com/forms/d/e/1FAIpQLSfwdjz4HT0iWeojGLPPhOp7fo7Z4mVy0J8iz__-lf81F_aDhA/viewform?usp=header");
            //Application.Quit();
        }
    }

    //*this* will detect the quit command
    void OnApplicationQuit()
    {
        
    }
}
