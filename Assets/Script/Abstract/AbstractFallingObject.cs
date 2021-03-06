﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFallingObject : MonoBehaviour {

    public float damage = 1f;
    public float timeToReachMark = .5f;
    public float timeBeforeFalling = 1f;
    public Transform groundMark;
    public SpriteRenderer markSprite;
    [SerializeField] MeshRenderer mesh;
    Vector3 startPos;
    float startTime;

    void Awake() {
        startPos = transform.position;
        startTime = Time.time;
        mesh.enabled = false;
        Destroy(this.gameObject, timeBeforeFalling + timeToReachMark + 2);
    }

    protected void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            other.GetComponent<IAttackable>().Defend(new Attack(AttackType.FallingObject, damage));
        }
        // Destroy(this.gameObject);
    }

    void FixedUpdate() {
        if (Time.time >= startTime + timeBeforeFalling) {
            transform.position = Vector3.Lerp(startPos, groundMark.position, (Time.time - (startTime + timeBeforeFalling)) / timeToReachMark);
            if (markSprite.enabled)
                markSprite.enabled = false;
            if (!mesh.enabled)
                mesh.enabled = true;
        }
    }
}
