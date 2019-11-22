using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorDetector : MonoBehaviour
{
    public AIBehavior aib;
    bool sentinel = false;

    //trying out something
    void OnTriggerEnter(Collider other)
    {
        if (!sentinel)
        {
            sentinel = true;
            aib.floorDetected = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (sentinel)
        {
            sentinel = false; 
            aib.floorDetected = false;
        }
    }
}
