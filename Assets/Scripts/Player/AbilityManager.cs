using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;

    [SerializeField] Transform firePoint;
    public Transform FirePoint { get { return firePoint; } }

    private void Awake()
    {
        ability1.Initialize(gameObject);
        ability2.Initialize(gameObject);
    }

    void ChangeAbility(Ability newAbility)
    {
        ability1 = newAbility;
        ability1.Initialize(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ability1.Use();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ability2.Use();
        }
    }

    /*
    void PrimaryAttack()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    void SecondaryAttack()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
    */
}
