using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    private bool damaged;

    // Start is called before the first frame update
    void Start()
    {
        damaged = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckCatCollision();

        if (damaged == true)
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile.vignette.enabled = true;
        else
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PostProcessingBehaviour>().profile.vignette.enabled = false;
    }

    /// <summary>
    /// Checks if the cat is ever colliding with the player
    /// </summary>
    public void CheckCatCollision()
    {
        // press b to simulate damge currently
        if (Input.GetKey(KeyCode.B))
            damaged = true;
        else
            damaged = false;
    }
}
