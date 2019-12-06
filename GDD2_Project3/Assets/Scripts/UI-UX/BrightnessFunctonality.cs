using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.PostProcessing;

public class BrightnessFunctonality : MonoBehaviour
{
    //render is used to change shader value for all objects using it
    float brightness = 0;
    public PostProcessingProfile ppProfile;
    ColorGradingModel.Settings colorSettings; //temp holder of values

    //sets slider to shader's value
    void Start()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        //copy current brightness settings from the profile into a temporary variable
        colorSettings = ppProfile.colorGrading.settings;
        slider.normalizedValue = (colorSettings.basic.postExposure+4.0f)/8.0f;
        brightness = colorSettings.basic.postExposure;
    }

    //when slider is moved, send new value to shader
    public void OnValueChanged(float newValue)
    {
        //range of -3f to 3f
        brightness = (newValue*8.0f)-4.0f;
        colorSettings = ppProfile.colorGrading.settings;
        colorSettings.basic.postExposure = brightness;
        ppProfile.colorGrading.settings = colorSettings;
    }

    public void ResetToDefault()
    {
        Slider slider = gameObject.GetComponent<Slider>();
        colorSettings = ppProfile.colorGrading.settings;
        colorSettings.basic.postExposure = 0.0f;
        slider.normalizedValue = (colorSettings.basic.postExposure + 4.0f) / 8.0f;
        ppProfile.colorGrading.settings = colorSettings;
    }

}