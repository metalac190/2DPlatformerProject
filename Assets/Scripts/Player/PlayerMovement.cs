using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour {

    CharacterController2D controller;
    Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool isJumping = false;
    bool isCrouching = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controller.OnLand.AddListener(HandleLand);
        controller.OnCrouch.AddListener(HandleCrouch);
    }

    private void OnDisable()
    {
        controller.OnLand.RemoveListener(HandleLand);
        controller.OnCrouch.RemoveListener(HandleCrouch);
    }

    void Update () {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            animator.SetBool("IsJumping", true);
        }
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
        animator.SetBool("IsJumping", false);
    }

    public void HandleCrouch(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        // reset jump at end of update
        isJumping = false;
    }
}
