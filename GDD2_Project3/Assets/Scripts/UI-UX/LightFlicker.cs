using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light Light;
    bool flickering;
    int numCheck = 0;

    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        StartCoroutine(Flickering());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Flickering()
    {
        while (true)
        {
            // give a time frame for these events
            yield return new WaitForSeconds(.01f);

            if (flickering)
            {
                // increase the intensity of the light back to full
                Light.intensity += .01f;
                if (Light.intensity >= .3f)
                {
                    Light.intensity = .3f;

                    // put a timer in here through the waitForSeconds
                    numCheck++;
                    if (numCheck >= 150)
                    {
                        // reset our timer
                        numCheck = 0;
                        flickering = false;
                    }
                }
            }
            else
            {
                // suddenly set the light intensity lower, to simulate sudden flickering
                Light.intensity = .1f;
                flickering = true;
            }
        }
        
    }
}