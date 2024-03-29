﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public GameObject pickupZone;
    public Transform theDest;

    private Quaternion objRot;
    private bool lockRotation;
    private bool startCarry;
    private bool endCarry;
    private bool pickedUp = false;

    [SerializeField] AudioSource sound = null;

    void Start()
    {
        lockRotation = false;
        pickupZone = GameObject.FindGameObjectWithTag("PickupSpot");
        
        if(pickupZone != null)
            theDest = pickupZone.transform;
    }

    void Update()
    {
        if (!lockRotation)
        {
            objRot = this.transform.rotation;
        }

        if (pickedUp)
        {
            MaskHandle();
        }

        HandleSound();
    }

    /// <summary>
    /// Runs when mouse is down. Updates item properties if in position for pickup
    /// </summary>
    private void OnMouseDown()
    {
        if (DistanceCheck())
        {
            // updating position
            theDest = pickupZone.transform;
            pickedUp = true;

            // lock rotation and position
            lockRotation = true;
            this.transform.rotation = objRot;

            // freeze specific fields of rigidbody
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            this.GetComponent<Rigidbody>().freezeRotation = true;

            // enable pickup
            //GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("Destination").transform;

            // update sound noise
            startCarry = true;
        }
    }

    /// <summary>
    /// Runs when mouse is let go. Resets all property updates
    /// </summary>
    private void OnMouseUp()
    {
        // disable pickup
        this.transform.parent = null;
        GetComponent<Rigidbody>().useGravity = true;
        pickedUp = false;

        // unfreeze rigidbody fields
        this.GetComponent<Rigidbody>().freezeRotation = false;

        // unlock rotation
        lockRotation = false;

        // update sound noise
        endCarry = true;
    }

    /// <summary>
    /// Checks if the object trying to be picked up is in range
    /// </summary>
    /// <returns>true/false based on in range</returns>
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

    /// <summary>
    /// Handles the sound of walking with trash
    /// </summary>
    private void HandleSound()
    {
        if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            return;
        }

        if (startCarry && lockRotation && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerIsMoving>().Moving)
        {
            sound.Play();
            startCarry = false;
        }

        if (endCarry || !lockRotation)
        {
            sound.Stop();
            endCarry = false;
        }

        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerIsMoving>().Moving)
        {
            sound.Pause();
        }
        else
        {
            sound.UnPause();
        }
    }

    /// <summary>
    /// Object does not clip through others
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (pickedUp)
        {
            this.transform.position = theDest.position;
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    /// <summary>
    /// Makes sure the object isn't too far into another
    /// </summary>
    public void MaskHandle()
    {
        LayerMask mask = LayerMask.GetMask("StaticAssets");

        // check if something is there with that tag
        if (Physics.Raycast(theDest.position, theDest.forward, 0.25f, mask))
        {
            //Debug.Log("Hit static asset");
            OnMouseUp();
        }
    }
}
