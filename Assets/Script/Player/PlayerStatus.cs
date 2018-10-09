using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PlayerStatus : MonoBehaviour, IAttackable {

    [Header("Energy Settings")]
    [SerializeField] float energyPerSecond = 20 / 3f;
    [SerializeField] float energyMax = 100f;
    [SerializeField] Slider manaBar;
    [SerializeField] Image projectileCooldownImg;
    [HideInInspector] public float projectileCost = -1;
    [Header("Life Settings")]
    [SerializeField] float lifeMax = 100f;
    [SerializeField] Slider lifeBar;

    float _energy = 100;
    float _life = 100;
    Animator anim;

    public float Energy {
        get { return _energy; }
        set {
            _energy = Mathf.Clamp(value, 0, energyMax);
            UpdateManaBar();
            UpdateProjectileCooldown();
        }
    }

    public float Life {
        get { return _life; }
        set {
            _life = Mathf.Clamp(value, 0, lifeMax);
            UpdateLifeBar();
            if (_life <= 0)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Start() {
        InitLifeBar();
        InitManaBar();
        Life = lifeMax;
        Energy = energyMax; // To call the "set" accessor
        StartCoroutine(EnergyRefill());
    }

    public void CollidWithEnergyBall(float amountPercentage) {
        float amountRaw = (amountPercentage / 100f) * energyMax;
        Energy += amountRaw;
    }

    IEnumerator EnergyRefill() {
        WaitForSeconds wait = new WaitForSeconds(.5f);
        while (true) {
            Energy += energyPerSecond / 2f;
            UpdateManaBar();
            yield return wait;
        }
    }

    void UpdateManaBar() {
        manaBar.value = Energy;
    }

    void InitManaBar() {
        manaBar.minValue = 0;
        manaBar.maxValue = energyMax;
    }

    void UpdateLifeBar() {
        lifeBar.value = Life;
    }

    void InitLifeBar() {
        lifeBar.minValue = 0;
        lifeBar.maxValue = lifeMax;
    }

    void UpdateProjectileCooldown() {
        if (projectileCost > 0 && !HasEnoughEnergy(projectileCost))
            projectileCooldownImg.enabled = true;
        else
            projectileCooldownImg.enabled = false;
    }

    public bool HasEnoughEnergy(float cost) {
        return cost <= Energy;
    }

    public void Defend(Attack attack) {
        Life -= attack.damage;
        anim.SetTrigger("hit");
    }
}
