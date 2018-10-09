using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnergyBall : MonoBehaviour {

    [SerializeField] float amountPercentage = 3f;
    [SerializeField] float timeToLive = 6f;
    public float upForce = 5f;
    public float forwardForce = 2f;
    Rigidbody rb;

    void Start() {
        Invoke("FadeOut", timeToLive);
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward*forwardForce + Vector3.up*upForce);
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player") {
            Collected(collision.gameObject.GetComponent<PlayerStatus>());
        }
    }

    void FadeOut() {
        // TODO Fade out animation
        Destroy(this.gameObject);
    }

    void Collected(PlayerStatus playerStatus) {
        playerStatus.CollidWithEnergyBall(amountPercentage);
        Destroy(this.gameObject);
    }

}
