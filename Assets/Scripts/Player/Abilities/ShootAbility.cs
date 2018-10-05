using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Player/Abilities/Shoot")]
public class ShootAbility : Ability {

    [SerializeField] GameObject bulletPrefab;

    AbilityManager manager;
    [SerializeField] Transform firePoint;

    public override void Initialize(GameObject obj)
    {
        manager = obj.GetComponent<AbilityManager>();
        firePoint = manager.FirePoint;
    }

    public override void Use()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
