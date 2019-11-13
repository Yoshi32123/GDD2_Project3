using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public Transform theDest;
    private Quaternion objRot;
    private bool lockRotation;

    void Start()
    {
        lockRotation = false;
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
}
