using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class PlayerStatus : MonoBehaviour, IAttackable {

    [Header("Energy Settings")]
    [SerializeField] float energyPerSecond = 20 / 3f;
    [SerializeField] float energyMax = 100f;
    [SerializeField] Slider manaBar;
    [SerializeField] Image projectileCooldownImg;
    [HideInInspector] public float projectileCost;
    float _energy = 100;
    Animator anim;    

    public float Energy {
        get { return _energy; }
        set {
            _energy = Mathf.Clamp(value, 0, energyMax);
            UpdateManaBar();
            UpdateProjectileCooldown();
        }
    }

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Start() {
        InitManaBar();
        Energy = Energy; // To call the "set" accessor
        StartCoroutine(EnergyRefill());
    }

    IEnumerator EnergyRefill() {
        WaitForSeconds wait = new WaitForSeconds(.5f);
        while (true) {
            Energy += energyPerSecond/2f;
            UpdateManaBar();
            yield return wait;
        }
    }

    void UpdateManaBar() {
        manaBar.value = Energy;
    }

    void InitManaBar() {
        manaBar.minValue = 0;
        manaBar.maxValue = 100;
    }

    void UpdateProjectileCooldown() {
        if (HasEnoughEnergy(projectileCost))
            projectileCooldownImg.enabled = false;
        else
            projectileCooldownImg.enabled = true;
    }

    public bool HasEnoughEnergy(float cost) {
        return cost <= Energy;
    }

    public void Defend(float damage) {
        anim.SetTrigger("hit");
    }
}
