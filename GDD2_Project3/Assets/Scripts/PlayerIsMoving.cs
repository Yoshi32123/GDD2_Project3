using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIsMoving : MonoBehaviour
{
    private bool moving = false;

    public bool Moving
    {
        get { return moving; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            moving = true;
        else
            moving = false;
    }
}
