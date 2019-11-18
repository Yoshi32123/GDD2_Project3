
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

    Vector3 direction;
    Vector3 velocity;
    private float searchTime = 0.0f;
    private float waitTime = 0.0f;
    private float timer = 0.0f;
    private enum MovementState { Wandering, Chasing, Searching};
    private MovementState currentBehavior;
    private int currentRoomIndex;


    // Start is called before the first frame update
    void Start()
    {
        ChasePlayer();
        //WanderAround();
        //SearchRoom();

        //adds all rooms to room list
        listOfRooms = new List<GameObject>();
        foreach (Transform child in motherNode.transform)
        {
            listOfRooms.Add(child.gameObject);
        }

        //picks a random node in the first room to go to
        currentRoomIndex = 0;
        int randomChildIdx = Random.Range(0, listOfRooms[currentRoomIndex].transform.childCount);
        Transform randomChild = listOfRooms[currentRoomIndex].transform.GetChild(randomChildIdx);
        aiDestinationSetterScript.target = randomChild;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentBehavior)
        {
            case MovementState.Chasing:
                Transform playerPosition = playerGO.transform;
                aiDestinationSetterScript.target = playerPosition;

                //If the cat is very close to player, go offpath for the kill
                if ((playerPosition.position - transform.position).magnitude <= 10)
                {
                    aiPathScript.isStopped = true;

                    direction = (playerGO.transform.position - transform.position);
                    direction.y = 0;
                    direction = direction.normalized;

                    //error check
                    if(direction != Vector3.zero)
                    {
                        transform.forward = Vector3.Slerp(direction, transform.forward, 0.94f);
                    }


                    direction = transform.forward;
                    direction.y = 0;

                    velocity = direction * (aiPathScript.maxSpeed / 50f);

                    Vector3 playerPos = playerPosition.position;
                    playerPos.y = 0.0f;
                    Vector3 catPos = transform.position;
                    catPos.y = 0.0f;

                    if ((playerPosition.position - transform.position).magnitude > 2.7f){
                        transform.position += velocity;
                    }
                }

                //player is too far, go back to pathfinding to find next closest point
                else
                {
                    aiPathScript.isStopped = false;
                }
                break;

            case MovementState.Searching:
                searchTime -= Time.deltaTime;

                //Increment timer when waiting 
                if (aiPathScript.reachedEndOfPath)
                {
                    timer += Time.deltaTime;
                }

                //Waited long enough. 
                //Picks a new place in the same room to go to
                if (timer > waitTime)
                {
                    // Remove the recorded wait time in seconds.
                    timer -= waitTime;

                    int randomChildIdx = Random.Range(0, listOfRooms[currentRoomIndex].transform.childCount);
                    Transform randomChild = listOfRooms[currentRoomIndex].transform.GetChild(randomChildIdx);
                    aiDestinationSetterScript.target = randomChild;


                    //begins wandering when it gets tired of searching if it searches too long
                    //but only switches mode when at an end node
                    if (searchTime <= 0.0f)
                    {
                        WanderAround();
                    }
                }

                break;

            case MovementState.Wandering:
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

                    //picks a random room and finds a random spot
                    currentRoomIndex = Random.Range(0, listOfRooms.Count);
                    int randomChildIdx = Random.Range(0, listOfRooms[currentRoomIndex].transform.childCount);
                    Transform randomChild = listOfRooms[currentRoomIndex].transform.GetChild(randomChildIdx);
                    aiDestinationSetterScript.target = randomChild;
                }
                break;

            default:
                break;
        }
    }

    /*
     * Makes cat rush towards the player for the kill!
     */
    public void ChasePlayer()
    {
        aiPathScript.isStopped = false;
        currentBehavior = MovementState.Chasing;
        aiPathScript.maxSpeed = 7;
    }


    /*
     * Makes cat move around the room looking for the player
     */
    public void SearchRoom()
    {
        aiPathScript.isStopped = false;
        currentBehavior = MovementState.Searching;
        aiPathScript.maxSpeed = 9;
        waitTime = 0.5f;
        searchTime = 20.0f;
    }


    /*
     * Makes cat wander around aimlessly
     */
    public void WanderAround()
    {
        aiPathScript.isStopped = false;
        currentBehavior = MovementState.Wandering;
        aiPathScript.maxSpeed = 5;
        waitTime = 2.0f;
    }
}
