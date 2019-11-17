using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrash : MonoBehaviour
{
    [SerializeField] GameObject effects = null;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            Instantiate(effects, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
        }
    }
}
