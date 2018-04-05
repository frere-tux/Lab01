using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public bool lookAtTarget = true;

    private void Start()
    {
        transform.position = target.position + offset;
        transform.LookAt(target);
    }

    private void FixedUpdate ()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        if (lookAtTarget)
            transform.LookAt(target);
	}
}
