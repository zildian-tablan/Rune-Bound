using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;
using System;


[RequireComponent(typeof(Rigidbody2D),typeof(TouchingDirections),typeof(Damageable))]//you cant add this component to the game object if it does not have a Rigidbody2D component

public class PlayerController : MonoBehaviour
{

    public float walkSpeed = 5f;
    public float airwalkSpeed = 3f;
    public float runSpeed = 10f;
    public float jumpImpulse = 7f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    public float CurrentRunSpeed  {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else
                    {
                        return airwalkSpeed;
                    }
                }
                else
                {
                    //idle speed
                    return 0;
                }
            }
            else
            {
                //move locked
                return 0;
            }
           
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get
       {
            return _isFacingRight;
        } private set
        {
            if(_isFacingRight != value)
            {
               //flip
               transform.localScale *= new Vector2(-1, 1);
            }

            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;
    Attack attack;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        attack = GetComponent<Attack>();
        if (attack == null)
        {
            attack = GetComponentInChildren<Attack>();
        }
    }

    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Debug.Log("isMoving: " + IsMoving + ", isRunning: " + IsRunning);
    //}

    private void FixedUpdate()
    {
        if(!damageable.LockVelocity)
        {
            rb.linearVelocity = new Vector2(moveInput.x * CurrentRunSpeed, rb.linearVelocity.y);
        }
        
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);

       
    }



    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
        
    }

    private void setFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            //right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            //jump
            animator.SetTrigger(AnimationStrings.jumpTrigger);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            //attack
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }

    }
    public void OnRangeAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (attack != null)
            {
                attack.TryActivateSkill1();
            }
            else
            {
                Debug.LogWarning("RangeAttack input received but Attack component is missing.");
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {

        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y);
    }

}
