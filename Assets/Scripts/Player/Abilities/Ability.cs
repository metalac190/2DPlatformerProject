using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject {

    [SerializeField] string abilityName = "New Ability";
    [SerializeField] AudioClip sound;
    [SerializeField] float cooldown;

    public abstract void Initialize(GameObject obj);
    public abstract void Use();
}
