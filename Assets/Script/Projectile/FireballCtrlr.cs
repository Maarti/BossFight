using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCtrlr : AbstractProjectile {

    new void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
    }
}
