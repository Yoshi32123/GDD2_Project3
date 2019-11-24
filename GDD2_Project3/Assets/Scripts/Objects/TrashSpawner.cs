using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> pref_trash;
    [SerializeField] int numTrash = 5;
    GameObject[] spawnpoints;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoints = GameObject.FindGameObjectsWithTag("TrashSpawnPoint");

        for(int i = 0; i < numTrash; i++)
        {
            int trashType = Random.Range(0, pref_trash.Count);
            int spawnpoint = Random.Range(0, spawnpoints.Length);

            Instantiate(pref_trash[trashType], spawnpoints[spawnpoint].transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
