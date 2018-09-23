using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerAttack : MonoBehaviour {

    [SerializeField] ProjectileType projectileType = ProjectileType.Fireball;
    [SerializeField] Transform projectilePosition;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float fireballSpeed = 20;

    Animator anim;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (CrossPlatformInputManager.GetButtonDown("AttackSword"))
            anim.SetTrigger("attackSword01");
        if (CrossPlatformInputManager.GetButtonDown("AttackProjectile"))
            ThrowProjectile();
    }

    void ThrowProjectile() {
        switch (projectileType) {
            case ProjectileType.Fireball:
                GameObject projectile = Instantiate(fireballPrefab, projectilePosition.position, transform.rotation);
                projectile.GetComponent<AbstractProjectile>().speed = fireballSpeed;
                break;
        }
    }

}
