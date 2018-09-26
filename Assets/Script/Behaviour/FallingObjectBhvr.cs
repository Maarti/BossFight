using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectBhvr : MonoBehaviour {

    [Header("Prefabs")]
    [SerializeField] GameObject stalactitePrefab;
    [Header("Falling object settings")]
    [SerializeField] float timeBeforeFalling = 1.5f;
    [SerializeField] Vector3 projectileSize = Vector3.one;
    [SerializeField] string projectileLayerName = "Enemy";
    int layer;

    void Start() {
        layer = LayerMask.NameToLayer(projectileLayerName);
    }

    public GameObject StartBehaviour(Vector3 position, ProjectileType type = ProjectileType.Fireball, float speedMultiplier = 1f, float sizeMultiplier = 1f) {
        GameObject element = Instantiate(stalactitePrefab, position, stalactitePrefab.transform.rotation);
        element.GetComponentInChildren<AbstractFallingObject>().timeBeforeFalling = timeBeforeFalling;
        element.transform.localScale = projectileSize * sizeMultiplier;
        element.layer = layer;
        return element;
    }
}
