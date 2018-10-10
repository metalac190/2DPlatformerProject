using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    CharacterController2D controller;
    Animator animator;

    public float runSpeed = 40f;
    //public float jumpHeight = 200f;
    //public float jumpExtraHeight = 30f;

    float horizontalMove = 0f;
    bool isAirborn = false;     // whether or not we are commanding player to jump this update
    bool isJumpingExtra = false;    // whether or not we applying extra jump force on this update
    bool isCrouching = false;   // whether or not we are commanding player to crouch this update


    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }
    
    private void OnEnable()
    {
        controller.OnLand.AddListener(HandleLand);
        controller.OnCrouch.AddListener(HandleCrouch);
        controller.OnFalling.AddListener(HandleFall);
    }

    private void OnDisable()
    {
        controller.OnLand.RemoveListener(HandleLand);
        controller.OnCrouch.RemoveListener(HandleCrouch);
        controller.OnFalling.AddListener(HandleFall);
    }

    void Update ()
    {
        CheckMovementInput();
        CheckInitialJumpInput();
        CheckExtraJumpInput();
        CheckCrouchInput();
    }

    private void FixedUpdate()
    {
        CommandCharacterMovement();
        ResetMoveStates();
    }

    private void ResetMoveStates()
    {
        isAirborn = false;
        isJumpingExtra = false;
    }

    private void CommandCharacterMovement()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isAirborn, isJumpingExtra);
    }

    private void CheckMovementInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
    }

    private void CheckInitialJumpInput()
    {
        if (Input.GetButtonDown("Jump") && !isAirborn)
        {
            animator.SetTrigger("Jump");
            isAirborn = true;
            animator.SetBool("IsAirborn", true);
        }
    }

    private void CheckExtraJumpInput()
    {
        if (Input.GetButton("Jump") && isAirborn)
        {
            isJumpingExtra = true;
        }
    }

    private void CheckCrouchInput()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
        }
    }

    void HandleLand()
    {
        animator.SetBool("IsAirborn", false);
    }

    void HandleCrouch(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void HandleFall()
    {
        animator.SetTrigger("Fall");
    }
}
