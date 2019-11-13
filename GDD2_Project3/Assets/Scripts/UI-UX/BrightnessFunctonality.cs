using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BrightnessFunctonality : MonoBehaviour
{
    //render is used to change shader value for all objects using it
    float brightness = 0;
    public Renderer rend;

    //sets slider to shader's value
    void Start()
    {
        float startValue = (rend.sharedMaterial.GetFloat("_Brightness") + 0.45f)/0.9f;
        Slider slider = gameObject.GetComponent<Slider>();
        slider.normalizedValue = startValue;
        brightness = startValue;
    }

    //when slider is moved, send new value to shader
    public void OnValueChanged(float newValue)
    {
        //range of -0.45f to 0.45f
        brightness = (newValue*0.9f)-0.45f;
        rend.sharedMaterial.SetFloat("_Brightness", brightness);
    }
}