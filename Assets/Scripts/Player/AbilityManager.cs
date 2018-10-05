using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour {

    [SerializeField] Ability ability1;
    [SerializeField] Ability ability2;

    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ability1.UseAbility();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            ability2.UseAbility();
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
