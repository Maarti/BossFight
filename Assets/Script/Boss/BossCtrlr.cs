using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrlr : MonoBehaviour, IAttackable {

    [Header("Boss settings")]
    [SerializeField] Transform projectileSpawnPos;
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] float projectileSpeed = 25f;
    [SerializeField] float projectileSizeMultiplier = 3f;
    [Header("Projectiles salve settings")]
    [SerializeField] [Tooltip("Where the salve will start to aim")] Transform startSalvePos;
    [SerializeField] [Tooltip("Where the salve will go (from startSalvePos)")] Transform endSalvePos;
    [SerializeField] float timeBetweenProjectiles = .1f;
    [SerializeField] [Tooltip("Number of projectiles in one salve")] int projectileNumber = 40;
    [SerializeField] [Tooltip("Number of consecutives porjectiles that will be removed from each salve")] int missingProjectile = 2;
    [SerializeField] int consecutiveSalves = 1;
    [SerializeField] float timeBetweenSalves = 15f;
    Animator anim;
    Quaternion[] projectilesDirections;
    int currentDirection = 0;
    float lastSalve = 0f;

    void Start() {
        anim = GetComponent<Animator>();
        StartCoroutine(SpawnSalve());

        if (projectileNumber < 2)
            Debug.LogWarning("projectileNumber should more than 2!");
        if (missingProjectile< 0 || missingProjectile>=projectileNumber)
            Debug.LogWarning("wrong missingProjectile number!");
    }

    void Update() {
        if (Time.time > lastSalve + timeBetweenSalves) {
            StartCoroutine(SpawnSalve());
            lastSalve = Time.time;
        }
    }

    public void Defend(float damage) {
        Debug.Log(this.name + " defends " + damage + " dmg");
        anim.SetTrigger("hit");
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
                    ThrowProjectile(projectilesDirections[currentDirection]);             
                yield return new WaitForSeconds(timeBetweenProjectiles);
            }
            nbSalve++;
        }
    }

    void ThrowProjectile(Quaternion direction) {
        GameObject projectile = Instantiate(fireballPrefab, projectileSpawnPos.position, direction);
        projectile.GetComponent<AbstractProjectile>().speed = projectileSpeed;
        projectile.transform.localScale *= projectileSizeMultiplier;
        projectile.layer = LayerMask.NameToLayer("Enemy");
    }

    void ComputeProjectilesDirections() {
        Vector3 rightTarget = new Vector3(startSalvePos.position.x, projectileSpawnPos.position.y, startSalvePos.position.z);
        Vector3 leftTarget = new Vector3(endSalvePos.position.x, projectileSpawnPos.position.y, endSalvePos.position.z);
        float diff = (rightTarget.x - leftTarget.x) / (projectileNumber - 1);
        projectilesDirections = new Quaternion[projectileNumber];
        Vector3 target = rightTarget;
        for (int i = 0; i < projectileNumber; i++) {
            projectilesDirections[i] = Quaternion.LookRotation(target - projectileSpawnPos.position);
            target.x -= diff;
        }
    }

}
