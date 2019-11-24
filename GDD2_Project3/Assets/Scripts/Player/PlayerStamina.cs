using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerStamina : MonoBehaviour
{
    private int stamina;
    private int time;
    private int previousTime;
    private float actualTime;

    // Start is called before the first frame update
    void Start()
    {
        stamina = 5;
        previousTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        UpdateStamina();

        // if stamina is out, turn off walk
        if (stamina == 0)
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 1.75f;
        }
        else
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 3.5f;
        }

        //Debug.Log("Current stamina: " + stamina);
    }


    public void UpdateStamina()
    {
        time = (int)actualTime;

        if (time != previousTime)
        {
            if (gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina < 5)
                stamina++;
            else if (!gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina > 0)
                stamina--;
        }

        previousTime = time;
    }
}
