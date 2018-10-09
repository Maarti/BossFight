using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class BossCtrlr : MonoBehaviour, IAttackable {

    [Header("Life Settings")]
    [SerializeField] float lifeMax = 50f;
    [SerializeField] Slider lifeBar;
    [Header("Projectile settings")]
    [SerializeField] ThrowProjectileBhvr throwProjectileBhvr;
    [Header("Salve settings")]
    [SerializeField] SalveBhvr salveBhvr;
    [Header("Falling object settings")]
    [SerializeField] FallingObjectBhvr fallingObjectBhvr;
    [Header("Collapsing settings")]
    [SerializeField] CollapsingBhvr collapsingBhvr;
    [Header("Energy Drop settings")]
    [SerializeField] EnergyDropBhvr energyDropBhvr;
    
    int phase = 0;
    Animator anim;
    float lastSalve = 0f;
    Transform player;
    float _life = 50;

    public float Life {
        get { return _life; }
        set {
            _life = Mathf.Clamp(value, 0, lifeMax);
            UpdateLifeBar();
            if (_life <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        InitLifeBar();
        Life = lifeMax;
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
                phase++;
                StartCoroutine(PhaseFallingObject());
                StartCoroutine(PhaseCollapsing());
                break;
            case 10:
                phase = 1;
                break;
        }
    }

    public void Defend(Attack attack) {
        Life -= attack.damage;
        energyDropBhvr.StartBehaviour(attack);
        anim.SetTrigger("hit");
    }

    void InitLifeBar() {
        lifeBar.minValue = 0;
        lifeBar.maxValue = lifeMax;
    }

    void UpdateLifeBar() {
        lifeBar.value = Life;
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
