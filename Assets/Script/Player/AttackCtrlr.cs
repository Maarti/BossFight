using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCtrlr : MonoBehaviour {

    [SerializeField] float damage;

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Boss") {
            other.GetComponent<IAttackable>().Defend(damage);
        }
    }

}
