using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerHealth : MonoBehaviour, IAttackable {

    Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    public void Defend(float damage) {
        anim.SetTrigger("hit");
    }
}
