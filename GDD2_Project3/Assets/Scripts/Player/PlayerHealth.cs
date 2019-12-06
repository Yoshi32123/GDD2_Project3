using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class PlayerHealth : MonoBehaviour
{
    // fields
    private int time;
    private int previousTime;
    private float actualTime;
    private int timeTracker;
    private int health;
    private bool justHit = false;
    public GameObject catHitDetector;

    [Header("UI References")]
    [SerializeField] public GameObject block1;
    [SerializeField] public GameObject block2;
    [SerializeField] public GameObject block3;
    [SerializeField] public Sprite filled;
    [SerializeField] public Sprite empty;

    public bool JustHit
    {
        get { return justHit; }
    }

    // Start is called before the first frame update
    void Start()
    {
        previousTime = 0;
        timeTracker = 0;
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        actualTime += Time.deltaTime;

        if (justHit)
        {
            DamageCooldown();
            UpdateUI();
        }
    }

    /// <summary>
    /// updates fields once based on getting hit
    /// </summary>
    public void TakeDamage()
    {
        if (!justHit)
        {
            time = 0;
            previousTime = 0;
            justHit = true;
            health--;
            gameObject.GetComponent<PlayerStamina>().ResetStamina();
        }
    }

    /// <summary>
    /// Tracks time and resets the time tracker and associated fields
    /// </summary>
    private void DamageCooldown()
    {
        // time updates
        time = (int)actualTime;
        if (time != previousTime)
            timeTracker++;
        previousTime = time;

        // reset vignette and become damageable again after a few seconds
        if (timeTracker >= 4)
        {
            timeTracker = 0;
            justHit = false;
            catHitDetector.GetComponent<CatHitDetection>().damaged = false;
        }
    }

    /// <summary>
    /// Updates the health bar in the UI
    /// </summary>
    private void UpdateUI()
    {
        if (health == 2)
        {
            block3.GetComponent<Image>().overrideSprite = empty;
        }
        else if (health == 1)
        {
            block2.GetComponent<Image>().overrideSprite = empty;
        }
        else if (health == 0)
        {
            block1.GetComponent<Image>().overrideSprite = empty;
        }
        else if (health < 0)
        {
            // go to game over scene

        }
    }
}
