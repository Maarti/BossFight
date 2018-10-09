using UnityEngine;

public class Attack {

    public AttackType attackType;
    public float damage = 0f;

    public Attack(AttackType attackType, float damage) {
        this.attackType = attackType;
        this.damage = damage;
    }
}
