using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BossCtrlr : MonoBehaviour, IAttackable {

    [Header("Projectile settings")]
    [SerializeField] ThrowProjectileBhvr throwProjectileBhvr;
    [Header("Salve settings")]
    [SerializeField] SalveBhvr salveBhvr;
    [Header("Falling object settings")]
    [SerializeField] FallingObjectBhvr fallingObjectBhvr;
    [Header("Collapsing settings")]
    [SerializeField] CollapsingBhvr collapsingBhvr;

    int phase = 0;
    Animator anim;
    float lastSalve = 0f;
    Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        phase++;
    }

    void Update() {
        switch (phase) {
            case 1:
                phase++;
                StartCoroutine(PhaseProjectile());
                break;
            case 3:
                phase++;
                StartCoroutine(PhaseSalve());
                break;
            case 5:
                phase++;
                StartCoroutine(PhaseFallingObject());
                break;
            case 7:
                StartCoroutine(PhaseCollapsing());
                phase++;
                break;
            case 9:
                phase = 1;
                break;
        }
    }

    public void Defend(float damage) {
        Debug.Log(this.name + " defends " + damage + " dmg");
        anim.SetTrigger("hit");
    }

    IEnumerator PhaseProjectile() {
        yield return new WaitForSeconds(2f);
        int count = 0;
        while (count < 3) {
            Vector3 target = new Vector3(player.position.x, throwProjectileBhvr.projectileSpawnPos.position.y, player.position.z);
            Quaternion direction = Quaternion.LookRotation(target - throwProjectileBhvr.projectileSpawnPos.position);
            throwProjectileBhvr.StartBehaviour(direction);
            count++;
            yield return new WaitForSeconds(2f);
        }
        phase++;
    }

    IEnumerator PhaseSalve() {
        yield return new WaitForSeconds(1f);
        salveBhvr.StartBehaviour();
        yield return new WaitForSeconds(6f);
        phase++;
    }

    IEnumerator PhaseFallingObject() {
        yield return new WaitForSeconds(2f);
        int count = 0;
        while (count < 5) {
            fallingObjectBhvr.StartBehaviour(player.position);
            count++;
            yield return new WaitForSeconds(1f);
        }
        phase++;
    }

    IEnumerator PhaseCollapsing() {
        yield return new WaitForSeconds(3f);
        collapsingBhvr.StartBehaviour();
        yield return new WaitForSeconds(8f);
        phase++;
    }

}
