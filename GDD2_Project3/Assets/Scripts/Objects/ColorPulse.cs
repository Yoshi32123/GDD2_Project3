﻿using System.Collections;
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
        float colorValue = 1.5f + Mathf.PingPong(Time.time * 3, 3.0f);
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(colorValue, colorValue, colorValue));
    }
}