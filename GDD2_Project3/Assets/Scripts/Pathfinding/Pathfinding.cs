using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] List<GameObject> path;
    [SerializeField] float minRangeToArrive = 1.0f;
    [SerializeField] float speed = 1.0f;

    [SerializeField] float visionAngle = 1.0f;
    [SerializeField] float visionRange = 5.0f;

    [SerializeField] GameObject player;

    GameObject target;

    Vector3 position;
    Vector3 direction;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        target = path[0];
    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs((target.transform.position - transform.position).sqrMagnitude) < minRangeToArrive * minRangeToArrive)
        //{
        //    if(target == path[path.Count - 1])
        //        target = path[0];
        //    else
        //        target = path[path.IndexOf(target) + 1];
        //}

        //direction = (target.transform.position - transform.position).normalized;
        //velocity = direction * speed;
        //position += velocity;
        //transform.position = position;

        //transform.LookAt(target.transform.position);
        //transform.Rotate(0, 180, 0);

        CheckPlayerVisible();
    }

    bool CheckPlayerVisible()
    {
        Vector3 catToPlayer = transform.position - player.transform.position;
        float theta = Mathf.Acos(Vector3.Dot(transform.forward, catToPlayer) / (transform.forward.magnitude * catToPlayer.magnitude));

        Debug.DrawLine(transform.position, transform.position + transform.forward * 10);

        if (theta < visionAngle / 2)
        {
            Debug.Log(theta);
        }

        return false;
    }
}
