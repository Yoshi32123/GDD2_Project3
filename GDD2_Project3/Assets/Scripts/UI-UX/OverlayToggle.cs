using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayToggle : MonoBehaviour
{
    bool Paused = false;
    bool Options = false;
    public GameObject PausedCanvas;
    public GameObject OptionsCanvas;
    public GameObject WinCanvas;
    public GameObject LostCanvas;

    void Start()
    {
    }

    ///<summary>alternates the paused state</summary>
    public void TogglePausing()
    {
        Paused = !Paused;

        //shows/hides pause screen
        PausedCanvas.SetActive(Paused);

        if (Paused)
        {
            //pause code here
        }
        else
        {
            //unpause code here
        }
    }

    ///<summary>alternates the option state</summary>
    public void ToggleOptions()
    {
        Options = !Options;

        //shows/hides options screen
        OptionsCanvas.SetActive(Options);
    }


    ///<summary>turns on win state</summary>
    public void WonGame()
    {
        //shows pause screen
        WinCanvas.SetActive(true);
    }

    ///<summary>turns on lost state</summary>
    public void LostGame()
    {
        //shows pause screen
        LostCanvas.SetActive(true);
    }
}
