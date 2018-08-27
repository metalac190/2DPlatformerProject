using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    // config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;

    // state
    bool isAlive = true;

    // cached component references
    Rigidbody2D rigidbody2D;
    Animator animator;
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider2D;
    float gravityScaleAtStart;

    // messages then methods
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
    }

    private void Update()
    {
        Run();
        ClimbLadder();
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

    private void ClimbLadder()
    {
        // we're not touching a climbing layer, don't even calculate ladder climb
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            animator.SetBool("Climbing", false);
            rigidbody2D.gravityScale = gravityScaleAtStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(rigidbody2D.velocity.x, controlThrow * climbSpeed);
        rigidbody2D.velocity = climbVelocity;
        rigidbody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidbody2D.velocity.y) > Mathf.Epsilon;
        animator.SetBool("Climbing", playerHasVerticalSpeed);
    }

    private void Jump()
    {
        if (!feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

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
