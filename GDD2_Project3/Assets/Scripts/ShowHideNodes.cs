using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideNodes : MonoBehaviour
{
    public bool showNodes = false;
    private bool pastBool = false;
    public Material transparentMaterial;
    public Material[] visibleMaterial;


    // Start is called before the first frame update
    void Start()
    {
        SetMaterialOfNodes();
        pastBool = showNodes;
    }

    void Update()
    {

        if(pastBool != showNodes)
        {
            SetMaterialOfNodes();
        }

        pastBool = showNodes;
    }


    /*
     * Will show and hide the nodes based on whether the inspector value is set or not.
     * Will cycle through the different materials to help show the rooms.
     */
    private void SetMaterialOfNodes()
    {
        int index = 0;
        if (showNodes)
        {
            foreach (Transform room in transform)
            {
                if (index >= visibleMaterial.Length)
                {
                    index = 0;
                }

                foreach (Transform node in room)
                {
                    node.gameObject.GetComponent<Renderer>().material = visibleMaterial[index];
                }
                index++;
            }
        }
        else
        {
            foreach (Transform room in transform)
            {
                foreach (Transform node in room)
                {
                    node.gameObject.GetComponent<Renderer>().material = transparentMaterial;
                }
            }
        }
    }


}

