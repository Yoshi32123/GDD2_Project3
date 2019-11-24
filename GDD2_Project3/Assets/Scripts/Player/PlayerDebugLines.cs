using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebugLines : MonoBehaviour
{
    // fields
    [SerializeField] bool ToggleDebugLines;

    private Vector3 playerPos;
    private Vector3 playerForward;
    private Vector3 playerUp;
    private Vector3 playerRight;
    private float factor;

    // Start is called before the first frame update
    void Start()
    {
        ToggleDebugLines = false;

        playerPos = gameObject.transform.position;
        factor = 5.0f;

        playerForward = playerPos + factor * gameObject.transform.forward;
        playerUp = playerPos + factor * gameObject.transform.up;
        playerRight = playerPos + factor * gameObject.transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVectors();

        if (ToggleDebugLines)
        {
            DrawLines();
        }
    }

    /// <summary>
    /// Updates stored variables with objects stats
    /// </summary>
    public void UpdateVectors()
    {
        playerPos = gameObject.transform.position;

        playerForward = playerPos + factor * gameObject.transform.forward;
        playerUp = playerPos + factor * gameObject.transform.up;
        playerRight = playerPos + factor * gameObject.transform.right;
    }

    /// <summary>
    /// Draws debug lines for the forward, up, and right vectors
    /// </summary>
    public void DrawLines()
    {
        Debug.DrawLine(playerPos, playerForward, Color.blue);
        Debug.DrawLine(playerPos, playerUp, Color.green);
        Debug.DrawLine(playerPos, playerRight, Color.red);
    }
}
