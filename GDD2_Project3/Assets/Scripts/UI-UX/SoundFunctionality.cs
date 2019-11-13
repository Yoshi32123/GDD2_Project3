using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundFunctionality : MonoBehaviour
{
    public static bool SoundEnabled = true;

    //sets toggle state
    void Start()
    {
        Toggle toggle = gameObject.GetComponent<Toggle>();
        toggle.isOn = SoundEnabled;
    }

    /// <summary>
    /// Gets value of toggle and sets sound state
    /// </summary>
    /// <param name="ToggleState">New value of toggle</param>
    public void OnValueChanged(bool ToggleState)
    {
        SoundEnabled = ToggleState;
    }

}
