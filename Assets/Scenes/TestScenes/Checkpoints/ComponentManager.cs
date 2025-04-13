using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script ensures there is only ever one instance of
 * the game manager at a single time
*/
public class ComponentManager : MonoBehaviour
{
    private static ComponentManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
