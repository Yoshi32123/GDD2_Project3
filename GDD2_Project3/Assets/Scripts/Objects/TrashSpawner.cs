using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    List<GameObject> trash = new List<GameObject>();
    [SerializeField] List<GameObject> pref_trash;
    [SerializeField] int numTrash = 5;
    GameObject[] spawnpoints;

    [SerializeField] OverlayToggle overlayScript;

    // use this to determine when player wins
    public int TrashCount
    {
        get { return trash.Count; }
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("TrashSpawnPoint");

        List<int> usedSpawns = new List<int>();
        for(int i = 0; i < numTrash; i++)
        {
            int trashType = Random.Range(0, pref_trash.Count);
            int spawnpoint;
            do{
                spawnpoint = Random.Range(0, spawnpoints.Length);
            } while (usedSpawns.Contains(spawnpoint));
            usedSpawns.Add(spawnpoint);

            trash.Add(Instantiate(pref_trash[trashType], spawnpoints[spawnpoint].transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckEmpty();
        Debug.Log(spawnpoints);
    }

    /// <summary>
    /// Checks for a null spot in garbage
    /// </summary>
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
            overlayScript.WonGame();
        }
    }
}
