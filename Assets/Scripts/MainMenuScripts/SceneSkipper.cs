using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSkipper : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            loadSub();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            loadFloor();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            loadArena();
        }
    }

    public void loadSub()
    {
        SceneManager.LoadScene("1.Submarine");
    }
    public void loadFloor()
    {
        SceneManager.LoadScene("2.OceanFloor");
    }
    public void loadArena()
    {
        SceneManager.LoadScene("4.Arena");
    }
}
