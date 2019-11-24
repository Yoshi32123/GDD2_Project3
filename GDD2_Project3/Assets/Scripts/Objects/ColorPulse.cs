using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPulse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float colorValue = 0.8f + Mathf.PingPong(Time.time / 1.5f, 0.7f);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(colorValue, colorValue, colorValue));
    }
}
