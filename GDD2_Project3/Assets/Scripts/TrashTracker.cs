using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTracker : MonoBehaviour
{
    [SerializeField] List<GameObject> trash = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckEmpty();
    }

    public void CheckEmpty()
    {
        for (int i = 0; i < trash.Count; i++)
        {
            if (trash[i] == null)
            {
                trash.RemoveAt(i);
                i--;
            }
        }
    }
}
