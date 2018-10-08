using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching

	const float k_GroundedRadius = .3f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
    private bool m_Falling = false;       // whether the player is currently moving upward in jump
	const float k_CeilingRadius = .3f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent OnJump = new UnityEvent();
	public UnityEvent OnLand = new UnityEvent();
    public UnityEvent OnFalling = new UnityEvent();

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	public BoolEvent OnCrouch = new BoolEvent();
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
    {
        // Store previous ground state and reset, so that we can test if landed
        bool wasGrounded = m_Grounded;
        m_Grounded = false;
        // Store previous fall state and reset, so that we can test if falling
        bool wasFalling = m_Falling;
        m_Falling = false;

        CheckIfFalling(wasFalling);
        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        DetectGrounded(wasGrounded);
    }

    public void Move(float move, bool crouch, bool jump)
    {
        // determine if we are able to stand up from crouch
        crouch = CheckStandFromCrouch(crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // recalculate move based on crouch state
            move = CrouchMovement(move, crouch);
            ApplyMovement(move);
            // check player direction reverse
            FlipPlayer(move);
        }
        // If the player should jump...
        PlayerJumpIfPressed(jump);
    }

    private void CheckIfFalling(bool wasFalling)
    {
        if(m_Rigidbody2D.velocity.y < 0)
        {
            m_Falling = true;
            // if we are now falling and weren't previously, activate new fall state
            if(m_Falling && !wasFalling)
            {
                Debug.Log("Falling");
                OnFalling.Invoke();
            }
        }
        else
        {
            m_Falling = false;
        }
    }

    private void ApplyMovement(float move)
    {
        // Move the character by finding the target velocity
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
    }

    private void DetectGrounded(bool wasGrounded)
    {
        // use spheres to detect ground objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            // if we found a gameObject with the "Ground" layer attached
            if (colliders[i].gameObject != gameObject)
            {
                // we are touching ground
                m_Grounded = true;
                // make sure we specify that we must be falling in order to successfully land
                if (!wasGrounded && m_Falling)
                {
                    Debug.Log("Landed");
                    OnLand.Invoke();
                }
            }
        }
    }

    private float CrouchMovement(float move, bool crouch)
    {
        // If crouching
        if (crouch)
        {
            // if we were already crouching
            if (!m_wasCrouching)
            {
                m_wasCrouching = true;
                OnCrouch.Invoke(true);
            }

            // Reduce the speed by the crouchSpeed multiplier
            move *= m_CrouchSpeed;

            // Disable one of the colliders when crouching
            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = false;
        }
        else
        {
            // Enable the collider when not crouching
            if (m_CrouchDisableCollider != null)
                m_CrouchDisableCollider.enabled = true;

            if (m_wasCrouching)
            {
                m_wasCrouching = false;
                OnCrouch.Invoke(false);
            }
        }

        return move;
    }

    private bool CheckStandFromCrouch(bool crouch)
    {
        if (!crouch)
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
                crouch = true;
            }
        }

        return crouch;
    }

    private void FlipPlayer(float move)
    {
        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && m_FacingRight)
        {
            // ... flip the player.
            Flip();
        }
    }

    private void PlayerJumpIfPressed(bool jump)
    {
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            OnJump.Invoke();
        }
    }

    private void Flip()
	{
		// Switch the way the player is facing.
		m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
	}
}
