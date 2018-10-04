using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour {

    // config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);


    public event Action OnLand = delegate { };
    public event Action OnJump = delegate { };
    public event Action OnDeath = delegate { };
    public event Action OnRunning = delegate { };

    // state
    bool isAlive = true;
    bool isGrounded = true;
    public bool HasHorizontalSpeed { get; private set; }

    // cached component references
    Rigidbody2D rigidbody2D;
    CapsuleCollider2D bodyCollider2D;
    BoxCollider2D feetCollider2D;
    float gravityScaleAtStart;

    // messages then methods
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        bodyCollider2D = GetComponent<CapsuleCollider2D>();
        feetCollider2D = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody2D.gravityScale;
    }

    private void Update()
    {
        if(!isAlive) { return; }
        CheckGrounded();
        //CheckHorizontalSpeed();
        Run();
        CheckJump();
        Die();
    }

    void CheckGrounded()
    {
        // using special foot collider to test ground
        if (feetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void Die()
    {
        if (bodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            rigidbody2D.velocity = deathKick;
            OnDeath.Invoke();
            GameSession.Instance.ProcessPlayerDeath();
        }
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); 
        // only affect horizontal movement, vertical is controlled by gravity
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody2D.velocity.y);
        rigidbody2D.velocity = playerVelocity;
    }

    private void CheckJump()
    {
        // we're not touching the ground layer, don't bother with jump
        if (!isGrounded)
        {
            return;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidbody2D.velocity += jumpVelocityToAdd;
            OnJump.Invoke();
        }
    }

    void Land()
    {
        OnLand.Invoke();
    }
}
