using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PostProcessing;

public class ContrastFunctionality : MonoBehaviour
{

    //render is used to change shader value for all objects using it
    float contrast = 0;
    public PostProcessingProfile ppProfile;
    ColorGradingModel.Settings colorSettings; //temp holder of values

    //sets slider to shader's value
    void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        //copy current bloom settings from the profile into a temporary variable
        colorSettings = ppProfile.colorGrading.settings;
        slider.normalizedValue = (colorSettings.basic.contrast - 0.5f) / 1.5f;
        contrast = colorSettings.basic.contrast;
    }

    //when slider is moved, send new value to shader
    public void OnValueChanged(float newValue)
    {
        //range of 0.5f to 2.0f
        contrast = (newValue * 1.5f) + 0.5f;
        colorSettings = ppProfile.colorGrading.settings;
        colorSettings.basic.contrast = contrast;
        ppProfile.colorGrading.settings = colorSettings;
    }


    public void ResetToDefault()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        colorSettings = ppProfile.colorGrading.settings;
        colorSettings.basic.contrast = 1.0f;
        slider.normalizedValue = (colorSettings.basic.contrast - 0.5f) / 1.5f;
        ppProfile.colorGrading.settings = colorSettings;
    }
}
