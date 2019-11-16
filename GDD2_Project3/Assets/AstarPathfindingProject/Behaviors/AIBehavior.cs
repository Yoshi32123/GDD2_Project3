
using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIBehavior : MonoBehaviour
{
    public AIPath aiPathScript;
    public AIDestinationSetter aiDestinationSetterScript;
    public GameObject playerGO;
    public GameObject motherNode;
    private List<GameObject> listOfRooms;

    public bool seekingPlayer = false;
    private readonly float waitTime = 2.0f;
    private float timer = 0.0f;
    Vector3 direction;
    Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        //SeekPlayer();
        BeginWandering();

        //adds all rooms to room list
        listOfRooms = new List<GameObject>();
        foreach (Transform child in motherNode.transform)
        {
            listOfRooms.Add(child.gameObject);
        }

        //picks a random node in the first room to go to
        int randomChildIdx = Random.Range(0, listOfRooms[0].transform.childCount);
        Transform randomChild = listOfRooms[0].transform.GetChild(randomChildIdx);
        aiDestinationSetterScript.target = randomChild;
    }

    // Update is called once per frame
    void Update()
    {
        //found player and will hunt player down to the death!
        if (seekingPlayer)
        {
            Transform playerPosition = playerGO.transform;
            aiDestinationSetterScript.target = playerPosition;

            //If the cat is very close to player, go offpath for the kill
            if((playerPosition.position - transform.position).magnitude <= 10)
            {
                aiPathScript.isStopped = true;

                direction = (playerGO.transform.position - transform.position).normalized;
                direction.y = 0;
                velocity = direction * (aiPathScript.maxSpeed / 65f);
                transform.position += velocity;
            }

            //player is too far, go back to pathfinding to find next closest point
            else
            {
                aiPathScript.isStopped = false;
            }
        }

        //Wander room to room and waits 2 seconds when at its spot it wants to be at
        else
        {
            //Increment timer when waiting 
            if (aiPathScript.reachedEndOfPath)
            {
                timer += Time.deltaTime;
            }

            //Waited long enough. 
            //Picks a new place to go to
            if (timer > waitTime)
            {
                // Remove the recorded wait time in seconds.
                timer -= waitTime;

                int randomRoomIdx = Random.Range(0, listOfRooms.Count);
                int randomChildIdx = Random.Range(0, listOfRooms[randomRoomIdx].transform.childCount);
                Transform randomChild = listOfRooms[randomRoomIdx].transform.GetChild(randomChildIdx);
                aiDestinationSetterScript.target = randomChild;
            }

        }
    }

    /*
     * Makes cat rush towards the player for the kill!
     */
    public void SeekPlayer()
    {
        aiPathScript.isStopped = false;
        seekingPlayer = true;
        aiPathScript.maxSpeed = 13;
    }

    /*
     * Makes cat wander around aimlessly
     */
    public void BeginWandering()
    {
        aiPathScript.isStopped = false;
        seekingPlayer = false;
        aiPathScript.maxSpeed = 5;
    }
}
