using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectileBhvr : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] GameObject fireballPrefab;
    [Header("Projectile settings")]
    public Transform projectileSpawnPos;
    [SerializeField] ProjectileType projectileType;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] Vector3 projectileSize = Vector3.one;
    [SerializeField] string projectileLayerName = "Enemy";
    int layer;

    void Start() {
        layer = LayerMask.NameToLayer(projectileLayerName);
    }

    public GameObject StartBehaviour(Quaternion direction, ProjectileType type = ProjectileType.Fireball, float speedMultiplier = 1f, float sizeMultiplier = 1f) {
        GameObject projectile;
        switch (type) {
            case ProjectileType.Fireball:
            default:
                projectile = Instantiate(fireballPrefab, projectileSpawnPos.position, direction);
                break;
        }
        projectile.GetComponent<AbstractProjectile>().speed = projectileSpeed * speedMultiplier;
        projectile.transform.localScale = projectileSize * sizeMultiplier;
        projectile.layer = layer;
        return projectile;
    }
}
