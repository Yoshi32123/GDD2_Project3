using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerStamina2 : MonoBehaviour
{
    private float stamina;

    [Header("UI Reference")]
    [SerializeField] public GameObject StaminaMeter;
    private Slider staminaValue;

    [SerializeField] private float displayStamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = 5;
        staminaValue = StaminaMeter.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStamina();
        staminaValue.value = stamina;
        displayStamina = stamina;

        // if stamina is out, turn off walk
        if (stamina <= .04f)
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 1.75f;
        }
        else
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 3.5f;
        }

        //Debug.Log("Current stamina: " + stamina);
    }

    /// <summary>
    /// Updates stamina based on time that the player is running
    /// </summary>
    public void UpdateStamina()
    {
        // have preset stamina depletion and regen rates
        if (gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina < 5)
            stamina += .04f;
        else if (!gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina > 0)
            stamina -= .025f;
    }

    /// <summary>
    /// Resets the stamina
    /// </summary>
    public void ResetStamina()
    {
        stamina = 5;
    }
}
