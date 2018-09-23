
using UnityEngine;

public class BossCtrlr : MonoBehaviour, IAttackable {

    Animator anim;

    void Start() {
        anim = GetComponent<Animator>();
    }

    public void Defend(float damage) {
        Debug.Log(this.name + " defends " + damage + " dmg");
        anim.SetTrigger("hit");
    }

   
}
