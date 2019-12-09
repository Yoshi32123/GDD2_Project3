using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

public class PlayerStamina2 : MonoBehaviour
{
    private float stamina;
    private float time;
    private float previousTime;
    private float actualTime;

    [Header("UI References")]
    [SerializeField] public GameObject block1;
    [SerializeField] public GameObject block2;
    [SerializeField] public GameObject block3;
    [SerializeField] public GameObject block4;
    [SerializeField] public GameObject block5;
    [SerializeField] public Sprite filled;
    [SerializeField] public Sprite empty;
    [SerializeField] public GameObject StaminaMeter;
    private Slider staminaValue;

    [SerializeField] private float displayStamina;

    // Start is called before the first frame update
    void Start()
    {
        stamina = 5;
        previousTime = 0;
        staminaValue = StaminaMeter.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;
        UpdateStamina();
        staminaValue.value = stamina;

        // if stamina is out, turn off walk
        if (stamina <= .1f)
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 1.75f;
        }
        else
        {
            gameObject.GetComponent<FirstPersonController>().m_RunSpeed = 3.5f;
        }
        displayStamina = stamina;
        //Debug.Log("Current stamina: " + stamina);
    }

    /// <summary>
    /// Updates stamina based on time that the player is running
    /// </summary>
    public void UpdateStamina()
    {
        time = actualTime;
        float diff = time - previousTime;
        if (time != previousTime)
        {
            if (gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina < 5)
                stamina += .05f;
            else if (!gameObject.GetComponent<FirstPersonController>().m_IsWalking && stamina > 0)
                stamina -= .05f;
        }

        previousTime = time;
    }

    /// <summary>
    /// Updates the UI elements for the stamina reference
    /// </summary>
    public void UpdateStaminaUI()
    {

        if (stamina == 5)
        {
            block1.GetComponent<Image>().overrideSprite = filled;
            block2.GetComponent<Image>().overrideSprite = filled;
            block3.GetComponent<Image>().overrideSprite = filled;
            block4.GetComponent<Image>().overrideSprite = filled;
            block5.GetComponent<Image>().overrideSprite = filled;

        }
        else if (stamina == 4)
        {
            block1.GetComponent<Image>().overrideSprite = filled;
            block2.GetComponent<Image>().overrideSprite = filled;
            block3.GetComponent<Image>().overrideSprite = filled;
            block4.GetComponent<Image>().overrideSprite = filled;
            block5.GetComponent<Image>().overrideSprite = empty;
        }
        else if (stamina == 3)
        {
            block1.GetComponent<Image>().overrideSprite = filled;
            block2.GetComponent<Image>().overrideSprite = filled;
            block3.GetComponent<Image>().overrideSprite = filled;
            block4.GetComponent<Image>().overrideSprite = empty;
        }
        else if (stamina == 2)
        {
            block1.GetComponent<Image>().overrideSprite = filled;
            block2.GetComponent<Image>().overrideSprite = filled;
            block3.GetComponent<Image>().overrideSprite = empty;
        }
        else if (stamina == 1)
        {
            block1.GetComponent<Image>().overrideSprite = filled;
            block2.GetComponent<Image>().overrideSprite = empty;
        }
        else
        {
            block1.GetComponent<Image>().overrideSprite = empty;
        }
    }

    /// <summary>
    /// Resets the stamina
    /// </summary>
    public void ResetStamina()
    {
        stamina = 5;
    }
}
