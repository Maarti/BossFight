using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalveBhvr : MonoBehaviour {

    [Header("Projectile settings")]
    [SerializeField] ThrowProjectileBhvr throwProjectileBhvr;
    [Header("Salve settings")]
    [SerializeField] [Tooltip("Where the salve will start to aim")] Transform startSalvePos;
    [SerializeField] [Tooltip("Where the salve will go (from startSalvePos)")] Transform endSalvePos;
    [SerializeField] [Tooltip("Number of projectiles in one salve")] int projectileNumber = 40;
    [SerializeField] float timeBetweenProjectiles = .1f;
    [SerializeField] [Tooltip("Number of consecutives porjectiles that will be removed from each salve")] int missingProjectile = 2;
    [SerializeField] int consecutiveSalves = 1;

    bool isSalving = false;
    Quaternion[] projectilesDirections;
    int currentDirection = 0;
    Coroutine spawnSalveCoroutine;

    void Start() {
        if (projectileNumber < 2)
            Debug.LogWarning("projectileNumber should more than 2!");
        if (missingProjectile < 0 || missingProjectile >= projectileNumber)
            Debug.LogWarning("wrong missingProjectile number!");
    }

    public void StartBehaviour() {
        spawnSalveCoroutine = StartCoroutine(SpawnSalve());
    }

    public void StopBehaviour() {
        if (spawnSalveCoroutine != null)
            StopCoroutine(spawnSalveCoroutine);
    }
    
    IEnumerator SpawnSalve() {
        ComputeProjectilesDirections();
        int missingRand = 0;
        int nbSalve = 0;
        while (nbSalve < consecutiveSalves) {
            for (int currentDirection = 0; currentDirection < projectilesDirections.Length; currentDirection++) {
                // Choose what (first) projectile will be missing for this salve
                if (currentDirection == 0 && missingProjectile != 0)
                    missingRand = Random.Range(1, projectileNumber - missingProjectile);
                // Throw projectiles excepts the missing ones
                if (currentDirection < missingRand || currentDirection >= missingRand + missingProjectile || missingProjectile == 0)
                    throwProjectileBhvr.StartBehaviour(projectilesDirections[currentDirection]);
                yield return new WaitForSeconds(timeBetweenProjectiles);
            }
            nbSalve++;
        }
    }

    void ComputeProjectilesDirections() {
        Vector3 rightTarget = new Vector3(startSalvePos.position.x, throwProjectileBhvr.projectileSpawnPos.position.y, startSalvePos.position.z);
        Vector3 leftTarget = new Vector3(endSalvePos.position.x, throwProjectileBhvr.projectileSpawnPos.position.y, endSalvePos.position.z);
        float diff = (rightTarget.x - leftTarget.x) / (projectileNumber - 1);
        projectilesDirections = new Quaternion[projectileNumber];
        Vector3 target = rightTarget;
        for (int i = 0; i < projectileNumber; i++) {
            projectilesDirections[i] = Quaternion.LookRotation(target - throwProjectileBhvr.projectileSpawnPos.position);
            target.x -= diff;
        }
    }
}
