using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] List<GameObject> path;
    [SerializeField] float minRangeToArrive = 1.0f;
    [SerializeField] float speed = 1.0f;

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
        if (Mathf.Abs((target.transform.position - transform.position).sqrMagnitude) < minRangeToArrive * minRangeToArrive)
        {
            if(target == path[path.Count - 1])
                target = path[0];
            else
                target = path[path.IndexOf(target) + 1];
        }

        direction = (target.transform.position - transform.position).normalized;
        velocity = direction * speed;
        position += velocity;
        transform.position = position;

        transform.LookAt(target.transform.position);
        transform.Rotate(0, 180, 0);
    }
}
