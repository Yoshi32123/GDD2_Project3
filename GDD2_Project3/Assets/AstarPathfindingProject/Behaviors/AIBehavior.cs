using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] CharacterController cat;
    [SerializeField] float visionAngle = 1.5f; // peripheral vision
    [SerializeField] float visionRange = 15.0f; // how far the cat can see
    private bool seesPlayer = false; // how far the cat can see
    public bool doesCatSeePlayer() { return seesPlayer; }

    public AIPath aiPathScript;
    public AIDestinationSetter aiDestinationSetterScript;
    public GameObject motherNode;
    public GameObject eyesPosition;
    private List<GameObject> listOfRooms;

    Vector3 direction;
    Vector3 velocity;
    private float searchTime = 0.0f;
    private float waitTime = 0.0f;
    private float timer = 0.0f;
    private enum MovementState { Wandering, Chasing, Searching};
    private MovementState currentBehavior;
    private int currentRoomIndex;
    private CharacterController charContr;
    public bool floorDetected;

    private bool jumping = false;
    private readonly float initialJumpSpeed = 0.15f;
    private Vector3 gravity = new Vector3(0, -0.045f, 0);
    private Vector3 gravVelocity = new Vector3(0, 0, 0);

    private Transform knownPlayerPosition;

    //public CatCharacter 

    // Start is called before the first frame update
    void Start()
    {
        knownPlayerPosition = new GameObject().transform;
        jumping = false;
        charContr = GetComponent<CharacterController>();

        //ChasePlayer();
        WanderAround();
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
       
        if (seesPlayer = CheckPlayerVisible())
        {
            if (currentBehavior != MovementState.Chasing)
                //Changes mode to chasing player when player is seen
                ChasePlayer();
        }
        else
        {
            if (currentBehavior == MovementState.Chasing)
                //Will switch to room searching if the cat loses the player
                //while chasing.
                //
                //Will also switch to wandering after time has passed in the Searching method automatically
                SearchRoom();
        }

        switch (currentBehavior)
        {
            case MovementState.Chasing:
                Chasing();
                break;

            case MovementState.Searching:
                Searching();
                break;

            case MovementState.Wandering:
                Wandering();
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
        aiPathScript.maxSpeed = 1.5f;
    }

    /*
     * Has the cat chase the player via pathfinding but 
     * switches to directly running at player when they are close enough
     */
    private void Chasing()
    {
        knownPlayerPosition.position = player.transform.position;
        aiDestinationSetterScript.target = knownPlayerPosition;


        //attempts to jump onto obstacle in its way
        TryJumping(aiPathScript.steeringTarget);
        
        //If the cat is very close to player, go offpath for the kill
        if ((knownPlayerPosition.position - transform.position).magnitude <= 4f)
        {
            
            aiPathScript.isStopped = true;

            //directon to player
            direction = (player.transform.position - transform.position);
            direction.y = 0;
            direction = direction.normalized;

            //error check for if player is directly above
            if (direction != Vector3.zero && Time.timeScale != 0)
            {
                //turns the cat towards the player
                transform.forward = Vector3.Slerp(direction, transform.forward, 0.94f * (1/Time.timeScale));
            }

            //set direction to where the cat is actually facing
            direction = transform.forward;
            direction.y = 0;

            //strip the Y position
            Vector3 horizontalPlayerPos = knownPlayerPosition.position;
            horizontalPlayerPos.y = 0.0f;
            Vector3 horizontalCatPos = transform.position;
            horizontalCatPos.y = 0.0f;
            float horizontalDistance = (horizontalCatPos - horizontalPlayerPos).magnitude;

            //Move in that direction so the cat isn't sliding sidewards slightly
            //Slows down as it gets closer to player. 
            velocity = direction * (aiPathScript.maxSpeed / 26f) * ((horizontalDistance - 1f)/5.0f);

            //tries to jump to player if player is high
            TryJumping(player.transform.position);


            //makes it so the cat stops moving when it just barely 
            //touches player so it doesn't glitch out from the forced collisions
            if (horizontalDistance > 1.0f)
            {
                cat.Move(velocity);
            }
        }

        //player is too far, go back to pathfinding to find next closest point
        else
        {
            aiPathScript.isStopped = false;
        }
    }


    /*
     * Makes cat move around the room looking for the player
     */
    public void SearchRoom()
    {
        aiPathScript.isStopped = false;
        currentBehavior = MovementState.Searching;
        aiPathScript.maxSpeed = 0.5f;

        // Cross reference for animation walking


        waitTime = 0.5f;
        searchTime = 20.0f;

    }

    /*
     * Cat will pick various nodes in the room it is in and 
     * just wander around quickly looking for the player.
     */
    private void Searching()
    {
        searchTime -= Time.deltaTime;

        //attempts to jump onto obstacle in its way
        TryJumping(aiPathScript.steeringTarget);

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
    }


    /*
     * Makes cat wander around aimlessly
     */
    public void WanderAround()
    {
        aiPathScript.isStopped = false;
        currentBehavior = MovementState.Wandering;
        aiPathScript.maxSpeed = 1;
        waitTime = 2.0f;
    }

    /*
     * Cat will pick random nodes from throughout stage and wander slowly to it
     */
    private void Wandering()
    {
        //attempts to jump onto obstacle in its way
        TryJumping(aiPathScript.steeringTarget);

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
    }


    /*
     * Will check if it needs to jump to reach its current target passed in
     */
    private void TryJumping(Vector3 targetPos)
    {
        //direction to next node (not normalized)
        direction = (targetPos - transform.position);

        //Needs to jump so it jumps because it can.
        if (direction.y > 0.8f && !jumping)
        {
            gravVelocity = new Vector3(0, initialJumpSpeed, 0);
            jumping = true;
        }

        //Jumped but is now back on ground so reset jump
        if (jumping && Time.timeScale != 0)
        {
            gravVelocity += gravity * Time.deltaTime * (1 / Time.timeScale);
            charContr.Move(gravVelocity);


            if (charContr.velocity.y <= 1f && floorDetected)
            {
                gravVelocity = new Vector3(0, 0, 0);
                jumping = false;
            }
        }

    }

    /// <summary>
    /// checks if player is within visible range and if any obstacles are between cat and player
    /// </summary>
    /// <returns></returns>
    bool CheckPlayerVisible()
    {
        //add a small amount to player position so cat can see player's head
        Vector3 playerPos = player.transform.position + new Vector3(0.0f, 0.39f, 0.0f);
        Vector3 catToPlayer = playerPos - eyesPosition.transform.position;

        //get angle between cat and player
        float theta = Mathf.Acos(Vector3.Dot(eyesPosition.transform.forward, catToPlayer) / (eyesPosition.transform.forward.magnitude * catToPlayer.magnitude));

        //if player is within the peripheral vision of the cat and within the visual range of the cat
        if (theta < visionAngle / 2 && catToPlayer.sqrMagnitude < visionRange * visionRange)
        {
            //cast a ray from cat to player's head
            RaycastHit raycastHit;
            Physics.Raycast(eyesPosition.transform.position, catToPlayer.normalized, out raycastHit, catToPlayer.magnitude);

            //if the ray hits anything other than the player or cat, return false
            if (raycastHit.collider != null && raycastHit.collider.gameObject.tag != "Player" && raycastHit.collider.gameObject.tag != "Cat")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

}
