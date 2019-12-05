using UnityEngine;
using System.Collections;

public class CatCharacter : MonoBehaviour
{
    Animator catAnimator;
    public bool jumpStart = false;
    public float groundCheckDistance = 0.6f;
    public float groundCheckOffset = 0.01f;
    public bool isGrounded = true;
    public float jumpSpeed = 1f;
    Rigidbody catRigid;
    public float forwardSpeed;
    public float turnSpeed;
    public float walkMode = 1f;
    public float jumpStartTime = 0f;
    public float maxWalkSpeed = 1f;

    void Start()
    {
        catAnimator = GetComponent<Animator>();
        catRigid = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        CheckGroundStatus();
        Move();
        jumpStartTime += Time.deltaTime;
        maxWalkSpeed = Mathf.Lerp(maxWalkSpeed, walkMode, Time.deltaTime);
    }

    public void Attack()
    {
        catAnimator.SetTrigger("Attack");
    }

    public void Hit()
    {
        catAnimator.SetTrigger("Hit");
    }

    public void Death()
    {
        catAnimator.SetBool("IsLived", false);
    }

    public void Rebirth()
    {
        catAnimator.SetBool("IsLived", true);
    }

    public void StandUp()
    {
        catAnimator.SetTrigger("StandUp");
    }

    public void SitDown()
    {
        catAnimator.SetTrigger("SitDown");
    }

    public void LieDown()
    {
        catAnimator.SetTrigger("LieDown");
    }

    public void Sleep()
    {
        catAnimator.SetTrigger("Sleep");
    }

    public void WakeUp()
    {
        catAnimator.SetTrigger("WakeUp");
    }

    public void Roar()
    {
        catAnimator.SetTrigger("Roar");
    }

    public void Gallop()
    {
        walkMode = 4f;
    }

    public void Canter()
    {
        walkMode = 3f;
    }

    public void Trot()
    {
        walkMode = 2f;
    }

    public void Walk()
    {
        walkMode = 1f;
    }

    public void Jump()
    {
        if (isGrounded)
        {
            catAnimator.SetTrigger("Jump");
            jumpStart = true;
            jumpStartTime = 0f;
            isGrounded = false;
            catAnimator.SetBool("IsGrounded", false);
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
        isGrounded = Physics.Raycast(transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);

        if (jumpStart)
        {
            if (jumpStartTime > .25f)
            {
                jumpStart = false;
                catRigid.AddForce((transform.up + transform.forward * forwardSpeed) * jumpSpeed, ForceMode.Impulse);
                catAnimator.applyRootMotion = false;
                catAnimator.SetBool("IsGrounded", false);
            }
        }

        if (isGrounded && !jumpStart && jumpStartTime > .5f)
        {
            catAnimator.applyRootMotion = true;
            catAnimator.SetBool("IsGrounded", true);
        }
        else
        {
            if (!jumpStart)
            {
                catAnimator.applyRootMotion = false;
                catAnimator.SetBool("IsGrounded", false);
            }
        }
    }

    public void Move()
    {
        catAnimator.SetFloat("Forward", forwardSpeed);
        catAnimator.SetFloat("Turn", turnSpeed);
    }
}
