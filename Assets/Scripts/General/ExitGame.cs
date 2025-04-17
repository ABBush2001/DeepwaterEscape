using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This script holds the event that can be called to quit
 * out of the game.
*/
public class ExitGame : MonoBehaviour
{
    public void QuitGame(int confirm)
    {
        if (confirm == 1)
        {
            Application.Quit();
        }
    }
}
