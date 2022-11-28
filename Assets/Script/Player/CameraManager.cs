using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Transform target;
    private Camera cam;
    public BoxCollider2D areaBox;
    private float halfWidth,halfHeight;

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerMovement>().transform;
        cam = GetComponent<Camera>();

        halfHeight = cam.orthographicSize;
        halfWidth = cam.orthographicSize * cam.aspect;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, areaBox.bounds.min.x + halfWidth, areaBox.bounds.max.x - halfWidth),
                                         Mathf.Clamp(transform.position.y, areaBox.bounds.min.y + halfHeight, areaBox.bounds.max.y - halfHeight), transform.position.z);

    }
}
