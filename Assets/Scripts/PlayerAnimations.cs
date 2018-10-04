using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour {
    /*
    [SerializeField] Player player;
    [SerializeField] Rigidbody2D playerRigidbody;

    Animator animator;

    // Use this for initialization
    void Awake ()
    {
        FillReferences();
    }

    private void FillReferences()
    {
        animator = GetComponent<Animator>();

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if (playerRigidbody == null)
        {
            player.GetComponent<Rigidbody>();
        }
    }

    private void OnEnable()
    {
        player.OnLand += HandleLand;
        player.OnJump += HandleJump;
        player.OnDeath += HandleDeath;
    }

    private void OnDisable()
    {
        player.OnLand -= HandleLand;
        player.OnJump -= HandleJump;
        player.OnDeath -= HandleDeath;
    }

    // Update is called once per frame
    void Update () {

    }

    void HandleDeath()
    {
        animator.SetTrigger("Dying");
    }

    void HandleRun(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }

    void HandleJump()
    {
        animator.SetTrigger("OnJump");
    }

    void HandleLand()
    {
        animator.SetTrigger("OnLand");
    }

    private void FlipSprite()
    {
        // reverse current scaling of x axis
        transform.localScale = new Vector2(Mathf.Sign(playerRigidbody.velocity.x), 1f);
    }
    */
}
