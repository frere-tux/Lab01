using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMultiTarget : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;

    public float smoothTime = 0.5f;

    public float minZoom = 40.0f;
    public float maxZoom = 10.0f;
    public float zoomLimiter = 25.0f;

    public float minSize = 5.0f;
    public float maxSize = 15.0f;
    public float sizeLimiter = 25.0f;

    private Vector3 velocity;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Move();
        Zoom();
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void Zoom()
    {
        if (cam.orthographic)
        {
            float newSize = Mathf.Lerp(minSize, maxSize, GetGreatestDistance() / sizeLimiter);
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime);
        }
        else
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }
    }

    private float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            bounds.Encapsulate(target.position);
        }

        return bounds.center;
    }
}
