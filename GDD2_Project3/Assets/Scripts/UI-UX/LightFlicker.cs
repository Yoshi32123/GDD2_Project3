using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light Light;
    bool flickering;
    float numCheck = 0;
    bool up = false;
    float amplitude = 0;
    float angularFrequency = 0;


    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        StartCoroutine(Run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator Run()
    {
        while (true)
        {
            //yield return new WaitForSeconds(Random.Range(1f, 50f));
            //yield return new WaitForSeconds(.01f);

            StartCoroutine(Flickering());
        }
    }

    IEnumerator Flickering()
    {
        flickering = true;
        amplitude = .2f;
        angularFrequency = 2 * Mathf.PI * 100;
        while (true)
        {
            // give a time frame for these events
            yield return new WaitForSeconds(.01f);
            numCheck += .01f;
            if (flickering)
            {
                // oscillating as a decaying sinusoidal wave
                Light.intensity = amplitude * Mathf.Sin(angularFrequency * numCheck) + .3f;
                amplitude *= -.9f;
                if (amplitude <= .05f)
                {
                    flickering = false;
                }
            }
            else
            {
                // end the flickering
                //StopCoroutine(Flickering());
            }
        }
        
    }
}