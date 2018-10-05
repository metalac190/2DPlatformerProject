using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rigidbody2D;


	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (IsFacingRight())
        {
            rigidbody2D.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            rigidbody2D.velocity = new Vector2(-moveSpeed, 0f);
        }
	}

    bool IsFacingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(rigidbody2D.velocity.x)), 1f);
    }
}
