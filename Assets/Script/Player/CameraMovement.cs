using UnityEngine;

public class CameraMovement : MonoBehaviour {

    [HideInInspector] public Vector3 center = new Vector3(0f, 0f, 0f);
    [SerializeField] [Tooltip("Transform placed at the center of the room. The camera will try to stick at it")] Transform roomCenter;
    [SerializeField] Vector3 offset = new Vector3(0f, 15f, -18f);
    [SerializeField] [Tooltip("Camera will move if the player exceeds this distance from the center")] float centerDistMax = 12f;
    [SerializeField] float smoothing = 2f;
    Transform target;

    void Start() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (!roomCenter)
            Debug.LogWarning("Assign a RoomCenter transform to " + name);
        else
            center = roomCenter.transform.position;
    }

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(center, centerDistMax);
    }

    void LateUpdate() {
        Vector3 centerToTarget = target.position - center;
        float targetDistToCenter = centerToTarget.magnitude;
        if (targetDistToCenter > centerDistMax) {
            transform.position = Vector3.Lerp(transform.position, (target.position + offset), smoothing * Time.deltaTime);
        }
        else if (transform.position != center + offset) {
            transform.position = Vector3.Lerp(transform.position, (center + offset), smoothing * Time.deltaTime);
        }
    }

}
