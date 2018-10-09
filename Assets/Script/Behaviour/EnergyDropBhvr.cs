using System;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDropBhvr : MonoBehaviour {

    [SerializeField] AttackType[] attackType;
    [SerializeField] GameObject energyBallPrefab;
    [Header("Number of energy balls")]
    [SerializeField] int minDropNumber = 1;
    [SerializeField] int maxDropNumber = 3;
    [Header("Forces of energy balls")]
    [SerializeField] float minUpForce = 20f;
    [SerializeField] float maxUpForce = 50f;
    [SerializeField] float minForwardForce = 5f;
    [SerializeField] float maxForwardForce = 20f;


    public void StartBehaviour(Attack attack) {
        if (Array.Exists<AttackType>(attackType, element => element == attack.attackType)) {
            int totalDrop = UnityEngine.Random.Range(minDropNumber, maxDropNumber + 1);
            for (int count = 1; count <= totalDrop; count++) {
                DropEnergy();
            }
        }
    }

    void DropEnergy() {
        GameObject energyBallObj = Instantiate(energyBallPrefab, transform.position, Quaternion.Euler(0, UnityEngine.Random.Range(0f, 360f), 0));
        EnergyBall energyBall = energyBallObj.GetComponent<EnergyBall>();
        energyBall.forwardForce = UnityEngine.Random.Range(minForwardForce, maxForwardForce);
        energyBall.upForce = UnityEngine.Random.Range(minUpForce, maxUpForce);
    }

}
