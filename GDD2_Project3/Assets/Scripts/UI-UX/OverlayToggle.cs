using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;

public class OverlayToggle : MonoBehaviour
{
    bool Paused = false;
    bool Options = false;
    public GameObject PausedCanvas;
    public GameObject OptionsCanvas;
    public GameObject WinCanvas;
    public GameObject LostCanvas;
    public GameObject FPSController;

    void Start()
    {
        Unpause();
    }

    void Update()
    {
        //Show/hides the pause canvas when P or ESc is pressed.
        if (PausedCanvas != null && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
        {
            TogglePausing();
        }
    }

    ///<summary>alternates the paused state</summary>
    public void TogglePausing()
    {

        //shows/hides pause screen
        Paused = !Paused;
        PausedCanvas.SetActive(Paused);

        if (Paused)
        {
            Pause();
        }
        else
        {
            Unpause();
        }
    }

    ///<summary>pauses the game. Only to be used in overlay toggle</summary>
    private void Pause()
    {
        FPSController.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }


    ///<summary>unpauses the game. Needs to be called in start too</summary>
    private void Unpause()
    {
        FPSController.GetComponent<FirstPersonController>().enabled = true;
        OptionsCanvas.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
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
