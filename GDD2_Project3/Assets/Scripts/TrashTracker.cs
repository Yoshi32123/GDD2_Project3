using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashTracker : MonoBehaviour
{
    [SerializeField] List<GameObject> trash = null;

    // use this to determine when player wins
    public int TrashCount
    {
        get { return trash.Count; }
    }

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

        if (TrashCount == 0)
        {
            // win game here

        }
    }
}
