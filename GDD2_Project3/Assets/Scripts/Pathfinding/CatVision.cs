using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatVision : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float visionAngle = 1.5f; // peripheral vision
    [SerializeField] float visionRange = 15.0f; // how far the cat can see

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayerVisible();
    }

    bool CheckPlayerVisible()
    {
        Vector3 playerPos = player.transform.position + new Vector3(0.0f, 0.39f, 0.0f);
        Vector3 catToPlayer = playerPos - transform.position;
        float theta = Mathf.Acos(Vector3.Dot(transform.forward, catToPlayer) / (transform.forward.magnitude * catToPlayer.magnitude));

        Debug.DrawLine(transform.position, transform.position + transform.forward * visionRange);

        if (theta < visionAngle / 2 && catToPlayer.sqrMagnitude < visionRange * visionRange)
        {
            RaycastHit raycastHit;
            Physics.Raycast(transform.position, catToPlayer.normalized, out raycastHit, catToPlayer.magnitude);
            if(raycastHit.collider != null && raycastHit.collider.gameObject.tag != "Player")
            {
                return false;
            }
            else
            {
                Debug.Log("Whaddup, son?");
                return true;
            }
        }

        return false;
    }
}
