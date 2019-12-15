using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicFunctionality : MonoBehaviour
{
    public static bool MusicEnabled = true;
    public AudioSource[] audioSources;
    private Toggle toggle;

    //sets toggle state
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        toggle.isOn = MusicEnabled;

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = !MusicEnabled;
        }
    }

    /// <summary>
    /// Gets value of toggle and sets music state
    /// </summary>
    /// <param name="ToggleState">New value of toggle</param>
    public void OnValueChanged(bool ToggleState)
    {
        MusicEnabled = ToggleState;

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = !MusicEnabled;
        }
    }


    //to be called when reset to default button is pressed
    public void ResetToDefault()
    {
        MusicEnabled = true;
        toggle.isOn = MusicEnabled;

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = !MusicEnabled;
        }
    }
}
