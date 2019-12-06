using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CatHitDetection : MonoBehaviour
{
    public bool damaged;
    public GameObject cameraObj;
    public GameObject player;

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
            cameraObj.GetComponent<PostProcessingBehaviour>().profile.vignette.enabled = true;
        else
            cameraObj.GetComponent<PostProcessingBehaviour>().profile.vignette.enabled = false;
    }

    /// <summary>
    /// Checks if the cat is ever colliding with the player
    /// </summary>
    public void CheckCatCollision()
    {
        // press b to simulate damage indicator currently
        if (Input.GetKeyDown(KeyCode.B))
            damaged = !damaged;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "player" && !player.GetComponent<PlayerHealth>().JustHit)
        {
            damaged = true;
            player.GetComponent<PlayerHealth>().TakeDamage();
        }

        //Debug.Log("collision is working, collision with: " + other.gameObject.name);
    }
}
