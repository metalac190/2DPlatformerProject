using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour {

    CharacterController2D characterController;
    Rigidbody2D rigidbody2D;
    Animator animator;

    bool isAirborn = false;
    bool isJumpingExtra = false;
    bool isCrouching = false;

    // Use this for initialization
    void Awake ()
    {
        FillReferences();
    }

    private void FillReferences()
    {
        characterController = GetComponent<CharacterController2D>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        characterController.OnLand.AddListener(OnLandCallback);
        characterController.OnJump.AddListener(OnJumpCallback);
        characterController.OnCrouch.AddListener(OnCrouchCallback);
        characterController.OnFall.AddListener(OnFallCallback);
        //characterController.OnDeath.AddListener(OnDeathCallback);
    }

    private void OnDisable()
    {
        characterController.OnLand.RemoveListener(OnLandCallback);
        characterController.OnJump.RemoveListener(OnJumpCallback);
        characterController.OnCrouch.RemoveListener(OnCrouchCallback);
        characterController.OnFall.RemoveListener(OnFallCallback);
    }

    void OnJumpCallback()
    {
        animator.SetTrigger("OnJump");
    }

    void OnLandCallback()
    {
        animator.SetTrigger("OnLand");
    }

    void OnCrouchCallback(bool isCrouching)
    {
        animator.SetBool("IsCrouching", isCrouching);
    }

    void OnFallCallback()
    {
        animator.SetTrigger("Fall");
    }
}
