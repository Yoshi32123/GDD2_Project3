using UnityEngine;
using System.Collections;

public class CatUserController : MonoBehaviour
{
    CatCharacter catCharacter;

    void Start()
    {
        catCharacter = GetComponent<CatCharacter>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            catCharacter.Attack();
        }
        if (Input.GetButtonDown("Jump"))
        {
            catCharacter.Jump();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            catCharacter.Hit();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            catCharacter.Death();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            catCharacter.Rebirth();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            catCharacter.StandUp();
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            catCharacter.SitDown();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            catCharacter.LieDown();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            catCharacter.Sleep();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            catCharacter.WakeUp();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            catCharacter.Roar();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            catCharacter.Gallop();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            catCharacter.Canter();
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            catCharacter.Trot();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            catCharacter.Walk();
        }

        catCharacter.forwardSpeed = catCharacter.maxWalkSpeed * Input.GetAxis("Vertical");
        catCharacter.turnSpeed = Input.GetAxis("Horizontal");
    }

}
