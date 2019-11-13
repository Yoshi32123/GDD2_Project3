using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject pickupZone;
    public Transform theDest;
    private Quaternion objRot;
    private bool lockRotation;

    void Start()
    {
        lockRotation = false;
        pickupZone = GameObject.FindGameObjectWithTag("PickupSpot");
    }

    void Update()
    {
        if (!lockRotation)
        {
            objRot = this.transform.rotation;
        }
    }

    private void OnMouseDown()
    {
        if (DistanceCheck())
        {
            // updating position
            theDest = pickupZone.transform;

            // lock rotation and position
            lockRotation = true;
            this.transform.rotation = objRot;

            // freeze specific fields of rigidbody
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().freezeRotation = true;

            // enable pickup
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("Destination").transform;
        }
    }

    private void OnMouseUp()
    {
        // disable pickup
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;

        // unfreeze rigidbody fields
        this.GetComponent<Rigidbody>().freezeRotation = false;

        // unlock rotation
        lockRotation = false;
    }

    private bool DistanceCheck()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit, Distance: " + hit.distance);
            
            if (hit.distance < 3)
            {
                return true;
            }

            //Debug.Log("Out of range");
            return false;
        }

        return false;
        
    }
}
