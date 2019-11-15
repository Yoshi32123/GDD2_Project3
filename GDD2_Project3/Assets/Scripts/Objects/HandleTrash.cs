using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleTrash : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Trash")
        {
            Destroy(collision.gameObject);
        }
    }
}
