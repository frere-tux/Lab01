using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

    public Camera playerCamera;
    public Transform portal;
    public Transform otherPortal;

    private Camera portalCamera;

    void Start()
    {
        portalCamera = GetComponent<Camera>();
    }

    void Update ()
    {
        portalCamera.fieldOfView = playerCamera.fieldOfView;

        Vector3 playerOffsetFromPortal = playerCamera.transform.position - otherPortal.position;
        Quaternion rotationDiffBetweenPortals = portal.rotation * Quaternion.Inverse(otherPortal.rotation) * Quaternion.AngleAxis(180.0f, otherPortal.up);

        transform.position = portal.position + rotationDiffBetweenPortals * playerOffsetFromPortal;

        transform.rotation = rotationDiffBetweenPortals * playerCamera.transform.rotation;
    }
}
