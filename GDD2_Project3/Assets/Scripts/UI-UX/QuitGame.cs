using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    /// <summary>
    /// Kills the game application
    /// Only works in a compiled application (build. not within unity app) 
    /// </summary>
    public void doExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
