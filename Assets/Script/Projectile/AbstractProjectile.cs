using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Fireball, Axe }
public abstract class AbstractProjectile : MonoBehaviour {

    public float damage = 3f;
    public float speed = 20f;
    public bool destroyOnCollision = true;

    void Awake() {
        GameObject.Destroy(this.gameObject, 5f);
    }

    protected void OnTriggerEnter(Collider other) {
        Debug.Log(this.name + " trigger with " + other.name);
        if (other.tag == "Boss") {
            other.GetComponent<IAttackable>().Defend(damage);
        }
        else if (other.tag == "ReflectorShield") {
            this.gameObject.layer = LayerMask.NameToLayer("Friendly");
            transform.rotation = other.transform.rotation;
            return;
        }
        if (destroyOnCollision)
            GameObject.Destroy(this.gameObject);
    }

    void FixedUpdate() {
        transform.position += transform.forward * Time.deltaTime * speed;
    }
}
