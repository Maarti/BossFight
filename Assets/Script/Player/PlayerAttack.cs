using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerStatus))]
public class PlayerAttack : MonoBehaviour {

    [Header("Projectiles settings")]
    [SerializeField] ProjectileType projectileType = ProjectileType.Fireball;
    [SerializeField] Transform projectilePosition;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float fireballSpeed = 20f;
    [SerializeField] [Tooltip("Energy cost of the projectile")] float fireballCost = 20f;
    PlayerStatus playerStatus;

    Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        playerStatus = GetComponent<PlayerStatus>();
        playerStatus.projectileCost = fireballCost;
    }

    // Update is called once per frame
    void Update() {
        if (CrossPlatformInputManager.GetButtonDown("AttackSword"))
            anim.SetTrigger("attackSword01");
        if (CrossPlatformInputManager.GetButtonDown("AttackProjectile"))
            ThrowProjectile();
        if (CrossPlatformInputManager.GetButtonDown("Shield"))
            Shield(true);
        else if (CrossPlatformInputManager.GetButtonUp("Shield"))
            Shield(false);
    }

    void ThrowProjectile() {
        switch (projectileType) {
            case ProjectileType.Fireball:
                ThrowFireball();
                break;
        }
    }

    private void ThrowFireball() {
        if (playerStatus.HasEnoughEnergy(fireballCost)) {
            GameObject projectile = Instantiate(fireballPrefab, projectilePosition.position, transform.rotation);
            projectile.GetComponent<AbstractProjectile>().speed = fireballSpeed;
            playerStatus.Energy -= fireballCost;
        }
    }

    void Shield(bool activate) {
        anim.SetBool("isShielding", activate);
    }

}
