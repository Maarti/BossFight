using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FallingObjectBhvr))]
public class CollapsingBhvr : MonoBehaviour {

    [Header("Falling object settings")]
    [SerializeField] FallingObjectBhvr fallingObjectBhvr;
    [Header("Collapsing settings")]
    [SerializeField] Transform center;
    [SerializeField] float radius = 20f;
    [SerializeField] int nbObjects = 24;
    [SerializeField] float timeBetweenObjects = 0.2f;

    Coroutine coroutine;

    public void StartBehaviour() {
        coroutine = StartCoroutine(SpawnCollapsing());
    }

    public void StopBehaviour() {
        StopCoroutine(coroutine);
    }

    IEnumerator SpawnCollapsing() {
        int count = 0;
        WaitForSeconds wait = new WaitForSeconds(timeBetweenObjects);
        while (count < nbObjects) {
            Vector2 random = Random.insideUnitCircle * radius;
            Vector3 position = new Vector3(center.position.x + random.x, center.position.y, center.position.z + random.y);
            fallingObjectBhvr.StartBehaviour(position);
            count++;
            yield return wait;
        }
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center.position, radius);
    }

}