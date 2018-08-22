using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;

    // state
    bool isAlive = true;

    // cached component references
    Rigidbody2D rigidbody2D;
    Animator animator;

    // messages then methods
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Run();
        Jump();
        FlipSprite();
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); 
        // only affect horizontal movement, vertical is controlled by gravity
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        animator.SetBool("Running", playerHasHorizontalSpeed);
        
    }

    private void Jump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidbody2D.velocity += jumpVelocityToAdd;
        }
    }

    private void FlipSprite()
    {
        // if velocity is greater than 0, return true
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            // reverse current scaling of x axis
            transform.localScale = new Vector2(Mathf.Sign(rigidbody2D.velocity.x), 1f);
        }
    }

}
