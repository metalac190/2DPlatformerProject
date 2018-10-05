using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour, IProjectile {

    [SerializeField] float speed = 20f;
    [SerializeField] GameObject impactEffect;

    Rigidbody2D rigidbody;

    // Use this for initialization
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rigidbody.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageableObject = collision.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage();
        }

        OnImpact();
    }

    public void OnImpact()
    {
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        Destroy(gameObject);
    }
}
