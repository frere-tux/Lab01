using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    public Camera playerCamera;
    public float rollForce = 2.0f;
    public float pitchForce = 2.0f;
    public float yawForce = 2.0f;
    public float throttleForce = 2000.0f;
    public float verticalForce = 1000.0f;
    public float sideForce = 1000.0f;

    public float zoomEffect = 1.0f;

    public float boostMax = 3.0f;
    public float boostSpeed = 3.0f;
    public float boostFov = 100.0f;
    public float boostStars = 2.5f;

    public StarFieldSystem stars;

    float rollInput;
    float pitchInput;
    float yawInpinput;
    float throttleInput;
    float verticalInput;
    float sideInput;
    float zoomInput;

    float initialFov;

    float boost = 1.0f;

    private void Start()
    {
        initialFov = playerCamera.fieldOfView;
    }



    private void FixedUpdate()
    {
        // Boost
        float currentBoost = boost;
        if (boost < boostMax && Input.GetButton("Boost") && throttleInput > 0.8f)
        {
            currentBoost = Mathf.Lerp(boost, boostMax, boostSpeed * Time.deltaTime);
        }
        else if (boost > 1.0f)
        {
            currentBoost = Mathf.Lerp(boost, 1.0f, boostSpeed * Time.deltaTime);
        }

        if (boost != currentBoost)
        {
            boost = currentBoost;
            float boostRatio = (boost - 1.0f) / (boostMax - 1.0f);
            float boostFovModifier = boostRatio * (boostFov - initialFov);

            playerCamera.fieldOfView = initialFov + boostFovModifier;

            stars.BoostRatio =  1.0f + boostRatio * boostStars;
        }

        // Controls
        rollInput = Input.GetAxis("Roll");
        pitchInput = Input.GetAxis("Pitch");
        yawInpinput = Input.GetAxis("Yaw");

        throttleInput = Input.GetAxis("Throttle");
        verticalInput = Input.GetAxis("Vertical");
        sideInput = Input.GetAxis("Side");

        rb.AddForce(rb.transform.forward * throttleInput * throttleForce * boost);
        rb.AddForce(rb.transform.up * verticalInput * verticalForce);
        rb.AddForce(rb.transform.right * sideInput * sideForce);

        rb.AddTorque(rb.transform.forward * rollInput * rollForce);
        rb.AddTorque(rb.transform.up * yawInpinput * yawForce);
        rb.AddTorque(rb.transform.right * pitchInput * pitchForce);

        // Zoom
        float currentZoomInput = Input.GetAxis("Zoom");
        if (zoomInput != currentZoomInput)
        {
            zoomInput = currentZoomInput;

            playerCamera.fieldOfView = initialFov + initialFov * zoomInput * zoomEffect;
        }
    }
}
