using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFunctionality : MonoBehaviour
{
    public static bool SoundEnabled = true;
    public List<AudioSource> audioSources;
    private Toggle toggle;

    //sets toggle state
    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
        toggle.isOn = SoundEnabled;

        for(int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].mute = !SoundEnabled;
        }
    }

    /// <summary>
    /// Gets value of toggle and sets sound state
    /// </summary>
    /// <param name="ToggleState">New value of toggle</param>
    public void OnValueChanged(bool ToggleState)
    {
        SoundEnabled = ToggleState;

        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].mute = !SoundEnabled;
        }
    }

    //to be called when reset to default button is pressed
    public void ResetToDefault()
    {
        SoundEnabled = true;
        toggle.isOn = SoundEnabled;

        for (int i = 0; i < audioSources.Count; i++)
        {
            audioSources[i].mute = !SoundEnabled;
        }
    }
}
