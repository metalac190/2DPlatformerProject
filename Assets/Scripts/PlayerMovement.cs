using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
public class PlayerMovement : MonoBehaviour {

    CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool isJumping = false;
    bool isCrouching = false;

    private void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    void Update () {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
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

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, isCrouching, isJumping);
        // reset jump at end of update
        isJumping = false;
    }
}
