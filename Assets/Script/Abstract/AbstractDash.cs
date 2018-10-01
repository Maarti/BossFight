using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbstractDash : MonoBehaviour {

    [Header("Dash settings")]
    [Tooltip("Speed of the player during dash")] public float speedMultiplier = 2f;
    [SerializeField] [Tooltip("Duration of the dash")] float duration = .2f;
    [SerializeField] [Tooltip("Total time of the cooldown")] float cooldown = 3f;
    [SerializeField] Image cdImg;
    [HideInInspector] public bool canDash = true;
    [HideInInspector] public float lastDash = -100f;
    [HideInInspector] public bool isDashing = false;
    bool onCooldown = false;

    public void StartBehaviour() {
        if (!onCooldown)
            StartCoroutine(Dash());
    }

    void Update() {
        if (onCooldown)
            UpdateUI();
    }

    IEnumerator Dash() {
        StartCoroutine(DashCooldown());
        isDashing = true;
        lastDash = Time.time;
        yield return new WaitForSeconds(duration);
        isDashing = false;

    }

    IEnumerator DashCooldown() {
        onCooldown = true;
        StartUI();
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    void StartUI() {
        cdImg.enabled = true;
        cdImg.fillAmount = 1f;
    }

    void UpdateUI() {
        if (onCooldown)
            cdImg.fillAmount = Mathf.Clamp(1 - ((Time.time - lastDash) / cooldown), 0f, 1f);
        else StopUI();
    }

    void StopUI() {
        cdImg.enabled = false;
    }
}
