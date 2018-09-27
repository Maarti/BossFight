using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectBhvr : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] GameObject stalactitePrefab;
    [Header("Falling object settings")]
    [SerializeField] float timeBeforeFalling = 1.5f;
    [SerializeField] float timeToReachMark = .3f;
    [SerializeField] Vector3 projectileSize = Vector3.one;
    [SerializeField] string projectileLayerName = "Enemy";
    int layer;

    void Start() {
        layer = LayerMask.NameToLayer(projectileLayerName);
    }

    public GameObject StartBehaviour(Vector3 position, float speedMultiplier = 1f, float sizeMultiplier = 1f, ProjectileType type = ProjectileType.Fireball) {
        GameObject element = Instantiate(stalactitePrefab, position, stalactitePrefab.transform.rotation);
        AbstractFallingObject script = element.GetComponentInChildren<AbstractFallingObject>();
        script.timeBeforeFalling = timeBeforeFalling;
        script.timeToReachMark = timeToReachMark;
        element.transform.localScale = projectileSize * sizeMultiplier;
        element.layer = layer;
        return element;
    }
}
