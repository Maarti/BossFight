﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] float speed = 7f;
    [SerializeField] float dashSpeed = 20f;
    [SerializeField] float dashDuration = .2f;
    [SerializeField] [Tooltip("Enemies in this range will be focus autmatically")] float distanceMaxToAutoFocus = 25f;
    [SerializeField] Transform target;
    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Rigidbody rb;
    Animator anim;
    bool isDashing = false;
    List<GameObject> focusables = new List<GameObject>();
    float lastTimeFocusSearch = -10;

    // Use this for initialization
    void Start() {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (CrossPlatformInputManager.GetButtonDown("Dash"))
            StartCoroutine(Dash());
    }

    // Fixed update is called in sync with physics
    void FixedUpdate() {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        float v = CrossPlatformInputManager.GetAxis("Vertical");
        FindAllFocusableObjects();
        AutoFocus();
        Move(v, h);
        Turning();
    }

    void Move(float v, float h) {
        // calculate camera relative direction to move:
        camForward = Vector3.Scale(cam.forward, new Vector3(1, 0, 1)).normalized;
        move = v * camForward + h * cam.right;
        if (move.magnitude > 1f) move.Normalize();
        if (isDashing)
            move *= dashSpeed * Time.deltaTime;
        else
            move *= speed * Time.deltaTime;
        rb.MovePosition(transform.position + move);
    }

    void Turning() {
        Quaternion newRotation;
        if (target) {
            Vector3 direction = target.position - transform.position;
            newRotation = Quaternion.LookRotation(direction);
        }
        else {
            if (move == Vector3.zero)
                return;
            newRotation = Quaternion.LookRotation(move);
        }
        rb.rotation = newRotation;
    }

    IEnumerator Dash() {
        isDashing = true;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        yield return null;
    }

    void FindAllFocusableObjects() {
        if (Time.time - lastTimeFocusSearch > 1f) {
            focusables = new List<GameObject>(GameObject.FindGameObjectsWithTag("Boss"));
            /*foreach (GameObject o in GameObject.FindGameObjectsWithTag("Enemy")) {
                focusables.Add(o);
            }*/
            lastTimeFocusSearch = Time.time;
        }
    }

    void AutoFocus() {
        Vector3 diff;
        float distanceMin = distanceMaxToAutoFocus;
        GameObject closerFocusable = null;
        foreach (GameObject o in focusables) {
            diff = o.transform.position - transform.position;
            if (diff.magnitude <= distanceMin)
                closerFocusable = o;
        }
        if (closerFocusable != null)
            target = closerFocusable.transform;
        else
            target = null;
    }

}
