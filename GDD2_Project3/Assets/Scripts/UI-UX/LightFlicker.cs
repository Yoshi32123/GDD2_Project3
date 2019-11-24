using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    Light Light;
    //[SerializeField] bool flickering;
    [SerializeField] float numCheck = 0;
    //[SerializeField] bool up = false;
    [SerializeField] float amplitude = .2f;
    [SerializeField] float angularFrequency = 2 * Mathf.PI* 100;
    [SerializeField] float intensityBase;

    // Start is called before the first frame update
    void Start()
    {
        Light = GetComponent<Light>();
        //StartFlicker();
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
            yield return new WaitForSeconds(Random.Range(3f, 10f));
            //yield return new WaitForSeconds(2f);

            StartFlicker();
        }
    }

    private void StartFlicker()
    {
        amplitude = .15f;
        angularFrequency = 2 * Mathf.PI * 3f;
        numCheck = 0;

        //Debug.Log("preliminary log:");
        //Debug.Log("flickering: " + flickering.ToString());
        //Debug.Log("amplitude: " + amplitude.ToString());
        //Debug.Log("angular frequency: " + angularFrequency.ToString());
        StartCoroutine(Flickering());
    }

    IEnumerator Flickering()
    {

        while (true)
        {
            //Debug.Log("in-coroutine log:");
            //Debug.Log("flickering: " + flickering.ToString());
            //Debug.Log("amplitude: " + amplitude.ToString());
            //Debug.Log("angular frequency: " + angularFrequency.ToString());

            // give a time frame for these events
            yield return new WaitForSeconds(.01f);
            numCheck += .01f;
            
            // oscillating as a decaying sinusoidal wave
            // amplitude * e^(decayconstant * t) * (cos(angularFrequency * time + phase angle)) + base intensity
            Light.intensity = amplitude * Mathf.Exp(-3f * numCheck) * (Mathf.Cos(angularFrequency * numCheck + 1f)) + intensityBase;
            if (numCheck >= 1f)
            {
                numCheck = 0;
                yield break;
            }
        }
    }
}